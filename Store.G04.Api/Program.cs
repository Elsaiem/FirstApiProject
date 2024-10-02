
using Microsoft.EntityFrameworkCore;
using Store.G04.Core;
using Store.G04.Core.Mapping.Products;
using Store.G04.Core.ServicesContract;
using Store.G04.Repository.Data;
using Store.G04.Repository.Data.Contexts;
using Store.G04.Repository.Repositories;
using Store.G04.Service.Services.Products;

namespace Store.G04.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            builder.Services.AddScoped<IServiceProduct, ProductService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            builder.Services.AddAutoMapper(M => M.AddProfile(new ProductProfile()));


            var app = builder.Build();






            using var scope= app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var context=  service.GetRequiredService<StoreDbContext>();
            var looger=  service.GetRequiredService<ILoggerFactory>();
            try
            {
                await context.Database.MigrateAsync();
                await StoreDbContextSeed.SeedAsync(context);
            }

            catch (Exception ex) {

                var loger = looger.CreateLogger<Program>();
                loger.LogError(ex, "there are problem during apply migrations !");
            
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
        }
    }
}
