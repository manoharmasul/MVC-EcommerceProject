namespace MvcDemoProject.Models
{
    public class Products:BaseModel
    {
      //  Id,pName,pimgUrl,price
        public int Id { get; set; }
        public string pName { get; set; }
        public string pimgUrl { get; set; }
        public double price { get; set; }
    }
    public class ProductSearch
    {
        public string SearchText { get; set; }
    }
    public class CustomerProduct
    {

        public int Id { get; set; }
        public string pName { get; set; }
        public string pimgUrl { get; set; }
        public double price { get; set; }
        public string Specification { get; set; }
        public string Description { get; set; }
        public List<ProductTypeDropDown> prodType { get; set; }
    }
    public class ProductTypeDropDown
    {
        public string productType { get; set; }
    }
   
}
