using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using web_api.DataAccess;
using web_api.DataAccess.Abstract;
using web_api.Models;
using web_api.Services;

namespace web_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddSingleton<IKeyVaultService, KeyVaultService>();


            var serviceProvider = services.BuildServiceProvider();
            var kvService = serviceProvider.GetService<IKeyVaultService>();
            var connectionStringSecret = Configuration.GetValue<string>("AppSettings:ConnectionStringName");

            var databaseType = Configuration.GetValue<string>("AppSettings:DatabaseType");
            if (kvService != null)
            {
                var connectionString = kvService.GetKeyVaultSecret(connectionStringSecret).Value;

                switch (databaseType.ToLower())
                {
                    case "mssql":

                        services.AddDbContext<DatabaseContext>(options =>
                            options.UseMySQL(connectionString));
                        break;
                    case "mysql":


                        services.AddDbContext<DatabaseContext>(options =>
                            options.UseSqlServer(connectionString,
                                sqlServerOptions => sqlServerOptions.CommandTimeout(120)));


                        break;
                }
            }


            services.AddScoped(typeof(IRepository<DatabaseClassExample>), typeof(Repository<DatabaseClassExample>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
            });
        }
    }
}