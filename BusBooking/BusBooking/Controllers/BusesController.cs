using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusBooking.Models;

namespace BusBooking.Controllers
{
    public class BusesController : Controller
    {
        private readonly BusContext _context;

        public BusesController(BusContext context)
        {
            _context = context;
        }

        // GET: Buses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Buses.ToListAsync());
        }

        // GET: Buses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buses = await _context.Buses
                .SingleOrDefaultAsync(m => m.BusId == id);
            if (buses == null)
            {
                return NotFound();
            }

            return View(buses);
        }

        // GET: Buses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Buses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusId,Name,Age")] Buses buses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(buses);
        }

        // GET: Buses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buses = await _context.Buses.SingleOrDefaultAsync(m => m.BusId == id);
            if (buses == null)
            {
                return NotFound();
            }
            return View(buses);
        }

        // POST: Buses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusId,Name,Age")] Buses buses)
        {
            if (id != buses.BusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusesExists(buses.BusId))
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
            return View(buses);
        }

        // GET: Buses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buses = await _context.Buses
                .SingleOrDefaultAsync(m => m.BusId == id);
            if (buses == null)
            {
                return NotFound();
            }

            return View(buses);
        }

        // POST: Buses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buses = await _context.Buses.SingleOrDefaultAsync(m => m.BusId == id);
            _context.Buses.Remove(buses);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BusesExists(int id)
        {
            return _context.Buses.Any(e => e.BusId == id);
        }
    }
}
