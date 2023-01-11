using MvcDemoProject.Models;

namespace MvcDemoProject.Repository.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Products>> GetAllProudct();
        Task<int> AddNewProduct(Products product);
        Task<int> DeleteProduct(int id, int modifiedBy);
        Task<int> UpdateProduct(Products products);
        Task<CustomerProduct> GetProductById(int id);


       // Task<int> AddToCart(CartModel cartmodel);
       // Task<List<ViewCartModel>> ViewFromCart(int userId);
   //     Task<int> RemoveFromCart(int id, int modifiedBy);

        Task<List<CustomerProduct>> SearchProduct(string searchtext);
        Task<IEnumerable<CustomerProduct>> GetProductCustomer();
        Task<List<CustomerProduct>> GetProductByCategory(string searchtext);
      
    }
     
}
    
