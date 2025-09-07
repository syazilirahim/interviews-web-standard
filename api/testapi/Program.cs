using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using testapi.Data; // Add this
var builder = WebApplication.CreateBuilder(args);
// ✅ Fix for SQLitePCL: must call this before using EF Core
Batteries.Init();
// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=database.db")); // Or UseSqlServer(connectionString)

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNuxt", policy =>
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowNuxt");
app.UseSwagger();
app.UseSwaggerUI();

// ✅ RUN MIGRATIONS ON STARTUP
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //db.Database.EnsureDeleted();  // ⚠️ Deletes database file
    db.Database.Migrate();
}

app.MapControllers();
app.Run();