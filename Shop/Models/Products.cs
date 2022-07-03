using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Products
    {
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Image { get; set; }

    }
}
