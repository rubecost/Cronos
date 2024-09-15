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
using static Cronos.Services.InMemoryCacheService;

namespace Cronos.ViewModels
{
    public class EquipamentosViewModel : ValidatableObservableObject
    {
        #region VARIAVEIS
        private readonly IPreferencesService _preferencesService;
        private readonly ObterInformacoesEquipamentosApiClient _informacoesEquipamentosApiClient;

        private readonly DataCacheService<EquipamentosCompostoModel> _dataCachePages;
        private readonly InMemoryCacheService _dataCacheall;

        private ObservableCollection<EquipamentosCompostoModel>? _equipamentos;
        private string? _SaveIDsetor;
        private int _paginaAtual = 1;
        private int _quatidadeDados = 1;
        private bool _bloquearConsultas = false;

        private bool _SemInternet = false;
        private bool _PageLoading = false;
        #endregion

        #region CONSTRUTOR
        public EquipamentosViewModel()
        {
            _preferencesService = ServiceLocator.ServiceProvider.GetRequiredService<IPreferencesService>();

            _dataCachePages = App.DataCachePages ?? new DataCacheService<EquipamentosCompostoModel>();
            _dataCacheall = App.DataCacheAll ?? new InMemoryCacheService();

            _informacoesEquipamentosApiClient = new ObterInformacoesEquipamentosApiClient(this);
            Equipamentos = new ObservableCollection<EquipamentosCompostoModel>();

            InicializarUI();
        }
        #endregion

        #region OBJETOS
        public ObservableCollection<EquipamentosCompostoModel>? Equipamentos
        {
            get => _equipamentos;
            set => SetProperty(ref _equipamentos, value);
        }
        public string? SaveIDsetor
        {
            get => _SaveIDsetor;
            set => SetProperty(ref _SaveIDsetor, value);
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
        #endregion

        #region PROCESSOS
        private async void InicializarUI()
        {
            await CarregarDadosIniciais();
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
        private async Task LoadPreferences()
        {
            if (_preferencesService == null) throw new InvalidOperationException("PreferencesService não foi configurado.");

            SaveIDsetor = await _preferencesService.GetIdSetorAsync();

            await Task.CompletedTask;
        }
        private async Task CarregarDadosIniciais()
        {
            try
            {
                var cachedData = _dataCachePages.GetCachedPage(1);

                if (cachedData != null)
                {
                    Equipamentos?.Clear();

                    foreach (var item in cachedData)
                    {
                        Equipamentos?.Add(item);
                    }
                }
                else
                {
                    var initialData = await FetchDataFromDatabaseAsync(1);

                    _dataCachePages.CachePage(1, initialData);

                    foreach (var item in initialData)
                    {
                        Equipamentos?.Add(item);
                    }
                }
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

            var cachedData = _dataCachePages.GetCachedPage(_paginaAtual);

            if (cachedData != null)
            {
                if (cachedData.Count == 0)
                {
                    _bloquearConsultas = true;
                    return;
                }

                Equipamentos?.Clear();

                foreach (var item in cachedData)
                {
                    Equipamentos?.Add(item);
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

                _dataCachePages.CachePage(_paginaAtual, newData);

                Equipamentos?.Clear();

                foreach (var item in newData)
                {
                    Equipamentos?.Add(item);
                }
            }

        }
        private async Task<List<EquipamentosCompostoModel>> FetchDataFromDatabaseAsync(int page)
        {
            await LoadPreferences();

            if (string.IsNullOrEmpty(SaveIDsetor))
            {
                return new List<EquipamentosCompostoModel>();
            }

            var idsetor = int.Parse(SaveIDsetor);

            var detalhes = new EquipamentosCompostoModel
            {
                ID_Setor = idsetor,
                Page = page
            };

            var equipamentos = await _informacoesEquipamentosApiClient.ObterInformacoesEquipamentosAsync(detalhes);

            return equipamentos;
        }
        private async Task AbrirOpcoes(string? parameter)
        {
            Preferences.Default.Set("TagClicada", parameter);
            await Shell.Current.GoToAsync("//MainPage/EquipamentosPage/MenuEquipamentoPage");   
        }
        #endregion
        #region COMANDOS
        public ICommand Btn_CarregarDadosIniciais => new AsyncRelayCommand(CarregarDadosIniciais);
        public ICommand Btn_CarregarMaisOuMenosDados => new AsyncRelayCommand<string>(CarregarMaisOuMenosDados);
        public ICommand Btn_AbrirOpcoes => new AsyncRelayCommand<string?>(AbrirOpcoes);
        public ICommand Btn_AvisoSemInternet => new Command(AvisoSemInternet);
        #endregion
    }
}
