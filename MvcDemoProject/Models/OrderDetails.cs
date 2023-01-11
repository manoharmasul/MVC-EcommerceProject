namespace MvcDemoProject.Models
{
    public class OrderDetails:BaseModel
    {
        //Id,oId,pId,pName,qty,totalAmmount,createdBy,createdDate,modifiedBy,modifiedDate,isDeleted
        public int Id { get; set; }
        public int oId { get; set; }
        public int pId { get; set; }
        public int pName { get; set; }
        public int qty { get; set; }
        public int totalAmmount { get; set; }
        
    }
}
