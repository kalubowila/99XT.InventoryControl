using _99XT.InventoryControl.LoggerService.ILoggerCore;
using _99XT.InventoryControl.LoggerService.LoggerCore;
using Microsoft.Extensions.DependencyInjection;

namespace _99XT.InventoryControl.App.Extensions
{
    public static class ConfigureLogger
    {
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}
