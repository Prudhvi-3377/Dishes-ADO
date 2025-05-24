using System.ComponentModel.DataAnnotations;

namespace ADODISHES.Model
{
	public class Dish
	{
        public int Id { get; set; }
		[Required]
		public string? Name { get; set; }
		public string? Description { get; set; }

        public int Quantity { get; set; }
    }
}
