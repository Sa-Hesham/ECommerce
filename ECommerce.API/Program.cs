

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.InferastructureServices(builder.Configuration);
builder.Services.AddCoreServices();

builder.Services.AddTransient<IDataSeed,DataSeed>();
builder.Services.AddSwaggerGen();   
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var data = scope.ServiceProvider.GetRequiredService<IDataSeed>();
    data.DataSeed();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
