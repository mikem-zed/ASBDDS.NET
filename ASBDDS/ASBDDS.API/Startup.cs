using ASBDDS.Shared.Models.Database.DataDb;
using ASBDDS.API.Models;
using ASBDDS.API.Servers.TFTP;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;

namespace ASBDDS.NET
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            TftpServer = new TFTPServer("tftp_root");
            TftpServer.Start();
        }

        public IConfiguration Configuration { get; }
        public TFTPServer TftpServer { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DataDbConnection"),
                    x => x.MigrationsAssembly("ASBDDS.API")));
            services.AddTransient(provider => { return TftpServer; });
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ASBDDS.NET", Version = "v1" });
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "ASBDDS.Shared.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseSwagger();
            //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASBDDS.NET v1"));

            // Apply unapplied migrations when the service starts, or create a database if it doesn't exist. 
            //If no users are found, default users from config will be created.
            var serviceProvider = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider;
            SeedDatabase.Initialize(serviceProvider, Configuration);

            app.UseHttpsRedirection();

            app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/admin"), adminPanel =>
            {
                adminPanel.UseBlazorFrameworkFiles("/admin");
                adminPanel.UseStaticFiles("/admin");
                adminPanel.UseRouting();
                adminPanel.UseEndpoints(endpoints =>
                {
                      endpoints.MapControllers();
                    endpoints.MapFallbackToFile("/admin/{*path:nonfile}",
                        "admin/index.html");
                });
            });

            app.MapWhen(ctx => !ctx.Request.Path.StartsWithSegments("/admin"), userPanel =>
            {
                userPanel.UseBlazorFrameworkFiles();
                userPanel.UseStaticFiles();
                userPanel.UseSwagger();
                userPanel.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASBDDS.NET v1"));
                userPanel.UseRouting();
                userPanel.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapFallbackToFile("/{*path:nonfile}",
                        "index.html");
                });
            });
        }
    }
}
