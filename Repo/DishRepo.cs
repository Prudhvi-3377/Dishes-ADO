using ADODISHES.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ADODISHES.Repo
{
	public class DishRepo: IDishRepo
	{
		private readonly IConfiguration _configuration;
		public DishRepo(IConfiguration config)
		{
			_configuration = config;
		}
		
public async Task<Dish?> GetDishByIdAsync(int id) // Updated return type to Dish? to indicate it can return null
		{
			Dish? dish = null;

			string query = "SELECT * FROM Dishes WHERE Id = @Id";

			using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
			using (SqlCommand cmd = new SqlCommand(query, con))
			{
				cmd.Parameters.AddWithValue("@Id", id);

				await con.OpenAsync();
				using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
				{
					if (await reader.ReadAsync())
					{
						dish = new Dish
						{
							Id = reader.GetInt32(0),
							Name = reader.GetString(1),
							Description = reader.GetString(2),
							Quantity = reader.GetInt32(3)
						};
					}
				}
			}

			return dish; 
		}
		public async Task<IEnumerable<Dish>> GetDishesAsync()
		{
			List<Dish> dish = new List<Dish>();

			DataTable dt = new DataTable();

			using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
			{
				using (SqlCommand cmd = new SqlCommand("Select * from Dishes", con))
				{
					SqlDataAdapter adapter = new SqlDataAdapter(cmd);

					await con.OpenAsync();


					adapter.Fill(dt);
				}
			}

			foreach (DataRow dr in dt.Rows)
			{
				dish.Add
				(
					new Dish
					{
						Id = (int)dr["Id"],
						Name = (string)dr["Name"],
						Description = (string)dr["Description"],
						Quantity = (int)dr["Quantity"]
					}
				);
			}

			return (dish);
		}
		public async Task<Dish> InsertDishAsync(Dish dish)
		{
			String Query = "Insert into dishes values (@Name,@Description,@Quantity)";

			using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
			{
				using (SqlCommand cmd = new SqlCommand(Query, con))
				{

					await con.OpenAsync();
					cmd.Parameters.AddWithValue("@Name", dish.Name);

					cmd.Parameters.AddWithValue("@Description", dish.Description);
					cmd.Parameters.AddWithValue("@Quantity", dish.Quantity);

					await cmd.ExecuteNonQueryAsync();
				}
			}
			return dish;
		}
		public async Task<Dish> UpdateDishAsync(Dish dish)
		{

			String Query = "Update dishes set Name=@Name,Description=@Description,Quantity=@Quantity where Id=@Id";

			using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
			{
				using (SqlCommand cmd = new SqlCommand(Query, con))
				{

					await con.OpenAsync();

					cmd.Parameters.AddWithValue("@Id", dish.Id);
					cmd.Parameters.AddWithValue("@Name", dish.Name);
					cmd.Parameters.AddWithValue("@Description", dish.Description);
					cmd.Parameters.AddWithValue("@Quantity", dish.Quantity);

					await cmd.ExecuteNonQueryAsync();
				}
			}
			return dish;

		}
		public async Task<int> DeleteDishAsync(int id)
		{
			
			String Query = "Delete from  dishes  where Id=@Id";

			using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
			{
				using (SqlCommand cmd = new SqlCommand(Query, con))
				{

					await con.OpenAsync();

					cmd.Parameters.AddWithValue("@Id", id);


					await cmd.ExecuteNonQueryAsync();
				}
			}
			return id;
		}

	}
}
