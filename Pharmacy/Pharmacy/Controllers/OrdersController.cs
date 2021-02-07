using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pharmacy.Areas.Identity;
using Pharmacy.Data;
using Pharmacy.Models.Entities;
using Pharmacy.Models.Entities.Users;

namespace Pharmacy.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailSender _emailSender;

        public OrdersController(UserManager<AppUser> userManager,
            ApplicationDbContext context,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _context = context;
            using (StreamReader r = new StreamReader("./Areas/Identity/emailCredentials.json"))
            {
                string json = r.ReadToEnd();
                _emailSender = JsonConvert.DeserializeObject<EmailSender>(json);
            }
        }

        // GET: Orders
        /*public async Task<IActionResult> Index()
        {
            return View(await _context.tbOrders.ToListAsync());
        }*/

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.tbOrders
                .Include(order => order.DrugAndQuantities)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            var quant = await _context.DrugAndQuantity
                .Include(drugQuant => drugQuant.Drug)
                .FirstOrDefaultAsync(m => m.Id == order.DrugAndQuantities.Id);
            ViewData["DrugName"] = quant.Drug.Name;

            return View(order);
        }

        // GET: Orders/Create
        [HttpGet("Orders/Create/{drugQuantId}")]
        public async Task<IActionResult> Create(long? drugQuantId)
        {
            if (drugQuantId == null)
            {
                return NotFound();
            }
            /*var drugsQuantList = await (from drug in _context.tbDrugs
                                join drugsQuant in _context.DrugAndQuantity on drug equals drugsQuant.Drug
                                where drugsQuant.Drug.Id == drugId && drugsQuant.Quantity > 0
                                select drugsQuant.PharmacyId).ToListAsync();
           
            Drug drugInstance = await _context.tbDrugs.FindAsync(drugId);*/
            var drugQuantity = await _context.DrugAndQuantity
                .Include(drugQuant => drugQuant.Drug)
                .FirstOrDefaultAsync(m => m.Id == drugQuantId);

            var pharmacy = await _context.tbPharmacys.FindAsync(drugQuantity.PharmacyId);
            ViewData["Pharmacy"] = pharmacy.Name;
            ViewData["DrugName"] = drugQuantity.Drug.Name;
            ViewData["drugQuantityId"] = drugQuantity.Id;
            ViewData["DrugCost"] = drugQuantity.Price;
            
            return View();
        }

        //[HttpGet("Orders/UserList")]
        // GET: Orders/ScheduledOrders
        public async Task<IActionResult> ScheduledOrders()
        {
            var user = await _userManager.GetUserAsync(User);
            var timeNow = DateTime.Now;

            return View(await _context.tbOrders.Where(m => m.User.Id == user.Id && timeNow < m.TimeOfTransaction).ToListAsync());
        }

        // POST: Orders/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(long? drugQuantId, double? cost, long? pharmacy, DateTime? time)
        {
            var drugsQuantEl = await _context.DrugAndQuantity.FindAsync(drugQuantId);
            //change drugs and quant
            drugsQuantEl.Quantity -= 1;
            _context.Update(drugsQuantEl);
            await _context.SaveChangesAsync();

            //make a new order
            var order = new Order();
            order.DrugAndQuantities = drugsQuantEl;
            order.TimeOfTransaction = (DateTime)time;
            order.Cost = (double)cost;
            order.User = await _userManager.GetUserAsync(User);
            order.TransactionComplete = false;
            _context.Add(order);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            await _emailSender.SendEmailAsync(order.User.Email, "Pharmacy Order",
                $"Order indetification code is: {order.Id}");

            return RedirectToAction(nameof(ScheduledOrders));// ScheduledOrders();
        }


        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.tbOrders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Cost")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.tbOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var order = await _context.tbOrders.FindAsync(id);
            _context.tbOrders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(long id)
        {
            return _context.tbOrders.Any(e => e.Id == id);
        }
    }
}
