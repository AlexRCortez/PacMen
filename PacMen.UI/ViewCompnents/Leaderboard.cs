using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using PacMen.BL;
using PacMen.BL.Models;
using PacMen.Utility;

namespace PacMen.UI.ViewCompnents
{
    public class Leaderboard : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var apiClient = new ApiClient("https://localhost:7005/api/");
            var entities = apiClient.GetList<Score>(typeof(Score).Name);
            return View(entities);
        }
    }
}
