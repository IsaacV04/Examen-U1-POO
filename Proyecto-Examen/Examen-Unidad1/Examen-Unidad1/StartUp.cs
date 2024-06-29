using Examen_Unidad1.Services;
using Examen_Unidad1.Services.Interfaces;

namespace Examen_Unidad1
{
    public class StarUp
    {
        private IConfiguration Configuration { get; }

        public StarUp (IConfiguration configuration) 
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddTransient<IOrdersService, OrdersService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
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
