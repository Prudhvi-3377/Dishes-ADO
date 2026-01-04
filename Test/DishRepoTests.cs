using ADODISHES.Model;
using ADODISHES.Repo;
using Xunit;

namespace ADODISHES.Test
{
	public class DishRepoTests
	{
		private static void ResetDishes()
		{
			DishRepo.Dishes = new List<Dish>
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
		}

		private IConfiguration BuildConfig() => new ConfigurationBuilder().Build();

		[Fact]
		public async Task GetDishes_ReturnsAll()
		{
			ResetDishes();
			var repo = new DishRepo(BuildConfig());
			var all = (await repo.GetDishesAsync()).ToList();
			Assert.Equal(10, all.Count);
		}

		[Fact]
		public async Task GetDishById_ReturnsCorrect()
		{
			ResetDishes();
			var repo = new DishRepo(BuildConfig());
			var dish = await repo.GetDishByIdAsync(2);
			Assert.NotNull(dish);
			Assert.Equal("Chicken Curry", dish!.Name);
		}

		[Fact]
		public async Task Insert_AssignsIdAndAdds()
		{
			ResetDishes();
			var repo = new DishRepo(BuildConfig());
			var newDish = new Dish { Name = "UT Dish", Description = "unit test", Quantity = 5 };
			var inserted = await repo.InsertDishAsync(newDish);

			Assert.True(inserted.Id > 0);
			Assert.Contains(DishRepo.Dishes, d => d.Id == inserted.Id && d.Name == "UT Dish");
		}

		[Fact]
		public async Task Update_UpdatesExisting()
		{
			ResetDishes();
			var repo = new DishRepo(BuildConfig());
			var updated = new Dish { Id = 1, Name = "Updated Name", Description = "Updated", Quantity = 99 };
			await repo.UpdateDishAsync(updated);

			var found = DishRepo.Dishes.First(d => d.Id == 1);
			Assert.Equal("Updated Name", found.Name);
			Assert.Equal(99, found.Quantity);
		}

		[Fact]
		public async Task Delete_RemovesIfExists()
		{
			ResetDishes();
			var repo = new DishRepo(BuildConfig());
			var id = await repo.DeleteDishAsync(1);
			Assert.Equal(1, id);
			Assert.Null(DishRepo.Dishes.FirstOrDefault(d => d.Id == 1));
		}
	}
}