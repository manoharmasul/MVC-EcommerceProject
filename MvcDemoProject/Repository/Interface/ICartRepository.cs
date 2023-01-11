using MvcDemoProject.Models;

namespace MvcDemoProject.Repository.Interface
{
    public interface ICartRepository
    {
        Task<int> AddToCart(CartModel cartmodel);
        Task<List<ViewCartModel>> ViewFromCart(int userId);
        Task<int> RemoveFromCart(int id, int modifiedBy);
    }
}
