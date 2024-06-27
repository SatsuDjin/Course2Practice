using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Infrastructure.Data;
using Application.Interfaces;
using Infrastructure.Services;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ClinicContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
// Настройка аутентификации
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICityRepository, CityRepository>();
builder.Services.AddTransient<ICityService, CityService>();
builder.Services.AddTransient<IClinicRepository, ClinicRepository>();
builder.Services.AddTransient<IClinicService, ClinicService>();
builder.Services.AddTransient<ISpecializationRepository, SpecializationRepository>();
builder.Services.AddTransient<ISpecializationService, SpecializationService>();
builder.Services.AddTransient<IDoctorRepository, DoctorRepository>();
builder.Services.AddTransient<IDoctorService, DoctorService>();
builder.Services.AddScoped<IMainService, MainService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ClinicContext>();
    await DataSeeder.SeedAdminUserAsync(context);
    await DataSeeder.SeedCitiesAsync(context);
    await DataSeeder.SeedClinicsAsync(context);
    await DataSeeder.SeedSpecializationsAsync(context);
    await DataSeeder.SeedDoctorsAsync(context);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}",
    defaults: new { controller = "Main", action = "Index" });

app.MapRazorPages();

app.Run();
