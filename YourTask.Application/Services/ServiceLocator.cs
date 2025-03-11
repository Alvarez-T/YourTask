using Microsoft.Extensions.DependencyInjection;

namespace YourTask.Application.Services
{
    public static class ServiceLocator
    {
        private static IServiceProvider? _serviceProvider;

        // Initialize the service provider (call once at startup)
        public static void Initialize(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        // Resolve a service
        public static T GetService<T>() where T : class
        {
            if (_serviceProvider is null)
                throw new InvalidOperationException("ServiceProvider não inicializado.");

            return _serviceProvider.GetService<T>()!;
        }
    }
}
