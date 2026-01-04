namespace ADODISHES.Model
{
	public class Login
	{
		public int UserId { get; set; }
		public required string userName { get; set; }
		public required string password { get; set; }
		public required string Role { get; set; }
	}
}
