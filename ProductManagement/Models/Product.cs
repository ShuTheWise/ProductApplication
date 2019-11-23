namespace ProductManagement.Models
{
    //
    public class Product
    {
        //unfortunately id has to be a string here for a Dapper to work properly in quering SQL DB
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}