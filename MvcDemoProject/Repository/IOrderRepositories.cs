using Microsoft.CodeAnalysis;
using MvcDemoProject.Models;

namespace MvcDemoProject.Repository
{
    public interface IOrderRepositories
    {
        Task<IEnumerable<GetOrders>> GetOrders();
        Task<Order> GetOrderById(int? id);
        Task CreateOrder(Order order);
        Task<int> UpdateOrder(Order order);
        Task<int> DeleteOrder(int id);
        Task<int> OrderItem(int id, string custName, int custId);
        Task<List<MayOrders>> MyOrder(string custName);
        Task<int> OrderAddress(string shippingAddress, int Qty, int Id, int uid);



        // Task<int> NewOrder(int id); 

    }
}


