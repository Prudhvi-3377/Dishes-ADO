using ADODISHES.Filter;
using ADODISHES.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDishRepo, DishRepo>();
builder.Services.AddScoped<ILoginInterface, LoginService>();
// Register the custom filter as a service
builder.Services.AddScoped<CustomFilterinterface>();
// Register the custom filter globally
/*
builder.Services.AddControllers(options =>
{
	options.Filters.Add<CustomFilterinterface>();
});

*/


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseCors(policy=> policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.MapControllers();

app.Run();
