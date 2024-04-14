using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labb1EF.Data;
using Labb1EF.Models;

namespace Labb1EF.Controllers
{
    public class ReasonsController : Controller
    {
        private readonly NitroStoreDbContext _context;

        public ReasonsController(NitroStoreDbContext context)
        {
            _context = context;
        }

        // GET: Reasons
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reasons.ToListAsync());
        }

        // GET: Reasons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reason = await _context.Reasons
                .FirstOrDefaultAsync(m => m.ReasonId == id);
            if (reason == null)
            {
                return NotFound();
            }

            return View(reason);
        }

        // GET: Reasons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reasons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReasonId,ReasonTitle")] Reason reason)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reason);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reason);
        }

        // GET: Reasons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reason = await _context.Reasons.FindAsync(id);
            if (reason == null)
            {
                return NotFound();
            }
            return View(reason);
        }

        // POST: Reasons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReasonId,ReasonTitle")] Reason reason)
        {
            if (id != reason.ReasonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reason);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReasonExists(reason.ReasonId))
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
            return View(reason);
        }

        // GET: Reasons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reason = await _context.Reasons
                .FirstOrDefaultAsync(m => m.ReasonId == id);
            if (reason == null)
            {
                return NotFound();
            }

            return View(reason);
        }

        // POST: Reasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reason = await _context.Reasons.FindAsync(id);
            if (reason != null)
            {
                _context.Reasons.Remove(reason);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReasonExists(int id)
        {
            return _context.Reasons.Any(e => e.ReasonId == id);
        }
    }
}
