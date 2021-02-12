using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    [Authorize(Roles = "PharmacyAdmin")]
    public class SupplyOffersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;

        public SupplyOffersController(ApplicationDbContext context, UserManager<AppUser> userManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;

            using (StreamReader r = new StreamReader("./Areas/Identity/emailCredentials.json"))
            {
                string json = r.ReadToEnd();
                _emailSender = JsonConvert.DeserializeObject<EmailSender>(json);
            }
        }

        // GET: SupplyOffers
        public async Task<IActionResult> Index()
        {
            var pharm = await _userManager.GetUserAsync(User);
            return View(await _context.tbSupplyOffers.ToListAsync());
        }

        // GET: SupplyOffers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplyOffer = await _context.tbSupplyOffers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplyOffer == null)
            {
                return NotFound();
            }

            return View(supplyOffer);
        }

        // GET: SupplyOffers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SupplyOffers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Offer")] SupplyOffer supplyOffer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplyOffer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplyOffer);
        }

        // GET: SupplyOffers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplyOffer = await _context.tbSupplyOffers.FindAsync(id);
            if (supplyOffer == null)
            {
                return NotFound();
            }
            return View(supplyOffer);
        }

        // POST: SupplyOffers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Offer")] SupplyOffer supplyOffer)
        {
            if (id != supplyOffer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplyOffer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplyOfferExists(supplyOffer.Id))
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
            return View(supplyOffer);
        }

        // GET: SupplyOffers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplyOffer = await _context.tbSupplyOffers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplyOffer == null)
            {
                return NotFound();
            }

            return View(supplyOffer);
        }

        // POST: SupplyOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var supplyOffer = await _context.tbSupplyOffers.FindAsync(id);
            _context.tbSupplyOffers.Remove(supplyOffer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplyOfferExists(long id)
        {
            return _context.tbSupplyOffers.Any(e => e.Id == id);
        }

        [Route("SupplyOffers/AcceptAsync/{Id}")]
        private async Task<IActionResult> AcceptAsync(long Id)
        {
            var supplyOffer = await _context.tbSupplyOffers
                .FirstOrDefaultAsync(m => m.Id == Id);
            var user = await _userManager.GetUserAsync(User);
            var supplyOrders = await _context.tbSupplyOrders.Where(x => x.PharmacyId == user.PharmacyId).ToListAsync();
            var supplier = await _userManager.GetUserAsync(User);
            var allSupplyOffers = await _context.tbSupplyOffers.Where(x => x.SupplyOrder.Id == supplyOffer.SupplyOrder.Id).ToListAsync();
            var drugsAndQ = await _context.DrugAndQuantity.Where(x => x.PharmacyId == user.PharmacyId).ToListAsync();

            for (int i = 0; i < allSupplyOffers.Count(); i++)
            {
                if (allSupplyOffers[i].Id == supplyOffer.Id)
                {
                    await _emailSender.SendEmailAsync(user.Email, "Offer has been accepted :",
                        $" Tnx for supporting us");
                    var allSupplyItems = await _context.SupplyItems.Where(x => x.SupplyOrderId == allSupplyOffers[i].SupplyOrder.Id).ToListAsync();
                    for (int j = 0; j < allSupplyItems.Count(); j++)
                    {
                        for (int k = 0; k < drugsAndQ.Count(); k++)
                        {
                            if (allSupplyItems[j].DrugId == drugsAndQ[k].Drug.Id)
                            {
                                drugsAndQ[k].Quantity += (uint)allSupplyItems[j].ExtraQuantity;

                                try
                                {
                                    _ = await _context.AddAsync(drugsAndQ[k]);
                                    _ = await _context.SaveChangesAsync();
                                }
                                catch (DbUpdateConcurrencyException)
                                {
                                    return NotFound();
                                }
                                break;
                            }
                        }

                    }
                } else
                {
                    await _emailSender.SendEmailAsync(user.Email, "Offer has been rejected :",
                        $" Tnx for supporting us");
                }
                _context.tbSupplyOffers.Remove(allSupplyOffers[i]);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
