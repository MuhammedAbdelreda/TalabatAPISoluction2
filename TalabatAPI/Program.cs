using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using System.Security.Cryptography.X509Certificates;
using Talabat;
using TalabatAPI.Helpers;

namespace TalabatAPI
{
    public class Program
    {
        private static readonly IConfiguration configuration;

        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            /*            var host = CreatHostBuilder(args).Build();
                        var scope = host.Services.CreateScope();

                        StoreContext context = new StoreContext();
                        context.Database.MigrateAsync();*/

            // Add services to the container.
            #region For Updating DataBase without Manager Console
            //CreateHostBuilder(args).Build().Run();
            IHostBuilder CreateHostBuilder(string[] args)
            {
                return Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(async webBuilder =>
               {
                   var host = CreateHostBuilder(args).Build();
                   using var scope = host.Services.CreateScope();
                   var services = scope.ServiceProvider;
                   var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                   try
                   {
                       #region For Update DataBase (StoreContext)
                       var context = services.GetRequiredService<StoreContext>();
                       await context.Database.MigrateAsync();
                       #endregion
                       #region DataSeeding (ContextSeed)
                       await ContextSeed.SeedAsync(context, loggerFactory); 
                       #endregion
                       #region For Update DataBase (AppIdentityDbContext)
                       var identityContext = services.GetRequiredService<AppIdentityDbContext>();
                       await identityContext.Database.MigrateAsync(); //for update 
                       #endregion
                       #region DataSeeding (AppIdentityDbContextSeed)
                       var userManager = services.GetRequiredService<UserManager<AppUser>>();
                       await AppIdentityDbContextSeed.SeedUserAsync(userManager); 
                       #endregion
                   }
                   catch (Exception ex)
                   {
                       var logger = loggerFactory.CreateLogger<Program>();
                       logger.LogError(ex, "An Error Occured");
                   }
               });
            }
            #endregion

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #region Call OnConfiguring Automatically
            builder.Services.AddDbContext<StoreContext>(options =>
    {
        //options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("appsettings.json");
        var configuration = builder.Build();
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }
    ); 
            #endregion
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); //for controller
            builder.Services.AddAutoMapper(typeof(MappingProfiles)); //for mapper
            #region Call OnConfiguring Automatically for AppIdentityDbContext
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    {
        //options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("appsettings.json");
        var configuration = builder.Build();
        options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));

    }
    );
            #endregion
            builder.Services.AddIdentityServices(configuration); //for IdentityService(seed)
            builder.Services.AddScoped(typeof(ITokenService), typeof(TokenService)); //for controller
            builder.Services.AddScoped(typeof(IOrderService), typeof(OrderService));
            builder.Services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));
           
var app = builder.Build();
            #region ..
/*            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = services.GetRequiredService<StoreContext>();
                context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex,ex.Message);
            }

            StoreContext context = new StoreContext(); */
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseStaticFiles(); //for PictureResolver
            app.UseAuthentication();


            app.MapControllers();

            app.Run();
        }
    }
}
