using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;  //no idea why this is needed if I use the higher node too, but it's required for .TryAdd
using Microsoft.Extensions.Hosting;
using TennisBookings.Web.Configuration;
using TennisBookings.Web.Services;

namespace TennisBookings.Web
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
            //services.Configure<ExternalServicesConfig>(Configuration.GetSection("ExternalServices"));

            //services.AddHttpClient<IWeatherApiClient, WeatherApiClient>();

            services.AddSingleton<IWeatherForecaster, WeatherForecaster>();

            //var serviceDescriptor = new ServiceDescriptor(typeof(IWeatherForecaster), typeof(WeatherForecaster), ServiceLifetime.Singleton);
            //var serviceDescriptor = ServiceDescriptor.Describe(typeof(IWeatherForecaster), typeof(WeatherForecaster), ServiceLifetime.Singleton);
            //var serviceDescriptor = ServiceDescriptor.Singleton(typeof(IWeatherForecaster), typeof(WeatherForecaster));
            //var serviceDescriptor = ServiceDescriptor.Singleton<IWeatherForecaster, WeatherForecaster>();
            //services.Add(serviceDescriptor);

            //services.AddSingleton<IWeatherForecaster, AmazingWeatherForecaster>();
            //services.TryAddSingleton<IWeatherForecaster, AmazingWeatherForecaster>();
            //services.Replace(ServiceDescriptor.Singleton<IWeatherForecaster, AmazingWeatherForecaster>());
            //services.RemoveAll<IWeatherForecaster>();


            services.Configure<FeaturesConfiguration>(Configuration.GetSection("Features"));

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
