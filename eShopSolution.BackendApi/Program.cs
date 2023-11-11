using eShopSolution.Application.Catalog.Products;
using eShopSolution.Application.Common;
using eShopSolution.Data.EF;
using eShopSolution.Utilities.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace eShopSolution.BackendApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<EShopDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString(SystemConstants.MainConnectionString)));

            // declare DI
            builder.Services.AddTransient<IStorageService, FileStorageService>();


            builder.Services.AddTransient<IPublicProductService,PublicProductService>();
            builder.Services.AddTransient<IManageProductService,ManageProductService>();


            builder.Services.AddControllersWithViews();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger eShop Solution", Version = "v1" });
            });
                                        
            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger eShopSolution v1");
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}