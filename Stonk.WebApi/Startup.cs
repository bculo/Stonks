using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stonk.Application;
using Stonk.Application.Contracts.Clients;
using Stonk.Application.Contracts.Modules;
using Stonk.Application.Contracts.Services;
using Stonk.Application.Modules;
using Stonk.Application.Options;
using Stonk.Service.Clients;
using Stonk.Service.Services;

namespace Stonk.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAutoMapper(typeof(ApplicationAssemblyReference).Assembly);

            services.AddOptions();
            services.Configure<PremarketOptions>(Configuration.GetSection("PremarketOptions"));
            services.Configure<FinVizOptions>(Configuration.GetSection("FinVizOptions"));

            services.AddHttpClient<IPremarketClient, MarketWatchClient>();
            services.AddHttpClient<IStonkInfoClient, FinVizStockClient>();
            //services.AddHttpClient<IStonkInfoClient, YahooFinanceClient>();

            services.AddScoped<IHtmlLookupService, HtmlLookupService>();
            services.AddScoped<IPremarketStockService, PremarketStockService>();
            services.AddScoped<IStockInfoClientFactory, StockInfoClientFactory>();
            services.AddScoped<IStonkInformationService, StonkInformationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
