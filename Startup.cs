using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SellYourCar.DBContext_Related;
using SellYourCar.Entities;
using SellYourCar.Repos;

namespace SellYourCar
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            _cfg = builder.Build();
        }

        IConfigurationRoot _cfg { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_cfg);
            services.AddScoped<IMakeCarRepo, MakeCarRepo>();
            services.AddScoped<IModelCarRepo, ModelCarRepo>();
            services.AddScoped<IAdvertiseCarRepo, AdvertiseCarRepo>();
            services.AddDbContext<MyContext>(ServiceLifetime.Scoped);
            services.AddTransient<CarDbInitializer>();
            services.AddIdentity<CarAdderUser, IdentityRole>()
              .AddEntityFrameworkStores<MyContext>();
            
               
        
            
            services.AddAutoMapper();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddCors();


            // Add framework services.
            services.AddMvc().AddJsonOptions(opt=>{
             opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
        ILoggerFactory loggerFactory, CarDbInitializer initializerDb)
        {
            loggerFactory.AddConsole(_cfg.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseCors(cfg=>{
                cfg.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
             });
            app.UseIdentity();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            ///use this for my photos
            app.UseStaticFiles();
            
            app.UseMvc();
            initializerDb.Seed().Wait();
        }
    }
}
