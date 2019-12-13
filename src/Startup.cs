using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using src.Context;
using src.Model;

namespace src
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var endpoint = Configuration["Endpoint"];
            var key = Configuration["Key"];
            var database = Configuration["Database"];
            services.AddDbContext<MyContext>(options =>
                {
                    options.UseCosmos(endpoint, key, database);
                    options.UseLazyLoadingProxies();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            using (var dbContext = scope.ServiceProvider.GetService<MyContext>())
            {
                dbContext.Database.EnsureCreated();
            }
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/pushdata", async context =>
                {
                    using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                    using (var dbContext = scope.ServiceProvider.GetService<MyContext>())
                    {
                        var site = dbContext.Sites.FirstOrDefault();
                        if(site == null)
                        {
                            site = new Site();
                            var building = new Building();
                            building.Rooms.Add(new Room("Living"));
                            building.Devices.Add(new Device("Phone"));
                            site.Buildings.Add(building);
                            dbContext.Sites.Add(site);
                            await dbContext.SaveChangesAsync();
                        }
                        await context.Response.WriteAsync("Done");
                    }
                });
                endpoints.MapGet("data", async context =>
                {
                    using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                    using (var dbContext = scope.ServiceProvider.GetService<MyContext>())
                    {
                        var site = dbContext.Sites.FirstOrDefault();
                        var json = JsonConvert.SerializeObject(site);
                        await context.Response.WriteAsync(json);
                    }
                });
            });
        }
    }
}
