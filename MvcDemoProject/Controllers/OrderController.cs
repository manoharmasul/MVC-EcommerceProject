using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcDemoProject.Models;
using MvcDemoProject.Repository;

namespace MvcDemoProject.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepositories _odrepo;
        public OrderController(IOrderRepositories odrepo)
        {
            _odrepo = odrepo;
        }



        public async Task<ActionResult> Index()
        {
            var ordList = await _odrepo.GetOrders();
            var uname = HttpContext.Session.GetString("userName");

            //HttpContext.Session.GetString("userName", user.userName);
            ViewBag.un = uname;

            return View(ordList);
        }

        // GET: OrderController1/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var ord = await _odrepo.GetOrderById(id);
            return View(ord);
        }


        public ActionResult AddAddress(int result)
        {

            ViewBag.id = result;
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAddress(string shippingAddress, int Qty, int id)
        {

            var custId = HttpContext.Session.GetString("userId");


            var res = await _odrepo.OrderAddress(shippingAddress, Qty, id, Int32.Parse(custId));
            if (res > 0)
            {


                return RedirectToAction("ViewCart", "Cart", new { num = -2 });
            }
            else if (res == -4)
            {

                return RedirectToAction("ViewCart", "Cart", new { num = -4 });
            }

            else
            {

                return View();
            }

        }



        // GET: OrderController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Order order)
        {
            try
            {
                await _odrepo.CreateOrder(order);



                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController1/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            var ord = await _odrepo.GetOrderById(id);
            return View(ord);
        }

        // POST: OrderController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Order order)





        {
            try
            {
                var uid = HttpContext.Session.GetString("userId");
                order.modifiedBy = Int32.Parse(uid);
                var result = await _odrepo.UpdateOrder(order);

                if (result == 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController1/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var ord = await _odrepo.GetOrderById(id);
            return View(ord);
        }

        // POST: OrderController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteComfirmed(int id)
        {
            try
            {

                var result = await _odrepo.DeleteOrder(id);
                if (result == 1)
                {
                    return RedirectToAction("GetProductsCustomer", "Product", new { num = -3 });
                }
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> OrderIdtem(int id)
         {
            var custname = HttpContext.Session.GetString("userName");
            var custId = HttpContext.Session.GetString("userId");
            if (custId == null || custname == null)
            {

                return RedirectToAction("Create", "User", new { num = -1 });
            }

            int result = await _odrepo.OrderItem(id, custname, Int32.Parse(custId));

            if (result > 0)
            {

                return RedirectToAction(nameof(AddAddress), new { result });




            }
            else
                return View();

        }
        [HttpGet]
        public async Task<ActionResult> MyOrders()
        {
            var custname = HttpContext.Session.GetString("userName");
            var result = await _odrepo.MyOrder(custname);
            if (result.Count > 0)
            {
                return View(result);
            }
            else
                return View();
        }
    }
}
