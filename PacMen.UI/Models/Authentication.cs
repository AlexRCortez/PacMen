using PacMen.BL.Models;
using PacMen.UI.Extensions;

namespace PacMen.UI.Models
{
    public class Authentication
    {
        public static bool IsAuthenticated(HttpContext context)
        {
            if (context.Session.GetObject<User>("user") != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
