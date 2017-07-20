using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebServiceMobileFichaje.Validaciones;

namespace WebServiceMobileFichaje.Models
{
    [Validator(typeof(GeoLocationValidator))]
    public class GeoLocationTimeSheet
    {
        public decimal CoordenadaX { get; set; }
        public decimal CoordenadaY { get; set; }
        public DateTime Timestamp { get; set; }
        public string UUID { get; set; }
    }
}