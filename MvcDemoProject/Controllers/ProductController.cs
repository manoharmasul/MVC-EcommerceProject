using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcDemoProject.Models;
using MvcDemoProject.Repository;
using MvcDemoProject.Repository.Interface;

namespace MvcDemoProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IOrderRepositories orderRepositories;
        private readonly IProductRepository productRepository;
        private readonly ICartRepository cartRepository;

     
        public ProductController(IProductRepository product, IOrderRepositories orderRepositories, ICartRepository cartRepository)
        {
            this.orderRepositories = orderRepositories;
            this.productRepository = product;
            this.cartRepository = cartRepository;
        }
        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            var prodlist = await productRepository.GetAllProudct();
            return View(prodlist);
        }
        public async Task<ActionResult> GetProductsCustomerCard()
        {

            var prodlist = await productRepository.GetProductCustomer();

            return View(prodlist);
        }
        public async Task<ActionResult> DetailsAction(int id)
        {
            var prod = await productRepository.GetProductById(id);

            return View(prod);
        }
        public async Task<ActionResult> DetailsActionProduct(int id)
        {
            var prod = await productRepository.GetProductById(id);

            return View(prod);
        }
        public async Task<ActionResult> GetProductsCustomer()
        {
            var x = HttpContext.Request.QueryString.Value;
            if (x =="?num=-3")
            {
                ViewBag.y = x;
            }
            else if (x != null && x != "")
            {
                ViewBag.x = -1;
            }
            ViewBag.Idx = 1;
            var prodlist = await productRepository.GetProductCustomer();

            return View(prodlist);
        }
        public async Task<ActionResult> FrontProducts()
        {
            var produlist = await productRepository.GetProductCustomer();
            return View(produlist);
        }

        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var prod=await productRepository.GetProductById(id);    

            return View(prod);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
           
           
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Products products)
        {
            try
            {
                var uId = HttpContext.Session.GetString("userId");
                products.createdBy = Int32.Parse(uId); 

                var prod = await productRepository.AddNewProduct(products);
              
               
                //HttpContext.Session.GetString("userName", user.userName);
              
                if (prod > 0)
                {

                    return RedirectToAction(nameof(Index));
                }
                else
                     return View(prod); 
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var prod = await productRepository.GetProductById(id);

            return View(prod);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Products products)
        {
            try
            {
                var uId = HttpContext.Session.GetString("userId");
                products.modifiedBy = Int32.Parse(uId);
                var reuslt=await productRepository.UpdateProduct(products);

                if (reuslt > 0)
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

        // GET: ProductController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var prod=await productRepository.GetProductById(id);
            return View(prod);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]  
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                User ur = new User();
                var uId = HttpContext.Session.GetString("userId");
                int modifiedBy = Int32.Parse(uId);

                var reuslt = await productRepository.DeleteProduct(id,modifiedBy);
                if (reuslt > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {

                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
      /*  public async Task<IActionResult> AddToCart(int id)
        {
            string userid = HttpContext.Session.GetString("userId");
          
            CartModel cart = new CartModel();
            cart.pId = id;
            cart.uId = Convert.ToInt32(userid);
            cart.createdBy = Convert.ToInt32(userid);
            if (cart.uId == 0)
            {
                return RedirectToAction("Create", "User", new {num=-1});
            }
            int res = await productRepository.AddToCart(cart);
                if (res == 1 && cart.uId != 0)
                {

                    return RedirectToAction(nameof(GetProductsCustomer), new { num = -1 });
                }               
                else
                {


                    return View();
                }
              
            
           
        }*/
       /* [HttpGet]
       
        public async Task<IActionResult> ViewCart()
        {
            var x = HttpContext.Request.QueryString.Value;
            if (x != null && x != "")
            {
                ViewBag.x = -1;
            }
            string userid = HttpContext.Session.GetString("userId");
            int uid= Int32.Parse(userid);
            var model =await productRepository.ViewFromCart(uid);

            return View(model);
        }*/
       /* public async Task<IActionResult> DeleteItem(int id)
           {
            string userid = HttpContext.Session.GetString("userId");

      
            
          
             int modifiedBy = Convert.ToInt32(userid);

            int res = await productRepository.RemoveFromCart(id, modifiedBy);
            if (res == 1)
            {
                return RedirectToAction("ViewCart");
            }
            else
            {
                return View();
            }
        }*/
        public async Task<ActionResult> ProductSearch(string Searchtext)
        {
            var products = await productRepository.SearchProduct(Searchtext);
            if (products.Count() != 0)
            {
                return View(products);

            }
            else
                return View();
        }

        public async Task<ActionResult> ProductCategory(string Searchtext)
        {
            var products = await productRepository.GetProductByCategory(Searchtext);
            if (products.Count() != 0)
            {
                return View(products);

            }
            else
                return View();
        }
    }
        
}
