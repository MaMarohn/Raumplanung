using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using RaumplanungCore.Database;
using Microsoft.EntityFrameworkCore;
using RaumplanungCore.ViewModels.Reservation;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RaumplanungCore.Models;
using RaumplanungCore.Models.Roles;
using RaumplanungCore.Services;

namespace RaumplanungCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvcCore(); // ?
           
            services.AddScoped<NewModel, NewModel>();
            
            services.AddEntityFramework()
                .AddDbContext<ReservationContext>(
                options =>
                    options.UseSqlServer(
                        "Server=(localdb)\\mssqllocaldb;Database=Reservation;Trusted_Connection=True;MultipleActiveResultSets=true"));
            
            services.AddIdentity<Teacher, RoleAdmin>(options =>

                {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // Cookie settings
                options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(150);
                options.Cookies.ApplicationCookie.LoginPath = "/Account/LogIn";
                options.Cookies.ApplicationCookie.LogoutPath = "/Account/LogOff";

                // User settings
                options.User.RequireUniqueEmail = true;
            })
        .AddEntityFrameworkStores<ReservationContext>()
        .AddDefaultTokenProviders();

            services.AddTransient<IEmailSender, AuthMessageSender>();
            //services.AddTransient<ISmsSender, AuthMessageSender>();

            services.AddMvc();
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ConfirmedEmailOnly", policy => policy.RequireClaim("MailConfirmed","true"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ReservationContext reservationContext)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseIdentity();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}");
            });

            /* app.Run(async (context) =>
              {
                  await context.Response.WriteAsync("Hello World!");
              });*/


            

            DbInitializer.Initialize(reservationContext);
        }
    }
}
