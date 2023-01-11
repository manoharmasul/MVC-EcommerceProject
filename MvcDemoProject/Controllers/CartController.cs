using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcDemoProject.Models;
using MvcDemoProject.Repository;
using MvcDemoProject.Repository.Interface;

namespace MvcDemoProject.Controllers
{

    public class CartController : Controller
    {

        private readonly ICartRepository cartRepository;
        public CartController(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
    }
        // GET: CartController
        public async Task<IActionResult> AddToCart(int id)
        {
            string userid = HttpContext.Session.GetString("userId");

            CartModel cart = new CartModel();
            cart.pId = id;
            cart.uId = Convert.ToInt32(userid);
            cart.createdBy = Convert.ToInt32(userid);
            if (cart.uId == 0)
            {
                return RedirectToAction("Create", "User", new { num = -1 });
            }
            int res = await cartRepository.AddToCart(cart);
            if (res == 1 && cart.uId != 0)
            {

                return RedirectToAction("GetProductsCustomer","Product", new { num = -1 });
            }
            else
            {


                return View();
            }

        }
        [HttpGet]

        public async Task<IActionResult> ViewCart()
        {
            var x = HttpContext.Request.QueryString.Value;

        

            if (x != null && x != "")
            {
                
                ViewBag.y = x;
            }
            string userid = HttpContext.Session.GetString("userId");
            int uid = Int32.Parse(userid);
            var model = await cartRepository.ViewFromCart(uid);

            return View(model);
        }
        public async Task<IActionResult> DeleteItem(int id)
        {
            string userid = HttpContext.Session.GetString("userId");




            int modifiedBy = Convert.ToInt32(userid);

            int res = await cartRepository.RemoveFromCart(id, modifiedBy);
            if (res == 1)
            {
                return RedirectToAction("ViewCart");
            }
            else
            {
                return View();
            }
        }
    }
    
}
