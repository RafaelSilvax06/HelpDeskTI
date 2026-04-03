using Microsoft.EntityFrameworkCore;
using HelpDeskTI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add os serviços
builder.Services.AddControllersWithViews();

// Add o Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


// BD SQLLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=C:\\repositorios\\HelpDeskTI\\helpdesk.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (db.Database.GetPendingMigrations().Any())
    {
        db.Database.EnsureDeleted(); //  apaga tudo
        db.Database.Migrate();       //  recria com a nova estrutura
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

//  Ativar Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();