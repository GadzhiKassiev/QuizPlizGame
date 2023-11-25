using QuizPlizGame;
using System.Configuration;

namespace ConsoleGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StorageProvider sp = new StorageProvider(ConfigurationManager.AppSettings);
            var game = sp.GetService();
            game.Start();
        }
    }
}
