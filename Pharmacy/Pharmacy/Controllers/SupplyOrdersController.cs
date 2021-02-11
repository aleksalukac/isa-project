using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Data;
using Pharmacy.Models.Entities;
using Pharmacy.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Pharmacy.Models.Entities.Users;

namespace Pharmacy.Controllers
{
    public class SupplyOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public SupplyOrdersController(UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public SupplyOrderModelDTO SupplyOrders { get; set; }

        // GET: SupplyOrders
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbSupplyOrders.ToListAsync());
        }

        // GET: SupplyOrders/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplyOrder = await _context.tbSupplyOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplyOrder == null)
            {
                return NotFound();
            }

            return View(supplyOrder);
        }

        // GET: SupplyOrders/Create
        public async Task<IActionResult> CreateAsync()
        {
            //ViewData["DrugList"] = await _context.tbDrugs.ToListAsync();
            SupplyOrders = new SupplyOrderModelDTO();
            var drugs = await _context.tbDrugs.ToListAsync();
            SupplyOrders.SupplyItems = new List<SupplyItemDTO>();
            for (int i = 0; i < drugs.Count; i++)
            {
                SupplyItemDTO SupplyItemView = new SupplyItemDTO();
                SupplyItemView.Drug = new DrugDTO(drugs[i].Id, drugs[i].Name);
                SupplyItemView.Drug.Name = drugs[i].Name;
                SupplyItemView.DrugName = SupplyItemView.Drug.Name;
                SupplyItemView.DrugId = SupplyItemView.Drug.Id;
                SupplyOrders.SupplyItems.Add(SupplyItemView);
            }
            return View(SupplyOrders);
        }

        // POST: SupplyOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplyItems, DateExpired, SupplyOrders")] SupplyOrderModelDTO supplyOrderModel)
        {
            SupplyOrder supplyOrder = new SupplyOrder();
            supplyOrder.DeleveryDate = supplyOrderModel.DateExpired;
            var user = await _userManager.GetUserAsync(User);
            supplyOrder.PharmacyId = user.PharmacyId;
            if (ModelState.IsValid)
            {
                await _context.AddAsync(supplyOrder);
                await _context.SaveChangesAsync();
                for (int i = 0; i < supplyOrderModel.SupplyItems.Count(); i++)
                {
                    if (supplyOrderModel.SupplyItems[i].ExtraQuantity > 0)
                    {
                        SupplyItem supplyItem = new SupplyItem();
                        supplyItem.ExtraQuantity = supplyOrderModel.SupplyItems[i].ExtraQuantity;
                        supplyItem.DrugId = supplyOrderModel.SupplyItems[i].DrugId;
                        supplyItem.SupplyOrderId = supplyOrder.Id;
                        await _context.AddAsync(supplyItem);
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplyOrderModel.SupplyOrder);
        }

        // GET: SupplyOrders/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplyOrder = await _context.tbSupplyOrders.FindAsync(id);
            if (supplyOrder == null)
            {
                return NotFound();
            }
            return View(supplyOrder);
        }

        // POST: SupplyOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,PharmacyId,DeleveryDate")] SupplyOrder supplyOrder)
        {
            if (id != supplyOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplyOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplyOrderExists(supplyOrder.Id))
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
            return View(supplyOrder);
        }

        // GET: SupplyOrders/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplyOrder = await _context.tbSupplyOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplyOrder == null)
            {
                return NotFound();
            }

            return View(supplyOrder);
        }

        // POST: SupplyOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var supplyOrder = await _context.tbSupplyOrders.FindAsync(id);
            _context.tbSupplyOrders.Remove(supplyOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplyOrderExists(long id)
        {
            return _context.tbSupplyOrders.Any(e => e.Id == id);
        }
    }
}
