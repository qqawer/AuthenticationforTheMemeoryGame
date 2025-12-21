using AuthenticationforTheMemeoryGame.Configurations;
using AuthenticationforTheMemeoryGame.Data;
using AuthenticationforTheMemeoryGame.Extensions;
using AuthenticationforTheMemeoryGame.Interfaces;
using AuthenticationforTheMemeoryGame.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "please input: Bearer {your token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });
});

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
//regitser database context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IScoreService, ScoreService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAdService, AdService>();
builder.Services.AddScoped<ITokenService,TokenService>();

builder.Services.AddJwtAuthentication(builder.Configuration);


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRequestLogging();
app.UseGlobalExceptionHandler();
app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();//check Token
app.UseAuthorization();

app.MapControllers();

app.Run();
