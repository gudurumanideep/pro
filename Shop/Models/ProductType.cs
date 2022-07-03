using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class ProductType
    {
        public int Id { get; set; }
        [Required]
        public string ProductT { get; set; }
        
        public string Image { get; set; }
        public int Price { get; set; }
        public bool IsAvailable { get; set; }
    }
}
