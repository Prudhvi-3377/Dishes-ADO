using ADODISHES.Model;
using Microsoft.Data.SqlClient;
using System.Collections.Concurrent;
using System.Data;
using System.Linq;
namespace ADODISHES.Repo
{
	public class DishRepo : IDishRepo
	{
		private readonly IConfiguration _configuration;
		public static List<Dish> Dishes = new List<Dish>
		{
			new Dish { Id = 1, Name = "Mutton Biryani", Description = "Spicy rice dish made with marinated mutton and basmati rice", Quantity = 25 },
			new Dish { Id = 2, Name = "Chicken Curry", Description = "Traditional Indian curry with tender chicken pieces", Quantity = 30 },
			new Dish { Id = 3, Name = "Paneer Butter Masala", Description = "Creamy tomato-based curry with cottage cheese cubes", Quantity = 20 },
			new Dish { Id = 4, Name = "Vegetable Pulao", Description = "Fragrant rice with mixed vegetables and mild spices", Quantity = 15 },
			new Dish { Id = 5, Name = "Fish Fry", Description = "Crispy fried fish fillets with spices", Quantity = 18 },
			new Dish { Id = 6, Name = "Dal Tadka", Description = "Yellow lentils cooked with tempered spices and ghee", Quantity = 22 },
			new Dish { Id = 7, Name = "Chilli Chicken", Description = "Spicy Indo-Chinese dish with chicken and bell peppers", Quantity = 27 },
			new Dish { Id = 8, Name = "Egg Bhurji", Description = "Indian-style scrambled eggs with onions and spices", Quantity = 19 },
			new Dish { Id = 9, Name = "Rajma Chawal", Description = "Red kidney beans curry served with rice", Quantity = 16 },
			new Dish { Id = 10, Name = "Aloo Gobi", Description = "Dry curry made with potatoes and cauliflower", Quantity = 21 }
		};
		public DishRepo()
		{
		}

		public async Task<Dish?> GetDishByIdAsync(int id) // Updated return type to match the interface
		{

			return Dishes.FirstOrDefault(item => item.Id == id);
		}
		public async Task<IEnumerable<Dish>> GetDishesAsync()
		{
			return Dishes;
		}
		public async Task<Dish> InsertDishAsync(Dish dish)
		{
			dish.Id = Dishes.Count > 0 ? Dishes.Max(d => d.Id) + 1 : 1;
			Dishes.Add(dish);
			return dish;
		}
		public async Task<Dish> UpdateDishAsync(Dish dish)
		{
			var exisiting = Dishes.FirstOrDefault(item => item.Id == dish.Id);
			if (exisiting != null)
			{
				exisiting.Name = dish.Name;
				exisiting.Description = dish.Description;
				exisiting.Quantity = dish.Quantity;
				exisiting.Id = dish.Id;
			}
			return dish;

		}
		public async Task<int> DeleteDishAsync(int id)
		{
			var removingdish = Dishes.FirstOrDefault((item) => item.Id == id);
			if (removingdish != null)
				Dishes.Remove(removingdish);

			return id;
		}

	}
}
