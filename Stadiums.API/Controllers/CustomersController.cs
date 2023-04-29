using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stadiums.API.Data;
using Stadiums.API.Helpers;
using Stadiums.Shared.DTOs;
using Stadiums.Shared.Entities;

namespace Stadiums.API.Controllers
{
    [ApiController]
    [Route("/api/customers")]
    public class CustomersController: ControllerBase
    {
        private readonly DataContext _context;
        private readonly IFileStorage _fileStorage;

        public CustomersController(DataContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.customers
                .Include(x => x.TicketId)

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
            var queryable = _context.customers
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
            var customer = await _context.customers
                .Include(x => x.TicketId)

                .FirstOrDefaultAsync(x => x.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(CustomerDTO customerDTO)
        {
            try
            {
                Customer newTicket = new()
                {
                    Name = customerDTO.Name,
                    phone = customerDTO.phone,
                    address = customerDTO.address,
                   

                    
                   
                };



                _context.Add(newTicket);
                await _context.SaveChangesAsync();
                return Ok(customerDTO);
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
        public async Task<ActionResult> PutAsync(Customer customer)
        {
            try
            {
                _context.Update(customer);
                await _context.SaveChangesAsync();
                return Ok(customer);
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
            var customer = await _context.customers.FirstOrDefaultAsync(x => x.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Remove(customer);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }


}
