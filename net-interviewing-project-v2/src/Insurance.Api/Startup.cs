using Insurance.Api.Middleware;
using Insurance.Application.Services;
using Insurance.Application.Services.Interfaces;
using Insurance.Domain.Interfaces.Repositories;
using Insurance.Infrastructure.Repositories.MongoDb;
using Insurance.Infrastructure.Repositories.MongoDb.Context;
using Insurance.Infrastructure.Repositories.MongoDb.Dtos;
using Insurance.Infrastructure.Repositories.ProductApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Insurance.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<ExceptionHandlingMiddleware>();

            var productApiUrl = Configuration["ProductApiUrl"];
            services.AddHttpClient("ProductApiClient", client =>
            {
                client.BaseAddress = new Uri(productApiUrl);
            });
            services.AddSingleton<IProductRepository, ProductRepository>();

            SurchargeMap.Configure();
            var mongoConfig = new MongoDBConfig();
            Configuration.Bind("MongoDB", mongoConfig);
            services.AddSingleton(mongoConfig);
            var mongoContext = new InsuranceContext(mongoConfig);
            var surchargeMongoRepository = new SurchargeDbRepository(mongoContext);
            services.AddSingleton<ISurchargeRepository>(surchargeMongoRepository);



            services.AddScoped<IProductInsuranceService, ProductInsuranceService>();
            services.AddScoped<ISurchargeService, SurchargeService>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

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