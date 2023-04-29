using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stadiums.API.Data;
using Stadiums.API.Helpers;
using Stadiums.Shared.DTOs;
using Stadiums.Shared.Entities;

namespace Stadiums.API.Controllers
{
    [ApiController]
    [Route("/api/goals")]
    public class GoalsController: ControllerBase
    {
        private readonly DataContext _context;
        private readonly IFileStorage _fileStorage;

        public GoalsController(DataContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Goals
                .Include(x => x.Record)
                
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                .OrderBy(x => x.Name)
                .Paginate(pagination)
                .ToListAsync());
        }


        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Goals
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var ticket = await _context.Goals
                .Include(x => x.Record)
                
                .FirstOrDefaultAsync(x => x.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(GoalDTO GoalDTO)
        {
            try
            {
                Goal newTicket = new()
                {
                    Name = GoalDTO.Name,
                    Telefono = GoalDTO.Telefono,

                   
                   
                };



                _context.Add(newTicket);
                await _context.SaveChangesAsync();
                return Ok(GoalDTO);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una ciudad con el mismo nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(Goal goal)
        {
            try
            {
                _context.Update(goal);
                await _context.SaveChangesAsync();
                return Ok(goal);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un producto con el mismo nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var goal = await _context.Goals.FirstOrDefaultAsync(x => x.Id == id);
            if (goal == null)
            {
                return NotFound();
            }

            _context.Remove(goal);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}

