using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceMobileFichaje.Domain.Transfer.Business
{
    public class InOutUserReportRequest
    {
        public int ScheduleTypeId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int LocationId { get; set; }
    }
}