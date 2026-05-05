using AdoptABuddy.Models;
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

// Add services to the container.
builder.Services.AddControllersWithViews();

// ...
builder.Services.AddDbContext<AdoptBuddyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));




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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
