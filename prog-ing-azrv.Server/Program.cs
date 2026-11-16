using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using prog_ing_azrv.Server.Data;
using prog_ing_azrv.Server.Extensions;
using prog_ing_azrv.Server.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AzrvContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.RegisterRepositoryInterfaces();
builder.Services.RegisterServiceAbstractions();

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<AzrvContext>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();


var app = builder.Build();

app.MapGroup("/api/auth").MapIdentityApi<User>();

app.UseAuthentication();
app.UseAuthorization();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
