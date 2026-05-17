using AdoptABuddy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using modelMVC.Models;
using modelMVC.Repositories;
using modelMVC.Services;

var builder = WebApplication.CreateBuilder(args);
//linii pentru a inregistra Repository-urile si Serviciile
builder.Services.AddScoped<IRepository<Animal>, AnimalRepository>();
builder.Services.AddScoped<IAnimalService, AnimalService>();

builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IRepository<Shelter>, ShelterRepository>();
builder.Services.AddScoped<IShelterService, ShelterService>();

builder.Services.AddScoped<IRepository<ContactMessage>, ContactMessageRepository>();
builder.Services.AddScoped<IContactMessageService, ContactMessageService>();

builder.Services.AddScoped<IRepository<AdoptionStory>, AdoptionStoryRepository>();
builder.Services.AddScoped<IAdoptionStoryService, AdoptionStoryService>();

builder.Services.AddScoped<IRepository<MedicalRecord>, MedicalRecordRepository>();
builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();

builder.Services.AddScoped<IAuthService, AuthService>();


// Add services to the container.
builder.Services.AddControllersWithViews();

// ...
builder.Services.AddDbContext<AdoptBuddyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurare ASP.NET Core Identity cu setari de parola conform instructiunilor din laborator
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;            // Sa contina cifre 
    options.Password.RequiredLength = 6;             // Lungime minima 
    options.Password.RequireNonAlphanumeric = false; // Fara caractere speciale obligatorii 
    options.Password.RequireUppercase = true;        // Sa contina litere mari 
    options.Password.RequireLowercase = false;       // Fara litere mici obligatorii 
})
.AddEntityFrameworkStores<AdoptBuddyContext>()
.AddDefaultTokenProviders();

// Inregistram Serviciul nostru personalizat de Autentificare
builder.Services.AddScoped<IAuthService, AuthService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// CRITIC: Autentificarea trebuie sa fie intotdeauna INAINTEA Autorizarii!
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
