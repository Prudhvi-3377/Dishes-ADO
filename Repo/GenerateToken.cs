using ADODISHES.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ADODISHES.Repo
{
	public class GenerateToken : IGenerateToken
	{
		private readonly JwtSettings _jwtSettings;

		public GenerateToken(IOptions<JwtSettings> jwtSettings)
		{
			_jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
		}
		public string CreateToken(string userName,string admin)
		{
			if (string.IsNullOrEmpty(_jwtSettings.Key))
			{
				throw new InvalidOperationException("JWT Key cannot be null or empty.");
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
									new Claim(ClaimTypes.Name, userName),
									new Claim(ClaimTypes.Role, admin)
								}),
				Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
				Issuer = _jwtSettings.Issuer,
				Audience = _jwtSettings.Audience,
				SigningCredentials = new SigningCredentials(
					new SymmetricSecurityKey(key),
					SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
