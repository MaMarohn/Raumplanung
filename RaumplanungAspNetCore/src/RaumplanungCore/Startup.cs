using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RaumplanungCore.Database;
using Microsoft.EntityFrameworkCore;
using Raumplanung.Database;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RaumplanungCore.ViewModels.Reservation;

namespace RaumplanungCore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvcCore(); // ?
            services.AddMvc();
            services.AddScoped<NewModel, NewModel>();
            services.AddDbContext<ReservationContext>(
                options =>
                    options.UseSqlServer(
                        "Server=(localdb)\\mssqllocaldb;Database=Reservation;Trusted_Connection=True;MultipleActiveResultSets=true"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ReservationContext reservationContext)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=reservation}/{action=Index}");
            });

            /* app.Run(async (context) =>
              {
                  await context.Response.WriteAsync("Hello World!");
              });*/




            DbInitializer.Initialize(reservationContext);
        }
    }
}
