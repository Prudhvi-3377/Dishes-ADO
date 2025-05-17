using ADODISHES.Model;

namespace ADODISHES.Repo
{
	public interface IDishRepo

	{

		Task <IEnumerable<Dish>> GetDishesAsync();
		Task <Dish> InsertDishAsync(Dish dish);
		Task<Dish> UpdateDishAsync(Dish dish);
		Task<int> DeleteDishAsync(int id);
		Task<Dish> GetDishByIdAsync(int id);
	}
}
