using _99XT.InventoryControl.App.Extensions;
using _99XT.InventoryControl.Core.Core;
using _99XT.InventoryControl.Core.ICore;
using _99XT.InventoryControl.DataAccess.DataAccess;
using _99XT.InventoryControl.DataAccess.IDataAccess;
using _99XT.InventoryControl.LoggerService.ILoggerCore;
using _99XT.InventoryControl.Models.DBContext;
using _99XT.InventoryControl.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;

namespace _99XT.InventoryControl.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<InventoryContext>(
                opts => opts.UseSqlServer(
                    Configuration["ConnectionString:InventoryDB"]
                    .Replace("{PASSWORD}", StringCipher.Decrypt(FileHandler.GetValueByKey("ConnectionStringPW")))
                    ));

            services.AddScoped<IInventoryCore, InventoryCore>()
                    .AddScoped<ISupplierCore, SupplierCore>()
                    .AddScoped<IInventoryDataManager, InventoryDataManager>()
                    .AddScoped<ISupplierDataManager, SupplierDataManager>();

            services.AddAuthenticationWithIdentityServer(Configuration);

            services.ConfigureLoggerService();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.ConfigureExceptionHandler(logger);
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
