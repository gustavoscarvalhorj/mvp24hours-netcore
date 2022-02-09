//=====================================================================================
// Developed by Kallebe Lins (https://github.com/kallebelins)
//=====================================================================================
// Reproduction or sharing is free! Contribute to a better world!
//=====================================================================================
using Microsoft.Extensions.DependencyInjection;
using Mvp24Hours.Helpers;

namespace Mvp24Hours.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceBuilderExtensions
    {
        /// <summary>
        /// Create a ServiceProvider
        /// </summary>
        public static IServiceCollection UseMvp24Hours(this IServiceCollection services, ServiceProviderOptions options = null)
        {
            if (options == null)
            {
                ServiceProviderHelper.SetProvider(services.BuildServiceProvider());
            }
            else
            {
                ServiceProviderHelper.SetProvider(services.BuildServiceProvider(options));
            }
            return services;
        }
    }
}
