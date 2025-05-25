
using ADODISHES.Model;

namespace ADODISHES.Repo
{

	public interface ILoginInterface
	{
		Task<(bool isSuccess,Login login)> LoginAsync(string username, string password);
	}
}
