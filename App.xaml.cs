using Cronos.ApiClients;
using Cronos.Platforms.Windows;
using Cronos.Services;
using Cronos.Views;
using System.Diagnostics;
using Cronos.Models;
using static Cronos.Services.InMemoryCacheService;

namespace Cronos
{
    public partial class App : Application
    {
        private readonly TokenApiClient _tokenClient;

        public static DataCacheService<EquipamentosCompostoModel>? DataCachePages { get; private set; }
        public static InMemoryCacheService? DataCacheAll { get; private set; }

        public App()
        {
            InitializeComponent();

            DataCachePages = new DataCacheService<EquipamentosCompostoModel>();
            DataCacheAll = new InMemoryCacheService();
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IPreferencesService, PreferencesService>();
            ServiceLocator.ServiceProvider = serviceCollection.BuildServiceProvider();
            _tokenClient = new TokenApiClient();

            // Tratamento de exceções de forma global
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                Exception ex = (Exception)args.ExceptionObject;
                
                 Debug.WriteLine($"Erro não tratado: {ex.Message}");
            };

            MainPage = new SplashPage();
        }
        protected override async void OnStart()
        {
            base.OnStart();

            await Task.Delay(3000); 

            bool isUserLoggedIn = await VerificarTokenAsync();

            if (isUserLoggedIn)
            {
                // Se o usuário estiver logado, navegue para o AppShell
                MainPage = new AppShell();
            }
            else
            {
                // Se o usuário não estiver logado, navegue para login
                MainPage = new NavigationPage(new LoginPage());
            }
        }
        private async Task<bool>VerificarTokenAsync()
        {
            var token = await SecureStorage.GetAsync("KeyToken");

            if (!string.IsNullOrEmpty(token))
            {
                bool isValid = await _tokenClient.VerificarTokenAsync(token);

                return isValid;
            }
            return false;
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            Window window = base.CreateWindow(activationState);
            window.Activated += Window_Activated;
            return window;
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            const int DefaultWidth = 1240;
            const int DefaultHeight = 720;

            if (sender is Window window)
            {
                // Define o tamanho de abertura da tela
                window.Width = DefaultWidth;
                window.Height = DefaultHeight;
                // Define o tamanho mínimo da tela
                window.MinimumWidth = 720;
                window.MinimumHeight = 480;
                // Aguarda um tempo para completar a tarefa de redimensionamento da janela
                window.Dispatcher.Dispatch(() => { });
                var disp = DeviceDisplay.Current.MainDisplayInfo;
                // Abre a tela no centro da tela
                window.X = (disp.Width / disp.Density - window.Width) / 2;
                window.Y = (disp.Height / disp.Density - window.Height) / 2;
            }
        }
    }
}
