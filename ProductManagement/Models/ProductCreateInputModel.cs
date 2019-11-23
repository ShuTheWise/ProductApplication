namespace ProductManagement.Models
{
    public class ProductCreateInputModel : ProductInputModel
    {
        public bool IsValid()
        {
            return
                !string.IsNullOrEmpty(Name) && IsPriceValid();
        }
    }
}