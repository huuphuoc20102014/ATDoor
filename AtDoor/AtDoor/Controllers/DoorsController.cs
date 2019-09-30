using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AtDoor.Efs.Context;
using AtDoor.Efs.Entities;
using AtECommerce.Controllers;

namespace AtDoor.Controllers
{
    public class DoorsController : Controller
    {
        private readonly AtDoorContext _context;

        public DoorsController(AtDoorContext context)
        {
            _context = context;
        }

        // GET: Doors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Door.ToListAsync());
        }

        // GET: Doors/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var door = await _context.Door
                .FirstOrDefaultAsync(m => m.Id == id);
            if (door == null)
            {
                return NotFound();
            }

            return View(door);
        }

        // GET: Doors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,Description,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,RowStatus,Status")] Door door)
        {
            if (ModelState.IsValid)
            {
                _context.Add(door);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(door);
        }

        // GET: Doors/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dbItem = await _context.Door.AsNoTracking()

                .Where(h => h.Id == id)

                .Select(h => new DoorEditViewModel
                {
                    Id = h.Id,
                    Code = h.Code,
                    Name = h.Name,
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

        // POST: Doors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DoorEditViewModel vmItem)
        {
            if (!ModelState.IsValid)
            {
                return View(vmItem);
            }
            var tableName = nameof(Door);
            var tableVersion = await _context.HistoryDoor.FirstOrDefaultAsync(h => h.Id == tableName);
            var dbItem = await _context.Door
                .Where(h => h.Id == vmItem.Id)
                .FirstOrDefaultAsync();
            // Update db item               
            dbItem.ModifiedBy = "user";
            dbItem.ModifiedDate = DateTime.Now;
            dbItem.RowStatus = vmItem.RowStatus;

            dbItem.Code = vmItem.Code;
            dbItem.Name = vmItem.Name;
            _context.Entry(dbItem).Property(nameof(Door.RowStatus)).OriginalValue = vmItem.RowStatus;
            // Set time stamp for table to handle concurrency conflict
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = dbItem.Id });
        }

        // GET: Doors/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var door = await _context.Door
                .FirstOrDefaultAsync(m => m.Id == id);
            if (door == null)
            {
                return NotFound();
            }

            return View(door);
        }

        // POST: Doors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var door = await _context.Door.FindAsync(id);
            _context.Door.Remove(door);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoorExists(string id)
        {
            return _context.Door.Any(e => e.Id == id);
        }
    }
    public class DoorBaseViewModel
    {

        public String Code { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }


    }

    public class DoorDetailsViewModel : DoorBaseViewModel
    {

        public String Id { get; set; }
        public String CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowStatus { get; set; }
        public AtRowStatus status { get; set; }

    }

    public class DoorCreateViewModel : DoorBaseViewModel
    {

    }

    public class DoorEditViewModel : DoorBaseViewModel
    {

        public String Id { get; set; }
        public byte[] RowStatus { get; set; }
    }
}
