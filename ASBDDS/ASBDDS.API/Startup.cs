using System;
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
using ASBDDS.API.Servers.DHCP;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ASBDDS.NET
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            TFTPServer = new TFTPServer();
            TFTPServer.Start();
            DHCPServer = new DHCPServer(Configuration.GetValue<string>("Networking:IP"));
            //DHCPServer.Start();
        }

        public IConfiguration Configuration { get; }
        public TFTPServer TFTPServer { get; }
        public DHCPServer DHCPServer { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DataDbConnection"),
                x => x.MigrationsAssembly("ASBDDS.API")));
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<DataDbContext>();
            services.AddTransient(provider => { return TFTPServer; });
            services.AddTransient(provider => { return DHCPServer; });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // укзывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = AuthOptions.ISSUER,
 
                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudience = AuthOptions.AUDIENCE,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,
 
                        // установка ключа безопасности
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };
                });
            
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ASBDDS.NET", Version = "v1" });
                var filePathShared = Path.Combine(System.AppContext.BaseDirectory, "ASBDDS.Shared.xml");
                var filePathApi = Path.Combine(System.AppContext.BaseDirectory, "ASBDDS.API.xml");
                c.IncludeXmlComments(filePathShared);
                c.IncludeXmlComments(filePathApi);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    In = ParameterLocation.Header, 
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey 
                });

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
                adminPanel.UseAuthentication();
                adminPanel.UseAuthorization();
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
                userPanel.UseAuthentication();
                userPanel.UseAuthorization();
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
