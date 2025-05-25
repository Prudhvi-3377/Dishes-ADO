using Microsoft.Data.SqlClient;
using ADODISHES.Model;
namespace ADODISHES.Repo
{
	public class LoginService : ILoginInterface
	{
		private readonly IConfiguration _configuration;
		public LoginService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task<(bool isSuccess, Login login)> LoginAsync(string username, string password)
		{
			string query = "select * from USERS where Username = @username and password = @password";
			using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
			{
				using (SqlCommand cmd = new SqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("username", username);
					cmd.Parameters.AddWithValue("password", password);
					con.Open();
					using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
					{
						if (await reader.ReadAsync())
						{
							Login login = new Login
							{
								UserId = reader.GetInt32(0),
								UserName = reader.GetString(1),
								Password = reader.GetString(2),
								Role = reader.GetString(3)
							};
							return (true, login);
						}
					}
				}
			}
			return (false, new Login());
		}
	}
}
