using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceMobileFichaje.Domain.EF.Model
{
    public interface IMultiTenant
    {
        int GrupoID { get; set; }
    }
}