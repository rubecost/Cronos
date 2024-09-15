using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Cronos.ApiClients;
using Cronos.Models;
using Cronos.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cronos.ViewModels
{
    public class ImagensEquipamentoViewModel : ObservableObject
    {
        #region VARIAVEIS 
        private readonly ObterImagensEquipamentoApiClient _ImagensEquipamentoApiClient;
        private ObservableCollection<ImagensEquipamentoComDescricaoModel>? _ImagensAgrupadas;
        private bool _SemInternet = false;
        private string _TagDeBusca;
        private string? _CorTema;
        private string? _imagemSelecionada;
        private bool _imagemExpandida;
        #endregion

        #region CONSTRUTOR
        public ImagensEquipamentoViewModel()
        {
            _TagDeBusca = Preferences.Default.Get("TagClicada", "");
            _ImagensEquipamentoApiClient = new ObterImagensEquipamentoApiClient(this);
            _ImagensAgrupadas = new ObservableCollection<ImagensEquipamentoComDescricaoModel>();

            InicializarUI();
        }
        #endregion

        #region OBJETOS
        public ObservableCollection<ImagensEquipamentoComDescricaoModel>? ImagensAgrupadas
        {
            get => _ImagensAgrupadas;
            set => SetProperty(ref _ImagensAgrupadas, value);
        }
        public string TagDeBusca
        {
            get => _TagDeBusca;
            private set => SetProperty(ref _TagDeBusca, value);
        }

        public bool SemInternet
        {
            get => _SemInternet;
            set => SetProperty(ref _SemInternet, value);
        }
        public string? CorTema
        {
            get => _CorTema ??= string.Empty;
            private set => SetProperty(ref _CorTema, value);
        }
        public string? ImagemSelecionada
        {
            get => _imagemSelecionada;
            set => SetProperty(ref _imagemSelecionada, value);
        }
        public bool ImagemExpandida
        {
            get => _imagemExpandida;
            set => SetProperty(ref _imagemExpandida, value);
        }
        #endregion

        #region PROCESSOS    
        private async Task<string>CorTemaButtons()
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

            await Task.CompletedTask;

            return CorTema;
        }
        private async void InicializarUI()
        {
            await CorTemaButtons();
            await ObterImagensEquipamento();
        }
        public void ExpandirImagem(string imagemUrl)
        {
            ImagemSelecionada = imagemUrl;
            ImagemExpandida = true;
        }

        public void FecharImagem()
        {
            ImagemExpandida = false;
            ImagemSelecionada = "";
        }
        public async Task ObterImagensEquipamento()
        {
            var newData = await FetchDataFromDatabaseAsync(1);

            ImagensAgrupadas?.Clear();

            foreach (var item in newData)
            {
                ImagensAgrupadas?.Add(item);
            }
        }


        private async Task<List<ImagensEquipamentoComDescricaoModel>> FetchDataFromDatabaseAsync(int page)
        {
            var detalhes = new ImagensEquipamentoComDescricaoModel
            {
                Tag = TagDeBusca,
                Page = page
            };

            var imagens = await _ImagensEquipamentoApiClient.ObterImagensEquipamentoAsync(detalhes);
            return imagens;
        }

        private async Task Voltar()
        {
            await Shell.Current.GoToAsync("TransitionPage", false);

            await Task.Delay(300);

            await Shell.Current.GoToAsync("MenuEquipamentoPage", false);
        }

        private void AvisoSemInternet()
        {
            if (VerificaInternetService.VerificaInternet())
            {
                SemInternet = !SemInternet;
            }
        }
        #endregion

        #region COMANDOS
        public ICommand Btn_AvisoSemInternet => new Command(AvisoSemInternet);
        public ICommand Btn_Voltar => new AsyncRelayCommand(Voltar);
        public ICommand Btn_ExpandirImagem => new Command<string>(ExpandirImagem);
        public ICommand Btn_FecharImagem => new Command(FecharImagem);
        #endregion
    }
}