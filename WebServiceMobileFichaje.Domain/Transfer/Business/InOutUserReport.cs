using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceMobileFichaje.Domain.Transfer.Business
{
    public class InOutUserReport
    {
        public string Description { get; set; }
        public string Date { get; set; }
        public string In { get; set; }
        public string Out { get; set; }
        public int Total { get; set; }
    }
}