using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Storefront.DATA.EF.Models;
using System.Drawing;
using Storefront.UI.MVC.Utilities;

namespace Storefront.UI.MVC.Controllers
{
    public class MobileSuitsController : Controller
    {
        private readonly Gunpla_StoreFrontContext _context;
        //added prop below for access to the wwwroot folder
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MobileSuitsController(Gunpla_StoreFrontContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;//added
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
        public async Task<IActionResult> Create([Bind("ModelId,Name,Description,CategoryId,UniverseId,Price,ScaleId,StockStatusId,StockAmount,Msimage,Image")] MobileSuit mobileSuit)
        {
            if (ModelState.IsValid)
            {

                #region File Upload - CREATE
                //Check to see if a file was uploaded
                if (mobileSuit.Image != null)
                {
                    //Check the file type 
                    //- retrieve the extension of the uploaded file
                    string ext = Path.GetExtension(mobileSuit.Image.FileName);

                    //- Create a list of valid extensions to check against
                    string[] validExts = { ".jpeg", ".jpg", ".gif", ".png" };

                    //- verify the uploaded file has an extension matching one of the extensions in the list above
                    //- AND verify file size will work with our .NET app
                    if (validExts.Contains(ext.ToLower()) && mobileSuit.Image.Length < 4_194_303)//underscores don't change the number, they just make it easier to read
                    {
                        //Generate a unique filename
                        mobileSuit.Msimage = Guid.NewGuid() + ext;

                        //Save the file to the web server (here, saving to wwwroot/images)
                        //To access wwwroot, add a property to the controller for the _webHostEnvironment (see the top of this class for our example)
                        //Retrieve the path to wwwroot
                        string webRootPath = _webHostEnvironment.WebRootPath;
                        //variable for the full image path --> this is where we will save the image
                        string fullImagePath = webRootPath + "/assets/images/";

                        //Create a MemoryStream to read the image into the server memory
                        using (var memoryStream = new MemoryStream())
                        {
                            await mobileSuit.Image.CopyToAsync(memoryStream);//transfer file from the request to server memory
                            using (var img = Image.FromStream(memoryStream))//add a using statement for the Image class (using System.Drawing)
                            {
                                //now, send the image to the ImageUtility for resizing and thumbnail creation
                                //items needed for the ImageUtility.ResizeImage()
                                //1) (int) maximum image size
                                //2) (int) maximum thumbnail image size
                                //3) (string) full path where the file will be saved
                                //4) (Image) an image
                                //5) (string) filename
                                int maxImageSize = 500;//in pixels
                                int maxThumbSize = 100;

                                ImageUtility.ResizeImage(fullImagePath, mobileSuit.Msimage, img, maxImageSize, maxThumbSize);
                                //myFile.Save("path/to/folder", "filename"); - how to save something that's NOT an image

                            }
                        }
                    }
                }
                else
                {
                    //If no image was uploaded, assign a default filename
                    //Will also need to download a default image and name it 'noimage.png' -> copy it to the /images folder
                    mobileSuit.Msimage = "noimage.png";
                }

                #endregion


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
        public async Task<IActionResult> Edit(int id, [Bind("ModelId,Name,Description,CategoryId,UniverseId,Price,ScaleId,StockStatusId,StockAmount,Msimage,Image")] MobileSuit mobileSuit)
        {
            if (id != mobileSuit.ModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                #region EDIT File Upload
                //retain old image file name so we can delete if a new file was uploaded
                string oldImageName = mobileSuit.Msimage;

                //Check if the user uploaded a file
                if (mobileSuit.Image != null)
                {
                    //get the file's extension
                    string ext = Path.GetExtension(mobileSuit.Image.FileName);

                    //list valid extensions
                    string[] validExts = { ".jpeg", ".jpg", ".png", ".gif" };

                    //check the file's extension against the list of valid extensions
                    if (validExts.Contains(ext.ToLower()) && mobileSuit.Image.Length < 4_194_303)
                    {
                        //generate a unique file name
                        mobileSuit.Msimage = Guid.NewGuid() + ext;
                        //build our file path to save the image
                        string webRootPath = _webHostEnvironment.WebRootPath;
                        string fullPath = webRootPath + "/assets/images/";

                        //Delete the old image
                        if (oldImageName != "noimage.png")
                        {
                            ImageUtility.Delete(fullPath, oldImageName);
                        }

                        //Save the new image to webroot
                        using (var memoryStream = new MemoryStream())
                        {
                            await mobileSuit.Image.CopyToAsync(memoryStream);
                            using (var img = Image.FromStream(memoryStream))
                            {
                                int maxImageSize = 500;
                                int maxThumbSize = 100;
                                ImageUtility.ResizeImage(fullPath, mobileSuit.Msimage, img, maxImageSize, maxThumbSize);
                            }
                        }

                    }
                }
                #endregion

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
