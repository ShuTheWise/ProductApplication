using ProductManagement.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Models
{
    public class ProductUpdateInputModel : ProductInputModel
    {
        [Required(ErrorMessage = "Id is required")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "Wrong guid format")]
        public Guid Id { get; set; }

        public ProductUpdateInputModel()
        {

        }

        public ProductUpdateInputModel(Product p)
        {
            Id = Guid.Parse(p.Id);
            Name = p.Name;
            Price = p.Price;
        }

        public bool IsValid()
        {
            return
                !string.IsNullOrEmpty(Name) && IsPriceValid();
        }
    }
}