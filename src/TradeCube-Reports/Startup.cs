using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TradeCube_Reports.Configuration;
using TradeCube_Reports.Services;

namespace TradeCube_Reports
{
    public class Startup
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpClient();
            services.AddHttpClient<TradeCubeApiService>();

            services.AddApiVersioning(v =>
            {
                v.AssumeDefaultVersionWhenUnspecified = true;
                v.DefaultApiVersion = new ApiVersion(1, 0);
                v.ApiVersionSelector = new CurrentImplementationApiVersionSelector(v);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TradeCube-Reports API", Version = "v1", Description = "TradeCube-Reports API" });
            });

            services.AddScoped<ITradeCubeConfiguration, TradeCubeConfiguration>();

            // Services
            services.AddScoped<IConfirmationReportService, ConfirmationReportReportService>();
            services.AddScoped<ICountryLookupService, CountryLookupService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IReportTemplateService, ReportTemplateService>();
            services.AddScoped<IReportRenderService, ReportRenderService>();
            services.AddScoped<ITradeService, TradeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TradeCube-Reports API v1");
            });

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
