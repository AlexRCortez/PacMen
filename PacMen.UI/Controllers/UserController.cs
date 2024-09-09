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
    public class UserController : Controller
    {
        private readonly DbContextOptions<PacMenEntities> options;

        public UserController(ILogger<PacMenEntities> logger,
                                DbContextOptions<PacMenEntities> options)
        {
            this.options = options;

        }

        public IActionResult Index()
        {
            return View(new UserManager(options).Load());
        }


        public IActionResult Seed()
        {
            new UserManager(options).Seed();
            return View();
        }

        private void SetUser(User user)
        {

            HttpContext.Session.SetObject("user", user);
            if (user != null)
            {
                HttpContext.Session.SetObject("fullname", "Welcome " + user.FullName);
            }
            else
            {
                HttpContext.Session.SetObject("fullname", string.Empty);
            }
        }
        public IActionResult Logout()
        {
            ViewBag.Title = "Logout";
            SetUser(null);
            return View();
        }

        public IActionResult Login(string returnUrl)
        {
            TempData["returnUrl"] = returnUrl;
            ViewBag.Title = "Login";
            return View();
        }


        [HttpPost]
        public IActionResult Login(User user)
        {
            try
            {
                bool result = new UserManager(options).Login(user);
                SetUser(user);

                if (TempData["returnUrl"] != null)
                {
                    return Redirect(TempData["returnUrl"]?.ToString());
                }

                return RedirectToAction(nameof(Index), "Home"); // ALWAYS Change default re-derict page 
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Login";
                ViewBag.Error = ex.Message;
                throw;
            }
        }
        public IActionResult Create()
        {
            ViewBag.Title = "Create User";
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            try
            {
                int result = new UserManager(options).Insert(user); // Insert the user in DB
                SetUser(user); // LogIn with new user 

                return RedirectToAction(nameof(Index)); // Redirect to Index after creating a user
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Create User";
                ViewBag.Error = ex.Message;
                return View(user);
            }
        }

        public IActionResult Edit(Guid id)
        {
            try
            {
                //User user = new UserManager(options).LoadById(id);
                //ViewBag.Title = "Edit User";

                if (Authentication.IsAuthenticated(HttpContext))
                {

                    var item = new UserManager(options).LoadById(id);
                    ViewBag.Title = "Edit";
                    return View(item);
                }
                else
                    return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) }); // Still need to add "Login" 


            }
            catch (Exception ex)
            {
                ViewBag.Title = "Edit User";
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            try
            {
                int result = new UserManager(options).Update(user);
                SetUser(user);

                return RedirectToAction(nameof(Index)); // Redirect to Order Index after editing a user
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Edit User";
                ViewBag.Error = ex.Message;
                return View(user);
            }
        }

        public IActionResult Delete(Guid id)
        {
            if (Authentication.IsAuthenticated(HttpContext))
            {
                var item = new UserManager(options).LoadById(id);
                ViewBag.Title = "Delete";
                return View(item);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }
        [HttpPost]
        public IActionResult Delete(Guid id, User user, bool rollback = false)
        {
            try
            {
                int result = new UserManager(options).Delete(id, rollback);
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
