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
    public class PricesController : Controller
    {
        private readonly BusContext _context;

        public PricesController(BusContext context)
        {
            _context = context;
        }

        // GET: Prices
        public async Task<IActionResult> Index()
        {
            var busContext = _context.Price.Include(p => p.Bus).Include(p => p.Type);
            return View(await busContext.ToListAsync());
        }

        // GET: Prices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _context.Price
                .Include(p => p.Bus)
                .Include(p => p.Type)
                .SingleOrDefaultAsync(m => m.PriceId == id);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // GET: Prices/Create
        public IActionResult Create()
        {
            ViewData["BusId"] = new SelectList(_context.Buses, "BusId", "BusId");
            ViewData["TypeId"] = new SelectList(_context.Type, "TypeId", "TypeId");
            return View();
        }

        // POST: Prices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PriceId,BusId,TypeId,Cost")] Price price)
        {
            if (ModelState.IsValid)
            {
                _context.Add(price);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusId"] = new SelectList(_context.Buses, "BusId", "BusId", price.BusId);
            ViewData["TypeId"] = new SelectList(_context.Type, "TypeId", "TypeId", price.TypeId);
            return View(price);
        }

        // GET: Prices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _context.Price.SingleOrDefaultAsync(m => m.PriceId == id);
            if (price == null)
            {
                return NotFound();
            }
            ViewData["BusId"] = new SelectList(_context.Buses, "BusId", "BusId", price.BusId);
            ViewData["TypeId"] = new SelectList(_context.Type, "TypeId", "TypeId", price.TypeId);
            return View(price);
        }

        // POST: Prices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PriceId,BusId,TypeId,Cost")] Price price)
        {
            if (id != price.PriceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(price);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriceExists(price.PriceId))
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
            ViewData["BusId"] = new SelectList(_context.Buses, "BusId", "BusId", price.BusId);
            ViewData["TypeId"] = new SelectList(_context.Type, "TypeId", "TypeId", price.TypeId);
            return View(price);
        }

        // GET: Prices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _context.Price
                .Include(p => p.Bus)
                .Include(p => p.Type)
                .SingleOrDefaultAsync(m => m.PriceId == id);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // POST: Prices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var price = await _context.Price.SingleOrDefaultAsync(m => m.PriceId == id);
            _context.Price.Remove(price);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriceExists(int id)
        {
            return _context.Price.Any(e => e.PriceId == id);
        }
    }
}
