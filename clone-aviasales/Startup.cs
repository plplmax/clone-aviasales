using clone_aviasales.Data.Repository;
using clone_aviasales.Data.Source;
using clone_aviasales.Domain.Interactors;
using clone_aviasales.Domain.Repository;
using clone_aviasales.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace clone_aviasales
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
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddControllersWithViews(options => options.ModelBinderProviders.Insert(0, new CustomTicketsFiltersModelBinderProvider()));
            services.AddTransient<FetchTicketsInteractor>();
            services.AddTransient<FilterTicketsInteractor>();
            services.AddSingleton<ITicketsRepository, TicketsRepositoryImpl>();
            services.AddSingleton<ICitiesRepository, CitiesRepositoryImpl>();
            services.AddSingleton<IAirlinesRepository, AirlinesRepositoryImpl>();
            services.AddSingleton<TicketsCloudDataSource>();
            services.AddSingleton<CitiesCacheDataSource>();
            services.AddSingleton<AirlinesCacheDataSource>();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
