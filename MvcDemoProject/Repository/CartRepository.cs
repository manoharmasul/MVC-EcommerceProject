using Dapper;
using Microsoft.Data.SqlClient;
using MvcDemoProject.DBContext;
using MvcDemoProject.Models;
using MvcDemoProject.Repository.Interface;
using System.Drawing;

namespace MvcDemoProject.Repository
{
    public class CartRepository:ICartRepository
    {
        private readonly DapperContext context;

        public CartRepository(DapperContext context)
        {
            this.context = context;
        }
            public async Task<int> AddToCart(CartModel cartmodel)
            {
                var query = @"insert into tblCart (pId,uId,createdBy,createdDate) 

                              values(@pId,@uId,@createdBy,getDate())";
                using (var connection = context.CreateConnection())
                {


                    var cartp = await connection.ExecuteAsync(query, cartmodel);
                    return cartp;
                }
            }
        public async Task<List<ViewCartModel>> ViewFromCart(int userId)
        {
            var query = @"select c.Id,c.pId,p.pName,p.pimgUrl,p.price,p.Specification,c.createdDate from tblCart c 

                         inner join tblProduct p on c.pId=p.Id inner join tblUser u on c.uId= u.Id

                          where c.isDeleted=0 and u.Id=@userId";

            using (var connection = context.CreateConnection())
            {
                var cart = await connection.QueryAsync<ViewCartModel>(query, new { userId = userId });
                return cart.ToList();
            }

        }
        public async Task<int> RemoveFromCart(int id, int modifiedBy)
        {
            var query = @"update tblCart set modifiedBy=@modifiedBy, modifiedDate=getDate(),isDeleted=1 where Id=@Id";
            using (var connection = context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, new { Id = id, modifiedBy = modifiedBy });
                return result;

            }
        }
    }
}
