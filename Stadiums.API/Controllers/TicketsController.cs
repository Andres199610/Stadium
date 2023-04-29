using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stadiums.API.Data;
using Stadiums.Shared.Entities;

namespace Stadiums.API.Controllers
{
    [ApiController]
    [Route("/api/tickets")]

    public class TicketsController: ControllerBase
    {
        private readonly DataContext _context;

        public TicketsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _context.Tickets.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Post(Ticket ticket)
        {
            _context.Add(ticket);
            await _context.SaveChangesAsync();
            return Ok(ticket);
        }
    }



}

