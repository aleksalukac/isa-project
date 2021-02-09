using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
    public class SaleItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailSender _emailSender;

        public SaleItemsController(UserManager<AppUser> userManager, ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            using (StreamReader r = new StreamReader("./Areas/Identity/emailCredentials.json"))
            {
                string json = r.ReadToEnd();
                _emailSender = JsonConvert.DeserializeObject<EmailSender>(json);
            }
        }

        // GET: SaleItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.SaleItems.ToListAsync());
        }

        // GET: SaleItems/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleItems = await _context.SaleItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleItems == null)
            {
                return NotFound();
            }

            return View(saleItems);
        }

        // GET: SaleItems/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["DrugAndQuantities"] = await  _context.DrugAndQuantity.ToListAsync();
            ViewData["Drugs"] = await _context.tbDrugs.ToListAsync();
            ViewData["SaleItems"] = await _context.SaleItems.ToListAsync();
            return View();
        }

        // POST: SaleItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BeforePrice,EndTime, DrugAndQuantitiesId")] SaleItems saleItems, long DrugAndQuantitiesId)
        {
            if (ModelState.IsValid)
            {
                saleItems.DrugAndQuantitiesId = DrugAndQuantitiesId;
                var drugAndQuantity = await _context.DrugAndQuantity.Include(x => x.Drug).FirstOrDefaultAsync(x => x.Id == DrugAndQuantitiesId) ;
                //var drugAndQuantity = await _context.DrugAndQuantity.FindAsync(DrugAndQuantitiesId);
                var price = drugAndQuantity.Price;
                drugAndQuantity.Price = saleItems.BeforePrice;
                saleItems.BeforePrice = price;
                _context.Update(drugAndQuantity);
                _context.Add(saleItems);
                await _context.SaveChangesAsync();

                // Sending Email to each user
                var pharmacyAdmin = await _userManager.GetUserAsync(User);
                var userSub = await _context.UserSubscribed.Where(x => x.PharmacyId == pharmacyAdmin.PharmacyId).Select(x => x.UserId).ToArrayAsync();
                List<string> entryPoint = await (from userrole in _context.UserRoles
                                                 join role in _context.Roles on userrole.RoleId equals role.Id
                                                 where role.Name == "User"
                                                 select userrole.UserId).ToListAsync();
                var user = await _context.AppUsers.Where(e => entryPoint.Contains(e.Id)).Where(x => userSub.Contains(x.Id)).ToListAsync();

                var drugAtDiscort = await _context.tbDrugs.FindAsync(drugAndQuantity.Drug.Id);
                var pharmacy = await _context.tbPharmacys.FindAsync(pharmacyAdmin.PharmacyId);
                
                foreach (AppUser UsersEmail in user)
                {
                    await _emailSender.SendEmailAsync(UsersEmail.Email, "DISCORT AT" + pharmacy.Name,
                         $"Order " + drugAtDiscort.Name + " for  ONLY " + drugAndQuantity.Price+ " until "+saleItems.EndTime+"! Save "+ (saleItems.BeforePrice - drugAndQuantity.Price) + "  EUROS");
                }

                return RedirectToAction(nameof(Index));
            }
            return View(saleItems);
        }

        // GET: SaleItems/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleItems = await _context.SaleItems.FindAsync(id);
            if (saleItems == null)
            {
                return NotFound();
            }
            return View(saleItems);
        }

        // POST: SaleItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,BeforePrice,EndTime")] SaleItems saleItems)
        {
            if (id != saleItems.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saleItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleItemsExists(saleItems.Id))
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
            return View(saleItems);
        }

        // GET: SaleItems/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleItems = await _context.SaleItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saleItems == null)
            {
                return NotFound();
            }

            return View(saleItems);
        }

        // POST: SaleItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var saleItems = await _context.SaleItems.FindAsync(id);
            _context.SaleItems.Remove(saleItems);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleItemsExists(long id)
        {
            return _context.SaleItems.Any(e => e.Id == id);
        }
    }
}
