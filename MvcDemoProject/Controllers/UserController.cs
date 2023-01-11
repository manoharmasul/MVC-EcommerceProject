using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcDemoProject.Models;
using MvcDemoProject.Repository.Interface;

namespace MvcDemoProject.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        // GET: UserController
        public async Task<ActionResult> GetAllUsers()
        {
            var userlist = await userRepository.GetAllUsers();

            return View(userlist);
        }

        // GET: UserController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var x = HttpContext.Request.QueryString.Value;
            if (x != null && x != "")
            {
                ViewBag.y = x;
            }

            var uId = HttpContext.Session.GetString("userId");

            var user = await userRepository.GetById(Int32.Parse(uId));

            return View(user);
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(logUserModel ur)
        {
            var user = await userRepository.UserlogIn(ur);
            if (user != null)
            {
                if (user.password == ur.password)
                {
                    HttpContext.Session.SetString("userName", user.userName);
                    HttpContext.Session.SetString("userId", user.Id.ToString());
                    HttpContext.Session.SetString("userRole", user.role);
                    ViewBag.user = user.userName;

                    if (user.role == "Admin")
                        return RedirectToAction("Index", "Order");
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                    ViewBag.num = 1;
                return View();
            }
            else
            {
                ViewBag.num = 1;
                return View();
            }
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            var x = HttpContext.Request.QueryString.Value;
            if (x != null && x != "")
            {
                ViewBag.x = -1;
            }

            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UsertRegistrationModel userr)
        {
            try
            {
                var user = await userRepository.AddNewUser(userr);
                if (user > 0)
                {

                    return RedirectToAction(nameof(Login));
                }
                else
                    ViewBag.num = 1;

                return View();

            }
            catch
            {
                ViewBag.num = 1;

                return View();
            }
        }

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {


            var user = await userRepository.GetById(id);

            return View(user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(User user)
        {
            try
            {
                var uId = HttpContext.Session.GetString("userId");
                user.Id = Int32.Parse(uId);
                user.modifiedBy = Int32.Parse(uId);

                var result = await userRepository.UpdateUser(user);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Details), new { num = -11 });
                }
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var result = await userRepository.GetById(id);
            return View(result);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await userRepository.DeleteUser(id);
                if (result > 0)
                {
                    return RedirectToAction(nameof(GetAllUsers));
                }
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult ErrorMasaage()
        {

            return View();
        }
        public ActionResult Incorrectdata()
        {

            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LogIn");
        }
        public ActionResult AddMoney()
        {

            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddMoney(UsertRegistrationModel userr)
        {
            try
            {
                var uId = HttpContext.Session.GetString("userId");
                int Id = Int32.Parse(uId);
                var user = await userRepository.AddMoney(userr, Id);
                if (user > 0)
                {

                    return RedirectToAction(nameof(GetWallet), new { num1 = -11 });
                }
                else
                    ViewBag.num = 1;

                return View();

            }
            catch
            {
                ViewBag.num = 1;

                return View();
            }
        }

        public async Task<ActionResult> GetWallet(int id)
        {
            var x = HttpContext.Request.QueryString.Value;
            if (x != null && x != "")
            {
                ViewBag.x = x;

            }

            var uId = HttpContext.Session.GetString("userId");

            var user = await userRepository.GetWallet(Int32.Parse(uId));

            return View(user);
        }
    }
}
