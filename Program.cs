using ADODISHES.Filter;
using ADODISHES.Model;
using ADODISHES.Repo;
using Microsoft.AspNetCore.Authentication.JwtBearer; // Add this using directive
using Microsoft.IdentityModel.Tokens; // Add this using directive
using System.Text; // Add this using directive

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDishRepo, DishRepo>();
builder.Services.AddScoped<ILoginInterface, LoginService>();
builder.Services.AddScoped<IGenerateToken, GenerateToken>();
// Register the custom filter as a service
builder.Services.AddScoped<CustomFilterinterface>();
// Register the JWT settings from appsettings.json
// 1. Bind JWT settings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// 2. Read values before the container locks
var jwtKey = builder.Configuration["JwtSettings:Key"];
if (string.IsNullOrEmpty(jwtKey))
{
	throw new InvalidOperationException("JwtSettings:Key is missing in configuration.");
}

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
     
// 3. Configure authentication (DO NOT nest AddAuthentication/AddJwtBearer inside itself)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
			ValidAudience = builder.Configuration["JwtSettings:Audience"],
			IssuerSigningKey = key
		};
	});


var app = builder.Build();


	app.UseSwagger();
	app.UseSwaggerUI();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

// Redirect HTTP to HTTPS
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
