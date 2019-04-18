using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceMobileFichaje.Domain.Transfer.Validation
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string ErrorMsg { get; set; }
    }
}