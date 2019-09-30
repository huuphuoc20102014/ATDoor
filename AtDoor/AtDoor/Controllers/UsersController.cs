using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AtDoor.Efs.Context;
using AtDoor.Efs.Entities;

namespace AtDoor.Controllers
{
    public class UsersController : AtBaseController
    {
        private readonly AtDoorContext _context;

        public UsersController(AtDoorContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var atDoorContext = _context.Users.Include(u => u.FkCard);
            return View(await atDoorContext.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(u => u.FkCard)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["FkCardId"] = new SelectList(_context.Card, "Id", "Id");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FkCardId,Permission,Description,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,RowStatus,Status")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkCardId"] = new SelectList(_context.Card, "Id", "Id", users.FkCardId);
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dbItem = await _context.Users.AsNoTracking()

                .Where(h => h.Id == id)

                .Select(h => new UsersEditViewModel
                {
                    Id = h.Id,
                    Name = h.Name,
                    FkCardId = h.FkCardId,
                    Permission = h.Permission,
                    Description = h.Description,
                    RowStatus = h.RowStatus,
                })
                .FirstOrDefaultAsync();
            if (dbItem == null)
            {
                return NotFound();
            }
            return View(dbItem);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UsersEditViewModel vmItem)
        {
            if (!ModelState.IsValid)
            {
                return View(vmItem);
            }
            var tableName = nameof(Users);
            var tableVersion = await _context.HistoryCard.FirstOrDefaultAsync(h => h.Id == tableName);
            var dbItem = await _context.Users
                .Where(h => h.Id == vmItem.Id)
                .FirstOrDefaultAsync();
            // Update db item               
            dbItem.ModifiedBy = "user";
            dbItem.ModifiedDate = DateTime.Now;
            dbItem.RowStatus = vmItem.RowStatus;

            dbItem.Name = vmItem.Name;
            dbItem.Permission = vmItem.Permission;
            dbItem.Description = vmItem.Description;
            dbItem.FkCardId = vmItem.FkCardId;
            _context.Entry(dbItem).Property(nameof(Users.RowStatus)).OriginalValue = vmItem.RowStatus;
            // Set time stamp for table to handle concurrency conflict
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = dbItem.Id });
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(u => u.FkCard)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
    public class UsersBaseViewModel
    {
        public String Name { get; set; }
        public String FkCardId { get; set; }
        public int? Permission { get; set; }
        public String Description { get; set; }

    }

    public class UsersDetailsViewModel : UsersBaseViewModel
    {

        public String Id { get; set; }
        public String CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public String ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowStatus { get; set; }
        public AtRowStatus Status { get; set; }

    }

    public class UsersCreateViewModel : UsersBaseViewModel
    {

    }

    public class UsersEditViewModel : UsersBaseViewModel
    {
        public String Id { get; set; }
        public byte[] RowStatus { get; set; }
    }
}
