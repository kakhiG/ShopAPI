using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using ShopApi.Repositories;
using ShopAPI.Data;

namespace ShopApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<ShopDataContext>(opts =>
                opts.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("shop-api", new Info
                {
                    Title = " ShopAPI",
                    Description = "Sample API"
                });
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/shop-api/swagger.json", "ShopAPI");
            });
        }
    }
}
