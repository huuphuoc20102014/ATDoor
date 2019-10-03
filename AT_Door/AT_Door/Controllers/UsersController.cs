using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AT_Door.Efs.Context;
using AT_Door.Efs.Entities;
using AtDoor.Controllers;

namespace AT_Door.Controllers
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
            return View(await _context.Users.Where(p => p.Status == (int)AtRowStatus.Normal).ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
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
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateViewModel vmItem)
        {
            // Invalid model
            if (!ModelState.IsValid)
            {
                return View(vmItem);
            }
            // Get time stamp for table to handle concurrency conflict
            var tableName = nameof(Door);
            var tableVersion = await _context.HistoryDoor.FirstOrDefaultAsync(h => h.Id == tableName);
            // Check code is existed
            if (await _context.Door.AnyAsync(h => h.Code == vmItem.Code))
            {
                ModelState.AddModelError(nameof(Door.Code), "Mã đã tồn tại.");
                return View(vmItem);
            }
            // Create save db item
            var dbItem = new Door
            {
                Id = Guid.NewGuid().ToString(),

                CreatedBy = _loginUserId,
                CreatedDate = DateTime.Now,
                ModifiedBy = null,
                ModifiedDate = null,
                Status = (int)AtRowStatus.Normal,
                RowStatus = null,

                Code = vmItem.Code,
                Name = vmItem.Name,
                Description = vmItem.Description,

            };
            _context.Add(dbItem);
            // Set time stamp for table to handle concurrency conflict
            //tableVersion.Action = 0;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = dbItem.Id });
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
                .Select(h => new UserEditViewModel
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

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel vmItem)
        {
            if (!ModelState.IsValid)
            {
                return View(vmItem);
            }
            var tableName = nameof(Users);
            var tableVersion = await _context.HistoryDoor.FirstOrDefaultAsync(h => h.Id == tableName);
            var dbItem = await _context.Door
                .Where(h => h.Id == vmItem.Id)
                .FirstOrDefaultAsync();
            // Update db item               
            dbItem.ModifiedBy = _loginUserId;
            dbItem.ModifiedDate = DateTime.Now;
            dbItem.RowStatus = vmItem.RowStatus;

            dbItem.Code = vmItem.Code;
            dbItem.Name = vmItem.Name;
            _context.Entry(dbItem).Property(nameof(Door.RowStatus)).OriginalValue = vmItem.RowStatus;
            // Set time stamp for table to handle concurrency conflict
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = dbItem.Id });
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dbItem = await _context.Users.AsNoTracking()
                .Where(h => h.Id == id)
                .FirstOrDefaultAsync();
            if (dbItem == null)
            {
                return NotFound();
            }

            return View(dbItem);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, byte[] rowStatus)
        {
            var dbItem = await _context.Users.FindAsync(id);
            if (rowStatus == null)
            {
                ModelState.AddModelError("RowVersion", "Phiên bản hàng không hợp lệ, vui lòng thử lại.");
                return View(dbItem);
            }
            // Update db item               
            if (dbItem.Status != (int)AtRowStatus.Deleted)
            {
                dbItem.Status = (int)AtRowStatus.Deleted;
                dbItem.ModifiedBy = _loginUserId;
                dbItem.ModifiedDate = DateTime.Now;
                dbItem.RowStatus = rowStatus;

                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index)); ;
        }

        private bool UsersExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }

    public class UserBaseViewModel
    {

        public String Code { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }


    }

    public class UserDetailsViewModel : UserBaseViewModel
    {

        public String Id { get; set; }
        public String CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowStatus { get; set; }
        public AtRowStatus Status { get; set; }


    }

    public class UserCreateViewModel : UserBaseViewModel
    {

    }

    public class UserEditViewModel : UserBaseViewModel
    {

        public String Id { get; set; }
        public byte[] RowStatus { get; set; }
    }
}
