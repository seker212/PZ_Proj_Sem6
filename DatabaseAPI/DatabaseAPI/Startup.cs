using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAPI.Services;
using DatabaseAPI.DAL;


namespace DatabaseAPI
{
    public class Startup
    {
        public static IDictionary<string, DatabaseModels.User> ActiveSessions = new Dictionary<string, DatabaseModels.User>();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private const string ConnectionString = "Host=localhost;Username=postgres;Password=mysecretpassword;Database=mydatabase;Port=5432";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DatabaseAPI", Version = "v1" });
            });
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.ToString());
            });

            services.AddScoped<IUserRepository>(x => new UserRepository(ConnectionString));
            services.AddScoped<IOrderRepository>(x => new OrderRepository(ConnectionString));
            services.AddScoped<ICashierRepository>(x => new CashierRepository(ConnectionString));
            services.AddScoped<IProductRepository>(x => new ProductRepository(ConnectionString));
            services.AddScoped<IDiscountRepository>(x => new DiscountRepository(ConnectionString));
            services.AddScoped<IOrderDiscountRepository>(x => new OrderDiscountRepository(ConnectionString));
            services.AddScoped<IPairRepository<DatabaseModels.OrderDiscount>>(x => new OrderDiscountRepository(ConnectionString));
            services.AddScoped<IPairRepository<DatabaseModels.OrderItems>>(x => new PairRepository<DatabaseModels.OrderItems>(ConnectionString, "order_items", DatabaseModels.OrderItems.ColumnNames));
            services.AddScoped<IPairRepository<DatabaseModels.DiscountSetItem>>(x => new PairRepository<DatabaseModels.DiscountSetItem>(ConnectionString, "discounts_set_items", DatabaseModels.DiscountSetItem.ColumnNames));

            services.AddScoped<ICrudPairServices<DatabaseModels.OrderDiscount>, CrudPairServices<DatabaseModels.OrderDiscount>>();
            services.AddScoped<ICrudPairServices<DatabaseModels.OrderItems>, CrudPairServices<DatabaseModels.OrderItems>>();
            services.AddScoped<ICrudPairServices<DatabaseModels.DiscountSetItem>, CrudPairServices<DatabaseModels.DiscountSetItem>>();
            services.AddScoped<ICrudCashierServices, CrudCashierServices>();
            services.AddScoped<ICrudProductServices, CrudProductServices>();
            services.AddScoped<ICrudOrderServices, CrudOrderServices>();
            services.AddScoped<ICrudDiscountServices, CrudDiscountServices>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<IDiscountServices, DiscountServices>();
            services.AddScoped<IUserServices, UserServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DatabaseAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
