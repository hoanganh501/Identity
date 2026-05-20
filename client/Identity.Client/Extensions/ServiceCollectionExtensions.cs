using Identity.Client.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Identity.Client.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityClient(
            this IServiceCollection services,
            string baseUrl)
        {
            services
                .AddRefitClient<IIdentityClient>()
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new Uri(baseUrl);
                });

            return services;
        }
    }
}
