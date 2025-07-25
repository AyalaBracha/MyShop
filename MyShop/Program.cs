using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyShop;
using NLog.Web;
using Reposetories;
using Servicess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserServicess, UserServicess>();
builder.Services.AddScoped<IUserReposetory, UserReposetory>();
builder.Services.AddScoped<IProductServicess, ProductServicess>();
builder.Services.AddScoped<IProductReposetories, ProductReposetories>();
builder.Services.AddScoped<ICategoryServicess, CategoryServicess>();
builder.Services.AddScoped<ICategoryReposetories, CategoryReposetories>();
builder.Services.AddScoped<IOrderServicess, OrderServicess>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IOrderReposetories, OrderReposetories>();
builder.Services.AddScoped<IRatingServicess, RatingServicess>();
builder.Services.AddScoped<IRatingReposetories, RatingReposetories>();
builder.Services.AddDbContext<MyShop214935017Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("School")));
builder.Services.AddMemoryCache();
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseNLog();

// JWT Authentication configuration
var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});

builder.Services.AddScoped<TokenService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseErrorHandlingMiddleware();

app.UseMiddlewareRating();
 app.UseMiddleware<MyShop.CspMiddleware>();
//  
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
