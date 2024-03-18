using Microsoft.Extensions.DependencyInjection;
using QuizPlizGame;
using System.Collections.Specialized;
using System.Configuration;

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

            var myCollection = ConfigurationManager.GetSection(NameValueCollectionOptions.SectionName) as NameValueCollection;

            services.AddSingleton(myCollection);

            services.Configure<NameValueCollectionOptions>(options =>
            {
                options.MyCollection = myCollection;
            });

            var application = services.BuildServiceProvider();
            using (var scope = application.CreateScope())
            {
                var game = scope.ServiceProvider.GetService<Game>();
                game.Start();
            }
        }

        public class NameValueCollectionOptions
        {
            public const string SectionName = "appSettings";
            public NameValueCollection MyCollection { get; set; }
        }
    }
}
