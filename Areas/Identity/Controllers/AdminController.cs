using FormIOProject.Areas.Identity.Data;
using FormIOProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormIOProject.Areas.Identity.Controllers
{
    [Area("Identity")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Identity/Admin
        public async Task<IActionResult> Index()
        {
            return View(await _context.FormIO.ToListAsync());
        }

        // GET: Identity/Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formIO = await _context.FormIO
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formIO == null)
            {
                return NotFound();
            }

            return View(formIO);
        }

        // GET: Identity/Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Identity/Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,CreatedBy,ModifiedBy,CreatedUtc,ModifiedUtc,VersionId,Latest,FormFields")] FormIO formIO)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formIO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formIO);
        }

        // GET: Identity/Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formIO = await _context.FormIO.FindAsync(id);
            if (formIO == null)
            {
                return NotFound();
            }
            return View(formIO);
        }

        // POST: Identity/Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,CreatedBy,ModifiedBy,CreatedUtc,ModifiedUtc,VersionId,Latest,FormFields")] FormIO formIO)
        {
            if (id != formIO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formIO);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormIOExists(formIO.Id))
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
            return View(formIO);
        }

        // GET: Identity/Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formIO = await _context.FormIO
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formIO == null)
            {
                return NotFound();
            }

            return View(formIO);
        }

        // POST: Identity/Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var formIO = await _context.FormIO.FindAsync(id);
            if (formIO != null)
            {
                _context.FormIO.Remove(formIO);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormIOExists(int id)
        {
            return _context.FormIO.Any(e => e.Id == id);
        }
    }
}
