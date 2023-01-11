namespace MvcDemoProject.Models
{
    public class CartModel:BaseModel
    {//Id,pId,price,uId

        public int Id { get; set; }
        public int pId { get; set; }
        public int uId { get; set; }
    }
    public class ViewCartModel
    {
        public int Id { get; set; }
        public int pId { get; set; }
        public string pName { get; set; }
        public string pimgUrl { get; set; }
        public double price { get; set; }
        public DateTime createdDate { get; set; }
        public string Specification { get; set; }

    }
}
