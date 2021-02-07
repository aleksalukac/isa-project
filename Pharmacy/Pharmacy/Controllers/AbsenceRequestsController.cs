using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Data;
using Pharmacy.Models.Entities;

namespace Pharmacy.Controllers
{
    public class AbsenceRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AbsenceRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AbsenceRequests
        public async Task<IActionResult> Index()
        {
            return View(await _context.tbAbsenceRequests.ToListAsync());
        }

        // GET: AbsenceRequests/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absenceRequest = await _context.tbAbsenceRequests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (absenceRequest == null)
            {
                return NotFound();
            }

            return View(absenceRequest);
        }

        // GET: AbsenceRequests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AbsenceRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,StartDateTime,EndDateTime,PharmacyAdministratorId")] AbsenceRequest absenceRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(absenceRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(absenceRequest);
        }

        // GET: AbsenceRequests/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absenceRequest = await _context.tbAbsenceRequests.FindAsync(id);
            if (absenceRequest == null)
            {
                return NotFound();
            }
            return View(absenceRequest);
        }

        // POST: AbsenceRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,EmployeeId,StartDateTime,EndDateTime,PharmacyAdministratorId")] AbsenceRequest absenceRequest)
        {
            if (id != absenceRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(absenceRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbsenceRequestExists(absenceRequest.Id))
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
            return View(absenceRequest);
        }

        // GET: AbsenceRequests/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absenceRequest = await _context.tbAbsenceRequests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (absenceRequest == null)
            {
                return NotFound();
            }

            return View(absenceRequest);
        }

        // POST: AbsenceRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var absenceRequest = await _context.tbAbsenceRequests.FindAsync(id);
            _context.tbAbsenceRequests.Remove(absenceRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbsenceRequestExists(long id)
        {
            return _context.tbAbsenceRequests.Any(e => e.Id == id);
        }
    }
}
