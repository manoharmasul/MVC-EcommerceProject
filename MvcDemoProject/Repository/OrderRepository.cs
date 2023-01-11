using Dapper;
using Microsoft.AspNetCore.Mvc;
using MvcDemoProject.DBContext;
using MvcDemoProject.Models;

namespace MvcDemoProject.Repository
{
    public class OrderRepository : IOrderRepositories
    {
        private readonly DapperContext context;

        public OrderRepository(DapperContext context)
        {
            this.context = context;
        }


        public async Task CreateOrder(Order order)
        {
            //Id,custName,pName,Qty,totalAmmount,cretedBy,createDate,modifiedBy,modifiedDate,isDeleted,orderStatus

            var query = @"insert into tblOrder (custName,pName,Qty,totalAmmount,

              createdBy,createdDate,isDeleted,orderStatus) 

            values(@custName,@pName,@Qty,@totalAmmount,

             @createdBy,getdate(),0,@orderStatus)";
            order.orderStatus = "pending";
            using (var connection = context.CreateConnection())
            {
                await connection.ExecuteAsync(query, order);
            }
        }

        public async Task<int> DeleteOrder(int id)
        {
            var query = @"update tblOrder set modifiedBy=101,modifiedDate=getdate(),isDeleted=1 where Id=@Id";
            using (var connections = context.CreateConnection())
            {
                var result = await connections.ExecuteAsync(query, new { Id = id });
                return result;
            }
        }

        public async Task<Order> GetOrderById(int? id)
        {
            var query = @"select * from tblOrder where Id=@Id and IsDeleted=0";
            using (var connection = context.CreateConnection())
            {
                var order = await connection.QueryAsync<Order>(query, new { Id = id });
                return order.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<GetOrders>> GetOrders()
        {
            //custName,pName,Qty,totalAmmount,orderDate,orderStatus
            var query = @"select * from tblOrder where IsDeleted=0 and shippingAddress is not null order by id desc";
            using (var connection = context.CreateConnection())
            {
                var orderlist = await connection.QueryAsync<GetOrders>(query);
                return orderlist;
            }
        }

        public async Task<List<MayOrders>> MyOrder(string custName)
        {
            var query = @"select pName,Qty,totalAmmount,orderStatus from tblOrder where custName=@custName and isDeleted=0 and shippingAddress is not null order by Id desc";
            using (var connection = context.CreateConnection())
            {
                var orde = await connection.QueryAsync<MayOrders>(query, new { custName = custName });
                return orde.ToList();
            }
        }

        public async Task<int> OrderAddress(string shippingAddress, int Qty, int Id, int uid)
        {
            var query = "update tblOrder set  shippingAddress=@shippingAddress,Qty=@Qty,totalAmmount=@totalAmmount where Id=@Id and isDeleted=0";
            using (var connection = context.CreateConnection())
            {
                var pr = await connection.QuerySingleAsync<double>(@"select totalAmmount from tblOrder where Id=@Id and isDeleted=0 ", new { Id = Id });
                double totolammount = pr * Qty;
                double wb = await connection.QuerySingleOrDefaultAsync<double>(@"select wallet from tblUser where Id=@Id", new { Id = uid });
                if (wb >= totolammount)
                {
                    wb -= totolammount;
                    await connection.ExecuteAsync(@"update tblUser set wallet=@wallet where Id=@Id", new { Id = uid, wallet = wb });

                    var add = await connection.ExecuteAsync(query, new { shippingAddress = shippingAddress, Qty = Qty, Id = Id, totalAmmount = totolammount });
                    return add;
                }
                else
                {
                    var result = await connection.ExecuteAsync(@"delete tblOrder where Id=@Id", new { Id = Id });

                    return -4;
                }

            }
        }

        public async Task<int> OrderItem(int id, string custName, int custId)
        {
            Order item = new Order();
            //Id,custName,pName,Qty,totalAmmount,cretedBy,createDate,modifiedBy,modifiedDate,isDeleted,orderStatus


            var query = @"insert into tblOrder (custName,pName,Qty,totalAmmount,

              createdBy,createdDate,isDeleted,orderStatus) 

            values(@custName,@pName,@Qty,@totalAmmount,

             @createdBy,getdate(),0,@orderStatus);SELECT CAST(SCOPE_IDENTITY() as int)";
            using (var connection = context.CreateConnection())
            {
                var prod = await connection.QuerySingleOrDefaultAsync<Products>(@"select * from tblProduct where Id=@Id", new { Id = id });
                item.pName = prod.pName;
                item.custName = custName;
                item.totalAmmount = prod.price;
                item.createdBy = custId;
                item.orderStatus = "pending";
                var result = await connection.QuerySingleAsync<int>(query, item);
                return result;
            }
        }



        //public async Task<int> NewOrderDetails(int id)
        //{        //Id,oId,pId,pName,qty,totalAmmount,createdBy,createdDate,modifiedBy,modifiedDate,isDeleted

        //    OrderDetails details = new OrderDetails();
        //    details.pId = id;   

        //    var query = @"insert into tblOrder(oId,pId,pName,qty,totalAmmount,createdBy,createdDate,isDeleted)        

        //                 values(@oId,@pId,@pName,@qty,@totalAmmount,@createdBy,getdate(),0)";

        //    using(var connection=context.CreateConnection())
        //    {

        //        var result =await connection.ExecuteAsync(query);   
        //    }
        //}

        public async Task<int> UpdateOrder(Order order)
        {

            //Id,custName,pName,Qty,totalAmmount,cretedBy,createDate,modifiedBy,modifiedDate,isDeleted,orderStatus

            var query = @"update tblOrder set pName=@pName,Qty=@Qty,

                        totalAmmount=@totalAmmount,

                        modifiedBy=@modifiedBy
 
                        ,modifiedDate=getdate(),orderStatus=@orderStatus,billingAddress=@billingAddress where isDeleted=0 and Id=@Id";
            using (var connection = context.CreateConnection())
            {
                var resutl = await connection.ExecuteAsync(query, order);
                return resutl;

            }
        }
    }
}
