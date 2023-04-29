using Stadiums.Shared.Entities;

namespace Stadiums.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckTicketAsync();
            await CheckGoalAsync();
            await CheckRecordAsync();
        }

        private async Task CheckTicketAsync()
        {
            if (!_context.Tickets.Any())
            {
                _context.Tickets.Add(new Ticket { Name = "Concierto" });
                _context.Tickets.Add(new Ticket { Type_purchse = "Tarjeta" });
                _context.Tickets.Add(new Ticket { Price = 1200000 });
                
            }

            await _context.SaveChangesAsync();
        }

        private async Task CheckGoalAsync()
        {
            if (!_context.Goals.Any())
            {
                _context.Goals.Add(new Goal { Name = "Norte" });
                _context.Goals.Add(new Goal { Telefono = 8188282 });
            }

            await _context.SaveChangesAsync();
        }
        private async Task CheckRecordAsync()
        {
            if (!_context.Records.Any())
            {
                _context.Records.Add(new Record { Checkpoint = "Norte" });
                _context.Records.Add(new Record { Use = "SI" });
            }

            await _context.SaveChangesAsync();
        }
    }

}
