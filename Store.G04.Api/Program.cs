
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Store.G04.Api.Errors;
using Store.G04.Api.Helper;
using Store.G04.Api.Middleware;
using Store.G04.Core;
using Store.G04.Core.Mapping.Products;
using Store.G04.Core.ServicesContract;
using Store.G04.Repository.Data;
using Store.G04.Repository.Data.Contexts;
using Store.G04.Repository.Repositories;
using Store.G04.Service.Services.Products;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Store.G04.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDependency(builder.Configuration);

            var app = builder.Build();

           await app.ConfigureMiddlewares();




        }
    }
}
