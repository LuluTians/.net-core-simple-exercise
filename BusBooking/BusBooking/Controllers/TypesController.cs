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
    public class TypesController : Controller
    {
        private readonly BusContext _context;

        public TypesController(BusContext context)
        {
            _context = context;
        }

        // GET: Types
        public async Task<IActionResult> Index()
        {
            return View(await _context.Type.ToListAsync());
        }

        // GET: Types/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @type = await _context.Type
                .SingleOrDefaultAsync(m => m.TypeId == id);
            if (@type == null)
            {
                return NotFound();
            }

            return View(@type);
        }

        // GET: Types/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Types/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeId,TypeName,Description")] Models.Type @type)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@type);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@type);
        }

        // GET: Types/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @type = await _context.Type.SingleOrDefaultAsync(m => m.TypeId == id);
            if (@type == null)
            {
                return NotFound();
            }
            return View(@type);
        }

        // POST: Types/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TypeId,TypeName,Description")] Models.Type @type)
        {
            if (id != @type.TypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@type);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeExists(@type.TypeId))
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
            return View(@type);
        }

        // GET: Types/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @type = await _context.Type
                .SingleOrDefaultAsync(m => m.TypeId == id);
            if (@type == null)
            {
                return NotFound();
            }

            return View(@type);
        }

        // POST: Types/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var @type = await _context.Type.SingleOrDefaultAsync(m => m.TypeId == id);
            _context.Type.Remove(@type);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeExists(string id)
        {
            return _context.Type.Any(e => e.TypeId == id);
        }
    }
}
