using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using prog_ing_azrv.Server.Data;
using prog_ing_azrv.Server.Extensions;
using prog_ing_azrv.Server.Mapper;
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

//builder.Services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<AzrvContext>();
//for the built in jwt token generationfrom MapIdentityApi


builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

builder.Services.AddIdentityApiEndpoints<User>(o => o.Password.RequiredLength = 5)
    .AddEntityFrameworkStores<AzrvContext>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGroup("/api/auth").MapIdentityApi<User>();

app.MapPost("/api/auth/logout", async (SignInManager<User> signInmanager) =>
{
    await signInmanager.SignOutAsync().ConfigureAwait(false);
    return Results.Ok();
}).RequireAuthorization();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.MapFallbackToFile("/index.html");

app.Run();
