using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Cronos.ApiClients;
using Cronos.Models;
using Cronos.Services;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Windows.ApplicationModel.Contacts;
using Windows.Media.Capture;

namespace Cronos.ViewModels
{
    public class NavegacaoViewModel : ObservableObject
    {
        #region VARIAVEIS
        private readonly ObterDadosMenuApiClient _obterDadosMenuApiClient;
        private readonly InMemoryCacheService _dataCacheall;
        private readonly IPreferencesService _preferencesService;
        private string? _LogoMenuSuperior;
        private string? _LogoMenuEsquerdo;
        private string? _AvatarUsuarioMenu;
        private string? _NomeUsuarioMenu;
        private string? _EmailUsuarioMenu;
        private string? _SaveIDusuario;
        private bool _estadoButtons = true;
        private bool _IconLinha1 = true;
        private bool _IconLinha2 = false;
        private bool _IconLinha3 = false;
        private bool _IconLinha4 = false;
        private bool _IconLinha5 = false;
        private bool _IconLinha6 = false;
        private bool _IconLinha7 = false;
        private bool _IconLinha8 = false;
        private string? _IconsCor1 = "#8A8A8A";
        private string? _IconsCor2 = "#8A8A8A";
        private string? _IconsCor3 = "#8A8A8A";
        private string? _IconsCor4 = "#8A8A8A";
        private string? _IconsCor5 = "#8A8A8A";
        private string? _IconsCor6 = "#8A8A8A";
        private string? _IconsCor7 = "#8A8A8A";
        private string? _IconsCor8 = "#8A8A8A";
        private Label? _LoadingEfeito;
        #endregion
        #region CONSTRUTOR    
        public NavegacaoViewModel()
        {
            _obterDadosMenuApiClient = new ObterDadosMenuApiClient();
            _preferencesService = ServiceLocator.ServiceProvider.GetRequiredService<IPreferencesService>();
            _dataCacheall = App.DataCacheAll ?? new InMemoryCacheService();
            LoadingEfeito = new Label { Opacity = 0 };
            ShowDetalhesUI();
        }
        #endregion
        #region OBJETOS
        public Label? LoadingEfeito
        {
            get => _LoadingEfeito;
            set => SetProperty(ref _LoadingEfeito, value);
        }
        public bool EstadoButtons
        {
            get => _estadoButtons;
            set => SetProperty(ref _estadoButtons, value);
        }
        public string SaveIDusuario
        {
            get => _SaveIDusuario ??= string.Empty;
            set => SetProperty(ref _SaveIDusuario, value);
        }
        public string LogoMenuSuperiro
        {
            get => _LogoMenuSuperior ??= string.Empty;
            set => SetProperty(ref _LogoMenuSuperior, value);
        }
        public string LogoMenuEsquerdo
        {
            get => _LogoMenuEsquerdo ??= string.Empty;
            set => SetProperty(ref _LogoMenuEsquerdo, value);
        }
        public string AvatarUsuarioMenu
        {
            get => _AvatarUsuarioMenu ??= string.Empty;
            set => SetProperty(ref _AvatarUsuarioMenu, value);
        }
        public string NomeUsuarioMenu
        {
            get => _NomeUsuarioMenu ??= string.Empty;
            set => SetProperty(ref _NomeUsuarioMenu, value);
        }
        public string EmailUsuarioMenu
        {
            get => _EmailUsuarioMenu ??= string.Empty;
            set => SetProperty(ref _EmailUsuarioMenu, value);
        }
        public bool IconLinha1
        {
            get => _IconLinha1;
            set => SetProperty(ref _IconLinha1, value);
        }
        public bool IconLinha2
        {
            get => _IconLinha2;
            set => SetProperty(ref _IconLinha2, value);
        }
        public bool IconLinha3
        {
            get => _IconLinha3;
            set => SetProperty(ref _IconLinha3, value);
        }
        public bool IconLinha4
        {
            get => _IconLinha4;
            set => SetProperty(ref _IconLinha4, value);
        }
        public bool IconLinha5
        {
            get => _IconLinha5;
            set => SetProperty(ref _IconLinha5, value);
        }
        public bool IconLinha6
        {
            get => _IconLinha6;
            set => SetProperty(ref _IconLinha6, value);
        }
        public bool IconLinha7
        {
            get => _IconLinha7;
            set => SetProperty(ref _IconLinha7, value);
        }
        public bool IconLinha8
        {
            get => _IconLinha8;
            set => SetProperty(ref _IconLinha8, value);
        }
        public string? IconsCor1
        {
            get => _IconsCor1 ??= string.Empty;
            private set => SetProperty(ref _IconsCor1, value);
        }
        public string? IconsCor2
        {
            get => _IconsCor2 ??= string.Empty;
            private set => SetProperty(ref _IconsCor2, value);
        }
        public string? IconsCor3
        {
            get => _IconsCor3 ??= string.Empty;
            private set => SetProperty(ref _IconsCor3, value);
        }
        public string? IconsCor4
        {
            get => _IconsCor4 ??= string.Empty;
            private set => SetProperty(ref _IconsCor4, value);
        }
        public string? IconsCor5
        {
            get => _IconsCor5 ??= string.Empty;
            private set => SetProperty(ref _IconsCor5, value);
        }
        public string? IconsCor6
        {
            get => _IconsCor6 ??= string.Empty;
            private set => SetProperty(ref _IconsCor6, value);
        }
        public string? IconsCor7
        {
            get => _IconsCor7 ??= string.Empty;
            private set => SetProperty(ref _IconsCor7, value);
        }
        public string? IconsCor8
        {
            get => _IconsCor8 ??= string.Empty;
            private set => SetProperty(ref _IconsCor8, value);
        }
        #endregion
        #region PROCESSOS
        private void EstadoLoading()
        {
            EstadoButtons = !EstadoButtons;
        }
        private async Task LoadPreferences()
        {
            if (_preferencesService == null) throw new InvalidOperationException("PreferencesService não foi configurado.");

            SaveIDusuario = await _preferencesService.GetIdUsuarioAsync();

            await Task.CompletedTask;
        }
        private async void ShowDetalhesUI()
        {
            try
            {
                var existeDadosMenu = _dataCacheall.ExisteNoCache("DadosMenu");

                if (existeDadosMenu)
                {
                    var dadosMenuCache = _dataCacheall.ObterDoCache<dynamic>("DadosMenu");

                    LogoMenuSuperiro = dadosMenuCache.URL_Imagem_Logo_Superior ?? string.Empty;
                    LogoMenuEsquerdo = dadosMenuCache.URL_Imagem_Logo_Esquerdo ?? string.Empty;
                    NomeUsuarioMenu = dadosMenuCache.Nome ?? string.Empty;
                    EmailUsuarioMenu = dadosMenuCache.Email ?? string.Empty;
                    AvatarUsuarioMenu = dadosMenuCache.URL_Image_Avatar ?? string.Empty;
                }
                else
                {
                    await LoadPreferences();

                    if (!string.IsNullOrEmpty(SaveIDusuario))
                    {
                        var idusuario = int.Parse(SaveIDusuario);

                        var dados = await _obterDadosMenuApiClient.ObterDadosMenuApiClientAsync(idusuario);

                        LogoMenuSuperiro = dados.URL_Imagem_Logo_Superior ?? string.Empty;
                        LogoMenuEsquerdo = dados.URL_Imagem_Logo_Esquerdo ?? string.Empty;
                        NomeUsuarioMenu = dados.Nome ?? string.Empty;
                        EmailUsuarioMenu = dados.Email ?? string.Empty;
                        AvatarUsuarioMenu = dados.URL_Image_Avatar ?? string.Empty;

                        // Armazenar informações de contato no cache
                        _dataCacheall.AdicionarAoCache("DadosMenu", dados);
                    }
                }          
            }
            catch (Exception)
            {
                // Erro de carregamento da informações de Menu
            }
        }
        private async Task ResetAllIcons()
        {
            EstadoLoading();

            var defaultColor = "#8A8A8A";
            // Define as cores dos ícones
            IconsCor1 = defaultColor;
            IconsCor2 = defaultColor;
            IconsCor3 = defaultColor;
            IconsCor4 = defaultColor;
            IconsCor5 = defaultColor;
            IconsCor6 = defaultColor;
            IconsCor7 = defaultColor;
            IconsCor8 = defaultColor;
            // Define as linhas dos ícones
            IconLinha1 = false;
            IconLinha2 = false;
            IconLinha3 = false;
            IconLinha4 = false;
            IconLinha5 = false;
            IconLinha6 = false;
            IconLinha7 = false;
            IconLinha8 = false;

            await Task.CompletedTask;
        }

        public async Task ShowDashboard()
        {
            await ResetAllIcons();
            IconLinha1 = !IconLinha1;
            IconsCor1 = "White";
            await Task.Delay(50);
            await NavigateTo("///MainPage");
        }
        public async Task ShowEquipamentos()
        {
            await ResetAllIcons();
            IconLinha2 = !IconLinha2;
            IconsCor2 = "White";
            await Task.Delay(50);
            await NavigateTo("//MainPage/EquipamentosPage");
        }
        public async Task ShowInserirInformacoesEquipamento()
        {
            await ResetAllIcons();
            IconLinha3 = !IconLinha3;
            IconsCor3 = "White";
            await Task.Delay(50);
            await NavigateTo("//MainPage/InserirInformacoesEquipamentoPage");
        }
        public async Task ShowHistorico()
        {
            await ResetAllIcons();
            IconLinha4 = !IconLinha4;
            IconsCor4 = "White";
            await Task.Delay(50);
            await NavigateTo("//MainPage/HistoricoPage");
        }
        public async Task ShowCriarConta()
        {
            await ResetAllIcons();
            IconLinha5 = !IconLinha5;
            IconsCor5 = "White";
            await Task.Delay(50);
            await NavigateTo("//MainPage/CriarContaPage");
        }
        public async Task ShowConfiguracaoEmpresa()
        {
            await ResetAllIcons();
            IconLinha6 = !IconLinha6;
            IconsCor6 = "White";
            await Task.Delay(50);
            await NavigateTo("//MainPage/ConfiguracaoEmpresaPage");
        }
        public async Task ShowConfiguracoesGerais()
        {
            await ResetAllIcons();
            IconLinha7 = !IconLinha7;
            IconsCor7 = "White";
            await Task.Delay(50);
            await NavigateTo("//MainPage/ConfiguracaoPage");
        }
        public async Task ShowAjuda()
        {
            await ResetAllIcons();
            IconLinha8 = !IconLinha8;
            IconsCor8 = "White";
            await Task.Delay(50);
            await NavigateTo("//MainPage/AjudaPage");
        }
        public async Task ShowNotificacoes()
        {
            await NavigateTo("//MainPage/NotificacoesPage");
        }

        private async Task NavigateTo(string route)
        {
            await Shell.Current.GoToAsync("TransitionPage", false);

            await Task.Delay(300);

            await Shell.Current.GoToAsync(route, false);

            EstadoLoading();
        }

        #endregion
        #region COMANDOS
        public ICommand Btn_ShowDashboard => new AsyncRelayCommand(ShowDashboard);
        public ICommand Btn_ShowEquipamentos => new AsyncRelayCommand(ShowEquipamentos);
        public ICommand Btn_ShowInserirInformacoesEquipamento => new AsyncRelayCommand(ShowInserirInformacoesEquipamento);
        public ICommand Btn_ShowHistorico => new AsyncRelayCommand(ShowHistorico);
        public ICommand Btn_ShowCriarConta => new AsyncRelayCommand(ShowCriarConta);
        public ICommand Btn_ShowConfiguracaoEmpresa => new AsyncRelayCommand(ShowConfiguracaoEmpresa);
        public ICommand Btn_ShowConfiguracoesGerais => new AsyncRelayCommand(ShowConfiguracoesGerais);
        public ICommand Btn_ShowAjuda => new AsyncRelayCommand(ShowAjuda);
        public ICommand Btn_ShowNotificacoesPage => new AsyncRelayCommand(ShowNotificacoes);

        #endregion
    }
}
