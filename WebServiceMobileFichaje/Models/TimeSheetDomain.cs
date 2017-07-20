using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebServiceMobileFichaje.Models
{
    public class TimeSheetDomain
    {
        [Key]
        public int TimeSheetDomainID { get; set; }
        public string Domain { get; set; }
        public string Descripcion { get; set; }
    }
}