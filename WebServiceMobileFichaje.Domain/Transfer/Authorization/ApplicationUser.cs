using System.Security.Claims;
using WebServiceMobileFichaje.Domain.Utils;

namespace WebServiceMobileFichaje.Domain.Transfer.Authorization
{
    public class ApplicationUser
    {
        public string UUID { get; set; }
        public int UserId { get; set; }
        public int GrupoId { get; set; }
        public string LogIn { get; set; }

        public bool IsUserValid()
        {
            return !string.IsNullOrEmpty(UUID) && UserId != 0;
        }

        public static ApplicationUser GetCurrent()
        {
            return ((ClaimsIdentity)System.Threading.Thread.CurrentPrincipal.Identity).GetUser();
        }
    }
}