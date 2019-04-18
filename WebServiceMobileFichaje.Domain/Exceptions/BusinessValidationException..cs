using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceMobileFichaje.Domain.Exceptions
{
    public class BusinessValidationException : Exception
    {
        public BusinessValidationException(string msg) : base(msg) { }
    }
}