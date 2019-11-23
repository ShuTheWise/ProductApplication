using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Models
{
    public abstract class ProductInputModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name too long")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 99999999.99)]
        [DataType(DataType.Currency)]
        //[Column(TypeName = "decimal(18, 2)")]
        //  [Range(0.01, 999999999999999999, ErrorMessage = "Price must be greter than zero !")]
        public decimal Price { get; set; }

        public bool IsPriceValid()
        {
            return (0.01M <= Price && Price <= 99999999.99M);
        }
    }
}