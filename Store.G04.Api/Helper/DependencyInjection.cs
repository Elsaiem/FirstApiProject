﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.AppConfig;
using Store.G04.Core.ServicesContract;
using Store.G04.Core;
using Store.G04.Repository.Data.Contexts;
using Store.G04.Repository.Repositories;
using Store.G04.Service.Services.Products;
using Store.G04.Core.Mapping.Products;
using Microsoft.AspNetCore.Mvc;
using Store.G04.Api.Errors;
using Store.G04.Core.RepositoreContract;
using StackExchange.Redis;
using Store.G04.Core.Mapping.Busket;

namespace Store.G04.Api.Helper
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddDependency(this IServiceCollection services ,IConfiguration configuration )
        {

            services.AddBuildService();
            services.AddSwaggerdService();
            services.AddDataBaseCOnnectiondService(configuration);
            services.AddUSerDEfinedCOnnectiondService();
            services.AddAutoMaperCOnnectiondService(configuration);
            services.ConfigureInvalidModelStateResponseService();
            services.AddRedisService(configuration);

            return services;    
        }


        private static IServiceCollection AddBuildService(this IServiceCollection services) {


            services.AddControllers();



           return services;
        }   private static IServiceCollection AddSwaggerdService(this IServiceCollection services) {

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();



            return services;
        } 
        private static IServiceCollection AddDataBaseCOnnectiondService(this IServiceCollection services,IConfiguration configuration) {


            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });


            return services;
        }
         private static IServiceCollection AddUSerDEfinedCOnnectiondService(this IServiceCollection services) {


           services.AddScoped<IServiceProduct, ProductService>();
           services.AddScoped<IUnitOfWork, UnitOfWork>();
           services.AddScoped<IBusketRepository, BusketRepository>();


            return services;
        } 
        private static IServiceCollection AddAutoMaperCOnnectiondService(this IServiceCollection services, IConfiguration configuration) {


            services.AddAutoMapper(M => M.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(M => M.AddProfile(new BusketProfile()));


            return services;
        }
        private static IServiceCollection ConfigureInvalidModelStateResponseService(this IServiceCollection services) {


            services.Configure<ApiBehaviorOptions>(options => {

                options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var error = ActionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                                       .SelectMany(p => p.Value.Errors)
                                                       .Select(E => E.ErrorMessage)
                                                       .ToArray();


                    var response = new ValidationApiErrorResponse()
                    {
                        Errors = error
                    };

                    return new BadRequestObjectResult(response);
                };







            });




            return services;
        }

        private static IServiceCollection AddRedisService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IConnectionMultiplexer>((ServiceProvider) =>
            {
                var connect = configuration.GetConnectionString("Redis");


                return ConnectionMultiplexer.Connect(connect);


            });





            return services;
        }
    }

}
