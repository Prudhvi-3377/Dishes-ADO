using ADODISHES.Model;
using ADODISHES.Repo;
using Microsoft.AspNetCore.Mvc;
namespace ADODISHES.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DishesController : ControllerBase
	{
		private readonly IDishRepo _dishRepo;
		public DishesController(IDishRepo dishRepo)
		{
			_dishRepo = dishRepo;
		}
		[Route("GetDishes/id")]
		[HttpGet]
		public async Task<ActionResult<Dish>> GetDishes(int id)
		{
			Dish dish = await _dishRepo.GetDishByIdAsync(id);
			if (dish == null)
			{
				return NotFound($"Dish with ID {id} not found.");
			}
			return (dish);
		}
		[HttpGet]
		public async Task<IActionResult> GetDishes()
		{
			IEnumerable<Dish> dishes = await _dishRepo.GetDishesAsync();
			return Ok(dishes);
		}

		[HttpPost]
		public async Task<IActionResult> InsertDishes(Dish dish)
		{
			Dish outDish = await _dishRepo.InsertDishAsync(dish);
			return Ok(String.Format("The Dish has been inserted with Name {0}, Description {1} and Quantity {2}", outDish.Name, outDish.Description, outDish.Quantity));
		}

		[HttpPut]
		public async Task<IActionResult> updateDishes(Dish dish)
		{
			Dish outDish = await _dishRepo.UpdateDishAsync(dish);
			return Ok(String.Format("The Dish has been Updated with an ID of {0} with Name {1}, Description {2} and Quantity {3}", outDish.Id, dish.Name, dish.Description, dish.Quantity));
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteDishes(int id)
		{
			int DeletedId = await _dishRepo.DeleteDishAsync(id);
			return Ok(String.Format("The Dish has been Deleted with an ID of {0} ", id));
		}
	}
}