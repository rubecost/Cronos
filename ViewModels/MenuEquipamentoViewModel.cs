using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Cronos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cronos.ViewModels
{
    public class MenuEquipamentoViewModel : ObservableObject
    {
        #region VARIAVEIS 
        private bool _SemInternet = false;

        private bool _LoadingPage = false;
        private string _TagDeBusca;
        #endregion
        #region CONSTRUTOR
        public MenuEquipamentoViewModel()
        {
            _TagDeBusca = Preferences.Default.Get("TagClicada", "");
        }
        #endregion
        #region OBJETOS
        public string TagDeBusca
        {
            get => _TagDeBusca;
            private set => SetProperty(ref _TagDeBusca, value);
        }
        public bool LoadingPage
        {
            get => _LoadingPage;
            private set => SetProperty(ref _LoadingPage, value);
        }
        public bool SemInternet
        {
            get => _SemInternet;
            set => SetProperty(ref _SemInternet, value);
        }
        #endregion
        #region PROCESSOS               
        private void OpenLoadingPage() => LoadingPage = true;

        private async Task CloseLoadingPage()
        {
            await Task.Delay(3000);
            LoadingPage = false;

        }
        private async Task AbrirOpcaoClicada(string? opcaoClicada)
        {
            await Shell.Current.GoToAsync("TransitionPage", false);

            await Task.Delay(300);

            await Shell.Current.GoToAsync(opcaoClicada, false);
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

        public ICommand Btn_CloseLoadingPage => new AsyncRelayCommand(CloseLoadingPage);
        public ICommand Btn_AbrirOpcaoClicada => new AsyncRelayCommand<string>(AbrirOpcaoClicada);
        #endregion
    }
}
