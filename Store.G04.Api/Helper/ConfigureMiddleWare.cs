using Microsoft.AspNetCore.Builder;
using Store.G04.Api.Middleware;
using Store.G04.Repository.Data.Contexts;
using Store.G04.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Store.G04.Repository.Identity.Context;
using Microsoft.AspNetCore.Identity;
using Store.G04.Core.Entities.Identity;
using Store.G04.Repository.Identity;

namespace Store.G04.Api.Helper
{
    public static class ConfigureMiddleWare
    {
        public static async Task<WebApplication> ConfigureMiddlewares(this WebApplication app)
        {




          //  app.UseMiddleware<ExceptionMiddleWare>();//configure userDefined MiddleWare




            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var context = service.GetRequiredService<StoreDbContext>();
            var contextIdentity = service.GetRequiredService<StoreIdentityDbContext>();
            var USerManger = service.GetRequiredService<UserManager<AppUser>>();
            var looger = service.GetRequiredService<ILoggerFactory>();
            try
            {
                await context.Database.MigrateAsync();
                await StoreDbContextSeed.SeedAsync(context);

                await contextIdentity.Database.MigrateAsync();
                await StoreIdentityDbContextSeed.SeedAppUserAsync(USerManger);

            }

            catch (Exception ex)
            {

                var loger = looger.CreateLogger<Program>();
                loger.LogError(ex, "there are problem during apply migrations !");

            }




            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithReExecute("/error/{0}");





            app.UseStaticFiles();

            app.UseHttpsRedirection();


            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();



            return app;
        
        }





    }
}
