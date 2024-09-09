using PacMen.BL.Models;

namespace PacMen.UI.Extensions
{
    public class Authenticate
    {
        public static bool IsAuthenticated(HttpContext context)
        {
            if (context.Session.GetObject<User>("user") != null)
            {
                User user = context.Session.GetObject<User>("user");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
