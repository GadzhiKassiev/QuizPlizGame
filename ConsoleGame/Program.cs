using QuizPlizGame;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddScoped<IDisplayer, Screen>();
            services.AddScoped<IController, ConsoleController>();
            services.AddScoped<IStorageProvider, StorageProvider>();
            services.AddScoped<Game>();
            var application = services.BuildServiceProvider();
            using (var scope = application.CreateScope())
            {
                var game = scope.ServiceProvider.GetService<Game>();
                game.Start();
            }
        }
    }
}
