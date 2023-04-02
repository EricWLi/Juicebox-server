using Microsoft.EntityFrameworkCore;
using JuiceboxServer.Data;
using JuiceboxServer.Models;
using JuiceboxServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using JuiceboxServer.Config;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("JuiceboxDB");
var jwtSettings = builder.Configuration.GetSection("JWTSettings").Get<JwtSettings>();

if (String.IsNullOrEmpty(jwtSettings?.Secret))
{
    throw new ApplicationException("JWT secret is not set.");
}

builder.Services.AddDbContext<JuiceboxContext>(options => 
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<JuiceboxContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<SpotifySettings>(builder.Configuration.GetSection("SpotifySettings"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped(typeof(ITokenStore<>), typeof(TokenStore<>));
builder.Services.AddScoped<SpotifyAuthService>();
builder.Services.AddScoped<SpotifyRemoteService>();

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
app.UseSession();
app.MapControllers();

app.Run();
