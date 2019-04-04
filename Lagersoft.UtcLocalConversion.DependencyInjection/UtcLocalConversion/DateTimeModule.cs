using Microsoft.Extensions.DependencyInjection;
using NodaTime;

namespace Lagersoft.UtcLocalConversion.DependencyInjection.UtcLocalConversion
{
    public static class DateTimeModule
    {
        /// <summary>
        /// Prepares the instance DateTimeLocalizerService to be consumed
        /// in a net core environment with dependency injection.
        /// </summary>
        /// <param name="services">The service collection</param>
        public static void AddUtcLocalConversion(this IServiceCollection services)
        {
            services.AddSingleton<IClock>(SystemClock.Instance);
            services.AddSingleton<DateTimeLocalizerService>();
        }
    }
}