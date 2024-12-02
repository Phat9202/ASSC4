using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bai4.DbContextt;
using Bai4.Models;

namespace Bai4.Controllers
{
    public class CustumersController : Controller
    {
        private readonly MyDb _context;

        public CustumersController(MyDb context)
        {
            _context = context;
        }

        // GET: Custumers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Custumers.ToListAsync());
        }

        // GET: Custumers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var custumer = await _context.Custumers
                .FirstOrDefaultAsync(m => m.id == id);
            if (custumer == null)
            {
                return NotFound();
            }

            return View(custumer);
        }

        // GET: Custumers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Custumers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,Adresss")] Custumer custumer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(custumer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(custumer);
        }

        // GET: Custumers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var custumer = await _context.Custumers.FindAsync(id);
            if (custumer == null)
            {
                return NotFound();
            }
            return View(custumer);
        }

        // POST: Custumers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Adresss")] Custumer custumer)
        {
            if (id != custumer.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(custumer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustumerExists(custumer.id))
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
            return View(custumer);
        }

        // GET: Custumers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var custumer = await _context.Custumers
                .FirstOrDefaultAsync(m => m.id == id);
            if (custumer == null)
            {
                return NotFound();
            }

            return View(custumer);
        }

        // POST: Custumers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var custumer = await _context.Custumers.FindAsync(id);
            if (custumer != null)
            {
                _context.Custumers.Remove(custumer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustumerExists(int id)
        {
            return _context.Custumers.Any(e => e.id == id);
        }
    }
}
