using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
