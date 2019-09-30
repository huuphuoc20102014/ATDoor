using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AT_Door.Efs.Context;
using AtDoor.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AT_Door.Controllers
{
    public class CardDoorController : AtBaseController
    {
        private readonly AtDoorContext _context;

        public CardDoorController(AtDoorContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.CardDoor.Where(p => p.Status == (int)AtRowStatus.Normal).ToListAsync());
        }
    }
}