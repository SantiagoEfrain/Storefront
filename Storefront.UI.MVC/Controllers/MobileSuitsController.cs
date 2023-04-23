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
    public class MobileSuitsController : Controller
    {
        private readonly Gunpla_StoreFrontContext _context;

        public MobileSuitsController(Gunpla_StoreFrontContext context)
        {
            _context = context;
        }

        // GET: MobileSuits
        public async Task<IActionResult> Index()
        {
            var gunpla_StoreFrontContext = _context.MobileSuits.Include(m => m.Category).Include(m => m.Scale).Include(m => m.StockStatus).Include(m => m.Universe);
            return View(await gunpla_StoreFrontContext.ToListAsync());
        }

        // GET: MobileSuits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MobileSuits == null)
            {
                return NotFound();
            }

            var mobileSuit = await _context.MobileSuits
                .Include(m => m.Category)
                .Include(m => m.Scale)
                .Include(m => m.StockStatus)
                .Include(m => m.Universe)
                .FirstOrDefaultAsync(m => m.ModelId == id);
            if (mobileSuit == null)
            {
                return NotFound();
            }

            return View(mobileSuit);
        }

        // GET: MobileSuits/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["ScaleId"] = new SelectList(_context.ModelScales, "ScaleId", "ScaleId");
            ViewData["StockStatusId"] = new SelectList(_context.StockStatuses, "StockStatusId", "StockStatusName");
            ViewData["UniverseId"] = new SelectList(_context.Universes, "UniverseId", "UniverseName");
            return View();
        }

        // POST: MobileSuits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModelId,Name,Description,CategoryId,UniverseId,Price,ScaleId,StockStatusId,StockAmount,Msimage")] MobileSuit mobileSuit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mobileSuit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", mobileSuit.CategoryId);
            ViewData["ScaleId"] = new SelectList(_context.ModelScales, "ScaleId", "ScaleId", mobileSuit.ScaleId);
            ViewData["StockStatusId"] = new SelectList(_context.StockStatuses, "StockStatusId", "StockStatusName", mobileSuit.StockStatusId);
            ViewData["UniverseId"] = new SelectList(_context.Universes, "UniverseId", "UniverseName", mobileSuit.UniverseId);
            return View(mobileSuit);
        }

        // GET: MobileSuits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MobileSuits == null)
            {
                return NotFound();
            }

            var mobileSuit = await _context.MobileSuits.FindAsync(id);
            if (mobileSuit == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", mobileSuit.CategoryId);
            ViewData["ScaleId"] = new SelectList(_context.ModelScales, "ScaleId", "ScaleId", mobileSuit.ScaleId);
            ViewData["StockStatusId"] = new SelectList(_context.StockStatuses, "StockStatusId", "StockStatusName", mobileSuit.StockStatusId);
            ViewData["UniverseId"] = new SelectList(_context.Universes, "UniverseId", "UniverseName", mobileSuit.UniverseId);
            return View(mobileSuit);
        }

        // POST: MobileSuits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModelId,Name,Description,CategoryId,UniverseId,Price,ScaleId,StockStatusId,StockAmount,Msimage")] MobileSuit mobileSuit)
        {
            if (id != mobileSuit.ModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mobileSuit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MobileSuitExists(mobileSuit.ModelId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", mobileSuit.CategoryId);
            ViewData["ScaleId"] = new SelectList(_context.ModelScales, "ScaleId", "ScaleId", mobileSuit.ScaleId);
            ViewData["StockStatusId"] = new SelectList(_context.StockStatuses, "StockStatusId", "StockStatusName", mobileSuit.StockStatusId);
            ViewData["UniverseId"] = new SelectList(_context.Universes, "UniverseId", "UniverseName", mobileSuit.UniverseId);
            return View(mobileSuit);
        }

        // GET: MobileSuits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MobileSuits == null)
            {
                return NotFound();
            }

            var mobileSuit = await _context.MobileSuits
                .Include(m => m.Category)
                .Include(m => m.Scale)
                .Include(m => m.StockStatus)
                .Include(m => m.Universe)
                .FirstOrDefaultAsync(m => m.ModelId == id);
            if (mobileSuit == null)
            {
                return NotFound();
            }

            return View(mobileSuit);
        }

        // POST: MobileSuits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MobileSuits == null)
            {
                return Problem("Entity set 'Gunpla_StoreFrontContext.MobileSuits'  is null.");
            }
            var mobileSuit = await _context.MobileSuits.FindAsync(id);
            if (mobileSuit != null)
            {
                _context.MobileSuits.Remove(mobileSuit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MobileSuitExists(int id)
        {
          return (_context.MobileSuits?.Any(e => e.ModelId == id)).GetValueOrDefault();
        }
    }
}
