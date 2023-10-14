using AspCoreRestFulAPI.Data;
using AspCoreRestFulAPI.Filters;
using AspCoreRestFulAPI.Infrastructure;
using AspCoreRestFulAPI.Repository;
using DataAccessLayer;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(o =>
{
    o.Filters.Add(new ExampleActionFilter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<DataContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("Con"));
//});
builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("Con"));

builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
builder.Services.AddResponseCaching();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapPost("/productItems",
    async (Product product, DataContext _context) =>
    {
        _context.Products?.Add(product);
        await _context.SaveChangesAsync();
        return Results.Created($"/productItems/{product.Id}", product);//on this product id our result is returned.
    });
app.UseResponseCaching();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
