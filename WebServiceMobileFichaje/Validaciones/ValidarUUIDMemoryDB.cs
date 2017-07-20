using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using WebServiceMobileFichaje.Services;

namespace WebServiceMobileFichaje.Validaciones
{
    public class ValidaCionUUIDMemoryDB : IValidacionUUID
    {
        private readonly TimeSheetUsuarioService _service;

        public ValidaCionUUIDMemoryDB(TimeSheetUsuarioService service)
        {
            _service = service;
        }

        public bool UUIDEsValido(string uuid)
        {
            if (uuid == null || uuid == "")
                return false;
            if (!ExisteEnCache(uuid))
            {
                if (ExisteEnDB(uuid))
                {
                    AgregarACache(uuid);
                    return true;
                }
                else
                    return false;
            }
            else return true;
        }
        
        private bool ExisteEnCache(string UUID)
        {
            ObjectCache cache = MemoryCache.Default;
            var usuario = cache.Get(UUID) as string;
            return usuario != null;
        }

        private bool ExisteEnDB(string UUID)
        {            
            return _service.ExisteUUID(UUID);
        }

        private void AgregarACache(string UUID)
        {
            ObjectCache cache = MemoryCache.Default;
            cache.Add(UUID, UUID, DateTimeOffset.UtcNow.AddMinutes(30));
        }
    }
}