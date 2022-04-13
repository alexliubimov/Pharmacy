using System.ComponentModel.DataAnnotations;
using System.Net;
using MediatR;
using Pharmacy.Common.Serialization;
using Pharmacy.Domain.Commands.CreatePharmacy;
using Pharmacy.Domain.DomainEvents;
using Pharmacy.Handlers;

namespace Pharmacy.Web.API
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

            services.AddSingleton<IEventSerializer>(new JsonEventSerializer(new[]
            {
                typeof(PharmacyEvents.PharmacyCreated).Assembly
            }))
                .AddInfrastructure(this.Configuration);

            services.AddScoped<ServiceFactory>(ctx => ctx.GetRequiredService);
            services.AddScoped<IMediator, Mediator>();

            
            services.Scan(scan =>
            {
                scan.FromAssembliesOf(typeof(CreatePharmacyCommand))
                    .RegisterHandlers(typeof(IRequestHandler<>))
                    .RegisterHandlers(typeof(IRequestHandler<,>))
                    .RegisterHandlers(typeof(INotificationHandler<>));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
