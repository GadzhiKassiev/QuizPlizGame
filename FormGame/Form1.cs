using Microsoft.Extensions.DependencyInjection;
using QuizPlizGame;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Threading;
using System.Windows.Forms;

namespace FormGame
{
    public partial class Form1 : Form
    {
        Thread _gameLogicThread;
        public static Form1 Instance;
        public AutoResetEvent GetInput;
        public ConsoleKey? Key;

        public Form1()
        {
            InitializeComponent();
            Instance = this;
            Key = null;
            GetInput = new AutoResetEvent(false);
        }

        public TextBox TextBox
        {
            get { return textBox1; }
        }

        public TextBox CloakBox
        {
            get { return cloakBox; }
        }

        public Button StartButton
        {
            get { return startButton; }
        }

        public Button ReportButton
        {
            get { return reportButton; }
        }

        public Button ExitButton
        {
            get { return exitButton; }
        }

        public Button ButtonOne
        {
            get { return button1; }
        }

        public Button ButtonTwo
        {
            get { return button2; }
        }

        public Button ButtonThree
        {
            get { return button3; }
        }

        public Button ButtonFour
        {
            get { return button4; }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Application.Idle += Application_Idle;
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            Application.Idle -= Application_Idle;

            var services = new ServiceCollection();
            services.AddScoped<IDisplayer, FormScreen>();
            services.AddScoped<IController, FormController>();
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
                _gameLogicThread = new Thread(game.Start);
                _gameLogicThread.IsBackground = true;
                _gameLogicThread.Start();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AssignKey(ConsoleKey.D1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AssignKey(ConsoleKey.D2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AssignKey(ConsoleKey.D3);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            AssignKey(ConsoleKey.D4);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            AssignKey(ConsoleKey.Y);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            AssignKey(ConsoleKey.R);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AssignKey(ConsoleKey.N);
        }

        private void AssignKey(ConsoleKey key)
        {
            Key = key;
            GetInput.Set();
        }
    }
    public class NameValueCollectionOptions
    {
        public const string SectionName = "appSettings";
        public NameValueCollection MyCollection { get; set; }
    }
}
