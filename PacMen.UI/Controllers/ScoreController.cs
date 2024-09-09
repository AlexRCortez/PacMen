using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PacMen.BL;
using PacMen.BL.Models;
using PacMen.PL.Data;
using PacMen.UI.Extensions;
using PacMen.UI.Models;

namespace PacMen.UI.Controllers
{
    public class ScoreController : Controller
    {
        private readonly DbContextOptions<PacMenEntities> options;

        public ScoreController(ILogger<PacMenEntities> logger,
                               DbContextOptions<PacMenEntities> options)
        {
            this.options = options;

        }

        public IActionResult Index()
        {
            return View(new ScoreManager(options).Load());
        }

        public IActionResult Create()
        {
            ViewBag.Title = "Create Score";
            return View();
        }

        [HttpPost]
        public IActionResult Create(Score Score)
        {
            try
            {
                int result = new ScoreManager(options).Insert(Score); // Insert the Score in DB

                return RedirectToAction(nameof(Index)); // Redirect to Index after creating a Score
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Create Score";
                ViewBag.Error = ex.Message;
                return View(Score);
            }
        }

        public IActionResult Edit(Guid id)
        {
            try
            {
                //Score Score = new ScoreManager(options).LoadById(id);
                //ViewBag.Title = "Edit Score";

                if (Authentication.IsAuthenticated(HttpContext))
                {

                    var item = new ScoreManager(options).LoadById(id);
                    ViewBag.Title = "Edit";
                    return View(item);
                }
                else
                    return RedirectToAction("Login", "Score", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) }); // Still need to add "Login" 


            }
            catch (Exception ex)
            {
                ViewBag.Title = "Edit Score";
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(Score Score)
        {
            try
            {
                int result = new ScoreManager(options).Update(Score);

                return RedirectToAction(nameof(Index)); // Redirect to Order Index after editing a Score
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Edit Score";
                ViewBag.Error = ex.Message;
                return View(Score);
            }
        }

        public IActionResult Delete(Guid id)
        {
            if (Authentication.IsAuthenticated(HttpContext))
            {
                var item = new ScoreManager(options).LoadById(id);
                ViewBag.Title = "Delete";
                return View(item);
            }
            else
            {
                return RedirectToAction("Login", "Score", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }
        [HttpPost]
        public IActionResult Delete(Guid id, Score Score, bool rollback = false)
        {
            try
            {
                int result = new ScoreManager(options).Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View();
            }
        }

    }
}
