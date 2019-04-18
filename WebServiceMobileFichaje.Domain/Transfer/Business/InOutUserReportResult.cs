using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceMobileFichaje.Domain.Transfer.Business
{
    public class InOutUserReportResult
    {
        public string User { get; set; }
        public string InTotal { get; set; }
        public string OutTotal { get; set; }
        public string Total { get; set; }
        public List<InOutUserReport> Report { get; set; }
    }
}