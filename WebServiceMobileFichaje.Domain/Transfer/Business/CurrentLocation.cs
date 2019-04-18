using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebServiceMobileFichaje.Domain.Transfer.Business
{
    public class CurrentLocation
    {
        [Required]
        public decimal CoordenadaX { get; set; }
        [Required]
        public decimal CoordenadaY { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }
    }
}