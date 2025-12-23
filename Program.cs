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

// Swagger
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
            Array.Empty<string>()
        }
    });
});

// ✅ CORS：注册 AllowAll（否则 app.UseCors("AllowAll") 会出问题）
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// DI
builder.Services.AddScoped<IScoreService, ScoreService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAdService, AdService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLogging();
app.UseGlobalExceptionHandler();

// app.UseHttpsRedirection(); // ✅ 本地用 http 就保持注释

app.UseCors("AllowAll");

// ✅ 关键：静态文件（wwwroot/images/ads/...）
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
