using ADODISHES.Filter;
using ADODISHES.Model;
using ADODISHES.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace ADODISHES.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[ServiceFilter(typeof(CustomFilterinterface))] // This filter will validate the model state

	public class DishesController : ControllerBase
	{
		private readonly IDishRepo _dishRepo;
		private readonly ILoginInterface _loginInterface;
		private readonly IGenerateToken _generateToken;
		public DishesController(IDishRepo dishRepo, ILoginInterface loginInterface, IGenerateToken generateToken)
		{
			_dishRepo = dishRepo;
			_loginInterface = loginInterface;
			_generateToken = generateToken;
		}
		[HttpPost("login")]
		public async Task<IActionResult> Login(string userName, string Password)
		{
			if ( string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(Password))
			{
				return BadRequest("Invalid login credentials.");
			}
			var (isSuccess, login) = await _loginInterface.LoginAsync(userName, Password);
			if (!isSuccess)
			{
				return Unauthorized("Invalid username or password.");
			}
			// Generate a token for the user
			return Ok( _generateToken.CreateToken(userName));

		}


		[Route("GetDishes/id")]
		[HttpGet]
		[Authorize] 
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