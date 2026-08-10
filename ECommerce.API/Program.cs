
using ECommerce.Domain.Abstraction;
using ECommerce.Inferastructure.DataSeed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddTransient<IDataSeed,DataSeed>();   
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var data = scope.ServiceProvider.GetRequiredService<IDataSeed>();
    data.DataSeed();
}
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
