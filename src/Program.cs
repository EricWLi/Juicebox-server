using Microsoft.EntityFrameworkCore;
using JuiceboxServer.Data;
using JuiceboxServer.Models;
using JuiceboxServer.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("JuiceboxDB");

builder.Services.AddDbContext<JuiceboxContext>(options => 
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<JuiceboxContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
