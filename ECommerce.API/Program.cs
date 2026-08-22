

using ECommerce.API.MiddleWare;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(
        new JsonStringEnumConverter());
});
builder.Services.AddProblemDetails ( options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
        context.ProblemDetails.Extensions.Add("requestId", context.HttpContext.TraceIdentifier);

    };

    
});
builder.Services.AddExceptionHandler<GlobalExceptionHanlder>();
builder.Services.InferastructureServices(builder.Configuration);
builder.Services.AddCoreServices();

builder.Services.AddTransient<IDataSeed,DataSeed>();
builder.Services.AddScoped<ISeedIdentityData, SeedIdentityData>();
builder.Services.AddSwaggerGen();   
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var data = scope.ServiceProvider.GetRequiredService<IDataSeed>();
    var usersAndroles = scope.ServiceProvider.GetRequiredService<ISeedIdentityData>();
    data.DataSeed();
    await usersAndroles.SeedRoleAndUserData(); 
}
    app.UseExceptionHandler();

    app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseStaticFiles();   

app.UseAuthorization();

app.MapControllers();

app.Run();
