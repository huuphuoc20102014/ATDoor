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
    public class CardsController : Controller
    {
        private readonly AtDoorContext _context;

        public CardsController(AtDoorContext context)
        {
            _context = context;
        }

        // GET: Cards
        public async Task<IActionResult> Index()
        {
            return View(await _context.Card.ToListAsync());
        }

        // GET: Cards/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Card
                .FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // GET: Cards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,Description,FkUserId")] Card card)
        {
            if (ModelState.IsValid)
            {
                card.RowStatus = null;
                card.Status = 0;
                card.CreatedBy = "user";
                card.CreatedDate = DateTime.Now;
                _context.Add(card);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(card);
        }

        // GET: Cards/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dbItem = await _context.Card.AsNoTracking()

                .Where(h => h.Id == id)

                .Select(h => new CardEditViewModel
                {
                    Id = h.Id,
                    Code = h.Code,
                    Name = h.Name,
                    Description = h.Description,
                    FkUserId = h.FkUserId,
                    RowStatus = h.RowStatus,
                })
                .FirstOrDefaultAsync();
            if (dbItem == null)
            {
                return NotFound();
            }
            return View(dbItem);
        }

        // POST: Cards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CardEditViewModel vmItem)
        {
            if (!ModelState.IsValid)
            {
                return View(vmItem);
            }
            var tableName = nameof(Card);
            var tableVersion = await _context.HistoryCard.FirstOrDefaultAsync(h => h.Id == tableName);
            var dbItem = await _context.Card
                .Where(h => h.Id == vmItem.Id)
                .FirstOrDefaultAsync();
            // Update db item               
            dbItem.ModifiedBy = "user";
            dbItem.ModifiedDate = DateTime.Now;
            dbItem.RowStatus = vmItem.RowStatus;

            dbItem.Code = vmItem.Code;
            dbItem.Name = vmItem.Name;
            dbItem.FkUserId = vmItem.FkUserId;
            _context.Entry(dbItem).Property(nameof(Card.RowStatus)).OriginalValue = vmItem.RowStatus;
            // Set time stamp for table to handle concurrency conflict
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = dbItem.Id });
        }

        // GET: Cards/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Card
                .FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var card = await _context.Card.FindAsync(id);
            _context.Card.Remove(card);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardExists(string id)
        {
            return _context.Card.Any(e => e.Id == id);
        }
    }
    public class CardBaseViewModel
    {

        public String Code { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String FkUserId { get; set; }


    }

    public class CardDetailsViewModel : CardBaseViewModel
    {

        public String Id { get; set; }
        public String CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public byte[] RowStatus { get; set; }
        public AtRowStatus Status { get; set; }

    }

    public class CardCreateViewModel : CardBaseViewModel
    {

    }

    public class CardEditViewModel : CardBaseViewModel
    {
        public String Id { get; set; }
        public byte[] RowStatus { get; set; }
    }
}
