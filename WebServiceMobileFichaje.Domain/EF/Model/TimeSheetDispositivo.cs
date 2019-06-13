using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebServiceMobileFichaje.Domain.EF.Model
{
    public class TimeSheetDispositivo
    {
        [Key]
        public int TimeSheetDispositivoID { get; set; }
        public int TimeSheetLocacionID { get; set; }
        public virtual TimeSheetLocacion Locaciones { get; set; }
        public int IntervaloFichado { get; set; }
    }
}