using Dapper;
using Microsoft.CodeAnalysis;
using MvcDemoProject.DBContext;
using MvcDemoProject.Models;
using MvcDemoProject.Repository.Interface;
using NuGet.Packaging;

namespace MvcDemoProject.Repository
{
    public class ProductRepository: IProductRepository
    {
        private readonly DapperContext context;

        public ProductRepository(DapperContext context)
        {
            this.context = context;
        }

        public async Task<int> AddNewProduct(Products product)
        {
            var query = @"insert into tblProduct (pName,pimgUrl,price,createdBy,createdDate,isDeleted)

                          values (@pName,@pimgUrl,@price,@createdBy,@createdDate,0)";
            using(var connection=context.CreateConnection())
            {
                var podid = await connection.ExecuteAsync(query, product);
                return podid;
            }
        }


        public async Task<int> DeleteProduct(int id, int modifiedBy )
        {
            var query = @"update tblProduct set modifiedBy=@modifiedBy,modifiedDate=getDate(),isDeleted=1 where Id=@Id";
            using(var connections=context.CreateConnection())
            {
                var result = await connections.ExecuteAsync(query, new { Id = id ,modifiedBy=modifiedBy});
                return result;
            }
        }

        public async Task<IEnumerable<Products>> GetAllProudct()
        {
            var query = @"select * from tblProduct where isDeleted=0";
            using(var connection=context.CreateConnection())
            {
                var result = await connection.QueryAsync<Products>(query);
                return result.ToList();
            }
        }

        public async Task<CustomerProduct> GetProductById(int id)
        {
            var query = @"select * from tblProduct where Id=@Id and isDeleted=0";
            using (var connection = context.CreateConnection())
            {
                var prod = await connection.QuerySingleOrDefaultAsync<CustomerProduct>(query, new { Id = id });
                return prod;
            }
           
        }

        public async Task<int> UpdateProduct(Products products)
        {
            var query = @"update tblProduct set pName=@pName,pimgUrl=@pimgUrl,price=@price,modifiedBy=@modifiedBy,modifiedDate=getDate() where isDeleted=0 and Id=@Id";
            using(var connection=context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, products);
                return result;
            }
        }


        /*public async Task<int> AddToCart(CartModel cartmodel)
        {
            var query = @"insert into tblCart (pId,uId,createdBy,createdDate) values(@pId,@uId,@createdBy,getDate())";
            using (var connection = context.CreateConnection())
            {


                var cartp = await connection.ExecuteAsync(query, cartmodel);
                return cartp;
            }
       
        
        
        }*/
       /* public async Task<int> RemoveFromCart(int id, int modifiedBy)
        {
            var query = @"update tblCart set modifiedBy=@modifiedBy, modifiedDate=getDate(),isDeleted=1 where Id=@Id";
            using (var connection = context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, new { Id = id, modifiedBy = modifiedBy });
                return result;

            }
        }*/

     /*   public async Task<List<ViewCartModel>> ViewFromCart(int userId)
        {
            var query = @"select c.Id,c.pId,p.pName,p.price,c.createdDate from tblCart c 

                         inner join tblProduct p on c.pId=p.Id inner join tblUser u on c.uId= u.Id

                          where c.isDeleted=0 and u.Id=@userId";

            using (var connection = context.CreateConnection())
            {
                var cart = await connection.QueryAsync<ViewCartModel>(query, new { userId = userId });
                return cart.ToList();
            }

        }*/

        public async Task<List<CustomerProduct>> SearchProduct(string searchtext)
        {
            var query = @"select * from tblProduct where pName like '%'+@pName+'%'  and isDeleted=0";
            using(var connection = context.CreateConnection())
            {
                var result=await connection.QueryAsync<CustomerProduct>(query, new { pName = searchtext });
                return result.ToList();
            }
        }

        public async Task<IEnumerable<CustomerProduct>> GetProductCustomer()
        {
            var query = @"select * from tblProduct where isDeleted=0";
            using (var connection = context.CreateConnection())
            {
                var result = await connection.QueryAsync<CustomerProduct>(query);
               
                return result.ToList();
            }

        }



        public async Task<List<CustomerProduct>> GetProductByCategory(string searchtext)
        {
            var query = "select p.Id,p.pName,p.pimgUrl,p.price,p.description,p.Specification,pt.productType from tblProduct p inner join  tblProductType pt on  p.typeId=pt.Id where pt.productType=@searchtext";
            using (var connections = context.CreateConnection())
            {
                var result = await connections.QueryAsync<CustomerProduct>(query, new { searchtext = searchtext });

                return result.ToList();
            }
        }
    }
}
