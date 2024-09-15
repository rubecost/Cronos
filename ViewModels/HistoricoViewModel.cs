using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Cronos.Bases;
using Cronos.Models;
using Cronos.Services;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Windows.Input;
using Cronos.ApiClients;


namespace Cronos.ViewModels
{
    public class HistoricoViewModel : ObservableObject
    {
        #region VARIAVEIS
        private readonly IPreferencesService _preferencesService;
        private readonly ObterRegistroHistoricoApiClient _registroHistoricoApiClient;
        private readonly InMemoryCacheService.DataCacheService<RegistroHistoricoModel> _dataCacheService;
        private ObservableCollection<RegistroHistoricoModel>? _registros;
        private string? _SaveIDunidade;
        private int _paginaAtual = 1;
        private int _quatidadeDados = 1;
        private bool _bloquearConsultas = false;


        private bool _SemInternet = false;
        private bool _PageLoading = false;
        private string? _CorTema;
        private Button? _BackgroundBtn;
        #endregion

        #region CONSTRUTOR
        public HistoricoViewModel()
        {
            _preferencesService = ServiceLocator.ServiceProvider.GetRequiredService<IPreferencesService>();
            _registroHistoricoApiClient = new ObterRegistroHistoricoApiClient(this);
            _dataCacheService = new InMemoryCacheService.DataCacheService<RegistroHistoricoModel>();
            Registros = new ObservableCollection<RegistroHistoricoModel>();

            BackgroundBtn = new Button { BackgroundColor = Color.FromArgb(CorTemaButtons()) };

            InicializarUI();
        }
        #endregion
        #region OBJETOS
        public ObservableCollection<RegistroHistoricoModel> Registros
        {
            get => _registros ??= [];
            set => SetProperty(ref _registros, value);
        }
        public string? SaveIDunidade
        {
            get => _SaveIDunidade;
            set => SetProperty(ref _SaveIDunidade, value);
        }
        public bool SemInternet
        {
            get => _SemInternet;
            set => SetProperty(ref _SemInternet, value);
        }
        public bool PageLoading
        {
            get => _PageLoading;
            set => SetProperty(ref _PageLoading, value);
        }
        public string? CorTema
        {
            get => _CorTema ??= string.Empty;
            private set => SetProperty(ref _CorTema, value);
        }
        public Button BackgroundBtn
        {
            get => _BackgroundBtn ??= new Button();
            set => SetProperty(ref _BackgroundBtn, value);
        }
        #endregion
        #region PROCESSOS
        private async Task LoadPreferences()
        {
            if (_preferencesService == null) throw new InvalidOperationException("PreferencesService não foi configurado.");

            SaveIDunidade = await _preferencesService.GetIdUnidadeAsync();

            await Task.CompletedTask;
        }
        private string CorTemaButtons()
        {
            bool hasKey = Preferences.Default.ContainsKey("KeyBtnColor");

            if (hasKey)
            {
                CorTema = Preferences.Default.Get("KeyBtnColor", "");
            }
            else
            {
                CorTema = "EB9C25";
            }

            return CorTema;
        }
        public void ShowPageLoading(bool onEfect)
        {
            PageLoading = onEfect;
        }
        private void AvisoSemInternet()
        {
            if (VerificaInternetService.VerificaInternet())
            {
                SemInternet = !SemInternet;
            }
        }
        private async void InicializarUI()
        {
            await CarregarDadosIniciais();
        }
        private async Task CarregarDadosIniciais()
        {
            try
            {
                var initialData = await FetchDataFromDatabaseAsync(1);

                _dataCacheService.CachePage(1, initialData);
                foreach (var item in initialData)
                {
                    Registros?.Add(item);
                }

                WeakReferenceMessenger.Default.Send(new MensageriaService("PararLoading"));
            }
            catch (Exception)
            {
                // Exibe erro ou reinicia
            }
        }

        private async Task CarregarMaisOuMenosDados(string? parametro)
        {

            switch (parametro)
            {
                case "MaisDados":
                    if (!_bloquearConsultas) _paginaAtual++;
                    else return;
                    break;
                case "MenosDados":
                    if (_paginaAtual > 1)
                    {
                        _paginaAtual--;
                        if (_bloquearConsultas) _bloquearConsultas = false;
                    }
                    else return;
                    break;
            }
            var cachedData = _dataCacheService.GetCachedPage(_paginaAtual);

            if (cachedData != null)
            {
                if (cachedData.Count == 0)
                {
                    _bloquearConsultas = true;
                    return;
                }

                Registros?.Clear();

                foreach (var item in cachedData)
                {
                    Registros?.Add(item);
                }
            }
            else
            {
                var newData = await FetchDataFromDatabaseAsync(_paginaAtual);

                if (newData.Count == 0)
                {
                    _bloquearConsultas = true;
                    return;
                }

                _dataCacheService.CachePage(_paginaAtual, newData);

                Registros?.Clear();

                foreach (var item in newData)
                {
                    Registros?.Add(item);
                }
            }
        }

        private async Task<List<RegistroHistoricoModel>> FetchDataFromDatabaseAsync(int page)
        {
            await LoadPreferences();

            if (string.IsNullOrEmpty(SaveIDunidade))
            {
                return new List<RegistroHistoricoModel>();
            }

            var idunidade = int.Parse(SaveIDunidade);

            var detalhes = new RegistroHistoricoModel
            {
                Id = idunidade,
                Page = page
            };

            var registros = await _registroHistoricoApiClient.ObterRegistroHistoricoAsync(detalhes);

            return registros;
        }
        #endregion
        #region COMANDOS
        public ICommand Btn_AvisoSemInternet => new Command(AvisoSemInternet);
        public ICommand Btn_CarregarDadosIniciais => new AsyncRelayCommand(CarregarDadosIniciais);
        public ICommand Btn_CarregarMaisOuMenosDados => new AsyncRelayCommand<string>(CarregarMaisOuMenosDados);
        #endregion
    }
}
