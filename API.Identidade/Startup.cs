using API.Core.Identity;
using API.Identidade.Configuration;
using Core.Bus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Identidade
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
            services.AddApiConfiguration();

            services.AddIdentityConfiguration(Configuration);

            services.AddJwtConfiguration(Configuration);

            services.AddSingleton<IMessageBus>(
                new MessageBus(Configuration.GetSection("MessageQueueConnection:MessageBus").Value)
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseApiConfiguration(env);
        }
    }
}
