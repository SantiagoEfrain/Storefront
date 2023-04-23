using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Storefront.DATA.EF.Models;

namespace Storefront.UI.MVC.Controllers
{
    public class ModelScalesController : Controller
    {
        private readonly Gunpla_StoreFrontContext _context;

        public ModelScalesController(Gunpla_StoreFrontContext context)
        {
            _context = context;
        }

        // GET: ModelScales
        public async Task<IActionResult> Index()
        {
              return _context.ModelScales != null ? 
                          View(await _context.ModelScales.ToListAsync()) :
                          Problem("Entity set 'Gunpla_StoreFrontContext.ModelScales'  is null.");
        }

        // GET: ModelScales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ModelScales == null)
            {
                return NotFound();
            }

            var modelScale = await _context.ModelScales
                .FirstOrDefaultAsync(m => m.ScaleId == id);
            if (modelScale == null)
            {
                return NotFound();
            }

            return View(modelScale);
        }

        // GET: ModelScales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ModelScales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScaleId,ScaleSize")] ModelScale modelScale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modelScale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(modelScale);
        }

        // GET: ModelScales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ModelScales == null)
            {
                return NotFound();
            }

            var modelScale = await _context.ModelScales.FindAsync(id);
            if (modelScale == null)
            {
                return NotFound();
            }
            return View(modelScale);
        }

        // POST: ModelScales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScaleId,ScaleSize")] ModelScale modelScale)
        {
            if (id != modelScale.ScaleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modelScale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelScaleExists(modelScale.ScaleId))
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
            return View(modelScale);
        }

        // GET: ModelScales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ModelScales == null)
            {
                return NotFound();
            }

            var modelScale = await _context.ModelScales
                .FirstOrDefaultAsync(m => m.ScaleId == id);
            if (modelScale == null)
            {
                return NotFound();
            }

            return View(modelScale);
        }

        // POST: ModelScales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ModelScales == null)
            {
                return Problem("Entity set 'Gunpla_StoreFrontContext.ModelScales'  is null.");
            }
            var modelScale = await _context.ModelScales.FindAsync(id);
            if (modelScale != null)
            {
                _context.ModelScales.Remove(modelScale);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModelScaleExists(int id)
        {
          return (_context.ModelScales?.Any(e => e.ScaleId == id)).GetValueOrDefault();
        }
    }
}
