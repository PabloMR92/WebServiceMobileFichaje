using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceMobileFichaje.Models
{
    public class LoginUsuarioResult
    {
        public int Result { get; set; }
        public int UsuarioID { get; set; }
        public int GrupoID { get; set; }
        public int TemaID { get; set; }
        public bool Habilitado { get; set; }
        public bool ClaveCaducada { get; set; }
        public string Dominio { get; set; }
    }
}