using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Stadiums.Shared.DTOs
{
    public class RecordDTO
    {
        public int Id { get; set; }

        [Display(Name = "Lugar de control")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Checkpoint { get; set; } = null!;

        [Display(Name = "Fecha uso")]
        public DateTime Use_date { get; set; }

        [Display(Name = "Uso")]
        public string Use { get; set; } = null!;
    }
}
