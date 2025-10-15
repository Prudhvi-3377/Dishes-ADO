using Microsoft.Data.SqlClient;
using ADODISHES.Model;
namespace ADODISHES.Repo
{
	public class LoginService : ILoginInterface
	{
		private static List<Login> Logins = new List<Login>
		{
			new Login { UserId = 1, userName = "ADMIN", password = "ADMIN", Role = "ADMIN" },
			new Login { UserId = 2, userName = "Lavanya", password = "Lavanya@12", Role = "ADMIN" },
			new Login { UserId = 3, userName = "Prudhvi", password = "Prudhvi@21", Role = "ADMIN" }
		};

		public LoginService()
		{
		}

		public async Task<(bool isSuccess, Login login)> LoginAsync(string userName, string password)
		{
			

			Login? login = Logins.FirstOrDefault(l => l.userName.Equals(userName, StringComparison.OrdinalIgnoreCase) && l.password == password);
			if(login != null)
			{
				return (true, login);
			}
			return (false, new Login());
		}
	}
}
