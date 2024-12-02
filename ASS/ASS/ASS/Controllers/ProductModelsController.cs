using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASS.Data;
using ASS.Models;

namespace ASS.Controllers
{
    public class ProductModelsController : Controller
    {
        private readonly MyDb _context;

        public ProductModelsController(MyDb context)
        {
            _context = context;
        }

        // GET: ProductModels
        public async Task<IActionResult> Index()
        {
            var accountId = HttpContext.Session.GetInt32("AccountId");
            ViewBag.AccountId = accountId;
            var myDb = _context.Products.Include(p => p.PhanLoai).Include(p => p.ThuongHieu);
            return View(await myDb.ToListAsync());
        } 

        // GET: ProductModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var accountId = HttpContext.Session.GetInt32("AccountId");
            ViewBag.AccountId = accountId;
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.Products
                .Include(p => p.PhanLoai)
                .Include(p => p.ThuongHieu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // GET: ProductModels/Create
        public IActionResult Create()
        {
            ViewData["PhanLoaiId"] = new SelectList(_context.Categorys, "Id", "Name");
            ViewData["ThuongHieuId"] = new SelectList(_context.Brands, "Id", "BrandName");
            return View();
        }

        // POST: ProductModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten,SoLuong,Mota,Gia,ManHinh,BoNho,PhanLoai,HeDieuHanh,PhanLoaiId,ThuongHieuId,Image")] Product productModel)
        {
            var accountId = HttpContext.Session.GetInt32("AccountId");
            ViewBag.AccountId = accountId;
            _context.Add(productModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: ProductModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.Products.FindAsync(id);
            if (productModel == null)
            {
                return NotFound();
            }
            ViewData["PhanLoaiId"] = new SelectList(_context.Categorys, "Id", "Name", productModel.PhanLoaiId);
            ViewData["ThuongHieuId"] = new SelectList(_context.Brands, "Id", "BrandName", productModel.ThuongHieuId);
            return View(productModel);
        }

        // POST: ProductModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ten,SoLuong,Mota,Gia,ManHinh,BoNho,PhanLoai,HeDieuHanh,PhanLoaiId,ThuongHieuId,Image")] Product productModel)
        {
            var accountId = HttpContext.Session.GetInt32("AccountId");
            ViewBag.AccountId = accountId;
            if (id != productModel.Id)
            {
                return NotFound();
            }
            _context.Update(productModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: ProductModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.Products
                .Include(p => p.PhanLoai)
                .Include(p => p.ThuongHieu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productModel == null)
            {
                return NotFound();
            }
            return View(productModel);
        }

        // POST: ProductModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productModel = await _context.Products.FindAsync(id);
            if (productModel != null)
            {
                _context.Products.Remove(productModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductModelExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
