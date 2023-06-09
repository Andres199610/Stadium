﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Stadiums.Shared.Entities
{
    public class Ticket
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Tipo de compra")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Type_purchse{ get; set; } = null!;

        public  DateTime Date { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]

        public decimal Price { get; set; }

        public ICollection<Goal>? Goals { get; set; }

        [Display(Name = "Porteria")]
        public int GoalsNumber => Goals == null ? 0 : Goals.Count;

        public ICollection<Customer>? Customers { get; set; }

        [Display(Name = "Clientes")]
        public int CustomersNumber => Customers == null ? 0 : Customers.Count;


    }
}
