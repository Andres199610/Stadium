using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Stadiums.Shared.Entities
{
    public  class Goal
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        public int Telefono { get; set; }

        public Record Record { get; set; } = null!;

        public int RecordId { get; set; }

        public Ticket Ticket { get; set; } = null!;

        public int TicketId { get; set; }

    }
}
