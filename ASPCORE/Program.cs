using ASPCORE.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ASPCORE.Models;
using ASPCORE.Infrastructure;
using ASPCORE.Repository;
using ASPCORE.GenericRepository;
using ASPCORE.GenericInfrastructure;
using Rotativa.AspNetCore;
using System.Globalization;

//Console.WriteLine(CultureInfo.CurrentCulture);
CultureInfo.CurrentCulture = new("en-US");
DisplayCurrentCulture();
CultureInfo.CurrentCulture = new("bs-Latn-BA");
DisplayCurrentCulture();

static void DisplayCurrentCulture()
{
    Console.WriteLine("----------------------------");
    Console.WriteLine(CultureInfo.CurrentCulture.Name);
    Console.WriteLine(CultureInfo.CurrentCulture.DisplayName);
    //Console.WriteLine(DateTime.Now);
    //Console.WriteLine("----------------------------");

    string dateString = "2.8.2022";
    DateTime date = DateTime.Parse(dateString, new CultureInfo("bs-Latn-BA"));
    Console.WriteLine(date.ToString());
    Console.WriteLine(date.ToString("D"));

    string numberString = "1.600";
    decimal number = decimal.Parse(numberString,
        new CultureInfo("en-US")) + 1;
    Console.WriteLine(number.ToString("c", 
        new CultureInfo("en-us")));
}

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Con")));
builder.Services.AddDbContext<DataContext>();//here used InMemory database using by DataContext
builder.Services.AddDefaultIdentity<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<DataContext>();
builder.Services.Configure<IdentityOptions>(options =>//We can also make Password Complexity this way and we can also add in ApplicationUser, thats I removed
{
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 2;
});
builder.Services.AddTransient<IStudentRepo, StudentRepo>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CreateRolePolicy", policy => policy.RequireClaim("Create Role"));
});
builder.Services.AddTransient(typeof(IRepo<>), typeof(Repository<>));

builder.Services.AddTransient<ITransientService, TaskService>();
builder.Services.AddScoped<IScopedService, TaskService>();
builder.Services.AddSingleton<ISingletonService, TaskService>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddMemoryCache();
//builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseStatusCodePagesWithRedirects("/Error/{0}");
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
//app.MapControllers();

//app.MapAreaControllerRoute(
//    name: "areas",
//    areaName: "Admin",
//    pattern: "{area}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Role}/{action=IndexRole}/{id?}");//pattern: "{tr}/{controller=StudentRepository}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();