using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
	[Table("tblAdmin")]
	public class AdminModel
	{
		[Key]
		public int Id { get; set; }
		public string uname { get; set; }
		public string password { get; set; }
	}
}
