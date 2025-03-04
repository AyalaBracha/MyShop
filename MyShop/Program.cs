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
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseErrorHandlingMiddleware();

app.UseMiddlewareRating();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
