using Microsoft.Extensions.DependencyInjection;

namespace Migratedata
{
    internal static class Program
    {
        private static IServiceProvider _serviceProvider;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var services = new ServiceCollection();

            ConfigureService(services);
            _serviceProvider = services.BuildServiceProvider();
            ApplicationConfiguration.Initialize();
            Application.Run(_serviceProvider.GetRequiredService<Form1>());
        }
        private static void ConfigureService(IServiceCollection services)
        {
            services.AddScoped<IDbStructureService, DbStructureService>();
            services.AddTransient<Form1>();
        }
    }
}