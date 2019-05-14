using Microsoft.Extensions.DependencyInjection;
using Repository.Contracts;
using Repository.Implementations;

namespace Repository
{
    public class Bootstrapper
    {
        public static void Initialize(
            IServiceCollection services,
            BootstrapperOptions options)
        {
            services.AddTransient(x => new IdeasContext(options.ConnectionString, options.DatabaseName));
            services.AddTransient<IIdeaRepository, IdeaRepository>();
        }
    }
}