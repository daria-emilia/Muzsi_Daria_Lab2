using Microsoft.EntityFrameworkCore;
using Muzsi_Daria_Lab2.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Muzsi_Daria_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Muzsi_Daria_Lab2Context' not found.");
builder.Services.AddDbContext<Muzsi_Daria_Lab2Context>(options => options.UseSqlServer(connectionString));

builder.Services.AddRazorPages();

var app = builder.Build();

// Aici aplicăm migrările automat la prima pornire pe baza de date goală:
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<Muzsi_Daria_Lab2Context>();
    context.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();