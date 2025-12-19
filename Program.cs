using AuthenticationforTheMemeoryGame.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//regitser database context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// ========== Add: Database connection test code ==========
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Get injected AppDbContext instance
        var dbContext = services.GetRequiredService<AppDbContext>();
        // Test connection: attempt to open database connection
        dbContext.Database.OpenConnection();
        // Verify if database exists (create empty .db file if not exists)
        var isCreated = dbContext.Database.EnsureCreated();

        if (isCreated)
        {
            Console.WriteLine("✅ Database file created successfully, connection established!");
        }
        else
        {
            Console.WriteLine("✅ Database already exists, connection established!");
        }
    }
    catch (Exception ex)
    {
        // Output error message if connection fails
        Console.WriteLine($"❌ Database connection failed: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
