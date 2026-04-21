using Microsoft.EntityFrameworkCore;
using HelpDeskTI.Data;
using HelpDeskTI.Services;
using HelpDeskTI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add os serviços
builder.Services.AddControllersWithViews();

// Add o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// REGISTRO DAS DEPENDÊNCIAS
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<UsuarioRepositories>();

builder.Services.AddScoped<ChamadoService>();      
builder.Services.AddScoped<ChamadoRepositories>(); 

builder.Services.AddControllers();

// BD SQLLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=helpdesk.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (app.Environment.IsDevelopment()) // 👈 MUITO IMPORTANTE
    {
        var hasPendingMigrations = db.Database.GetPendingMigrations().Any();

        if (hasPendingMigrations)
        {
            Console.WriteLine("⚠️ DEV: Mudanças detectadas. Recriando banco...");

            db.Database.EnsureDeleted(); // apaga tudo
            db.Database.Migrate();       // recria com migrations
        }
        else
        {
            Console.WriteLine("✅ Banco já atualizado.");
        }
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