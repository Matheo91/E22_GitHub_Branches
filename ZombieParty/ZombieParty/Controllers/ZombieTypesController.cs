using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZombieParty_DataAccess.Data;
using ZombieParty_Models;

namespace ZombieParty.Controllers
{
    public class ZombieTypesController : Controller
    {
        private readonly ZombiePartyDbContext _context;

        public ZombieTypesController(ZombiePartyDbContext context)
        {
            _context = context;
        }

        // GET: ZombieTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ZombieType.ToListAsync());
        }

        // GET: ZombieTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zombieType = await _context.ZombieType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zombieType == null)
            {
                return NotFound();
            }

            return View(zombieType);
        }

        // GET: ZombieTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ZombieTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeName")] ZombieType zombieType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zombieType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zombieType);
        }

        // GET: ZombieTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zombieType = await _context.ZombieType.FindAsync(id);
            if (zombieType == null)
            {
                return NotFound();
            }
            return View(zombieType);
        }

        // POST: ZombieTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeName")] ZombieType zombieType)
        {
            if (id != zombieType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zombieType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZombieTypeExists(zombieType.Id))
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
            return View(zombieType);
        }

        // GET: ZombieTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zombieType = await _context.ZombieType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zombieType == null)
            {
                return NotFound();
            }

            return View(zombieType);
        }

        // POST: ZombieTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zombieType = await _context.ZombieType.FindAsync(id);
            _context.ZombieType.Remove(zombieType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZombieTypeExists(int id)
        {
            return _context.ZombieType.Any(e => e.Id == id);
        }
    }
}
