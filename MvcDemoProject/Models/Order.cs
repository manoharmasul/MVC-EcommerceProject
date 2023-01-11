using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcDemoProject.Models
{
    public class Order:BaseModel
    {
        //Id,custName,pName,Qty,totalAmmount,cretedBy,createDate,modifiedBy,modifiedDate,isDeleted,orderStatus

        public int Id { get; set; }
        public string custName { get; set; }
        public string pName { get; set; }
        public int Qty { get; set; }
        public double totalAmmount { get; set; }
        public string orderStatus { get; set; }
        //billingAdress,shippingAddress
        public string billingAdress { get; set; }
        public string billingAddress { get; set; }

    }
    public class MayOrders
    {
        public int Id { get; set; }
        public string pName { get; set; }
        public int Qty { get; set; }
        public double totalAmmount { get; set; }
        public string orderStatus { get; set; }
    }
    public class GetOrders
    {
        public int Id { get; set; }
        public string custName { get; set; }
        public string pName { get; set; }
        public int Qty { get; set; }
        public double totalAmmount { get; set; }
        public string orderStatus { get; set; }
        public long createdBy { get; set; }
        public long modifiedBy { get; set; }
        [DisplayName("Order Date")]
        public DateTime createdDate { get; set; }

        [DisplayName("Dispached Date")]
        public DateTime modifiedDate { get; set; }
        public string billingAddress { get; set; }
    }
    public class OrderAddress
    {
        [Key]
        [ScaffoldColumn(true)]
        public int Id { get; set; }
        public string billingAddress { get; set; }

        public string shippingAddress { get; set; }

        [DisplayName("Quantity")]
        public int Qty { get; set; }
    }
}
