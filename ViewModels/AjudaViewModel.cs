using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Cronos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cronos.ViewModels
{
	public class AjudaViewModel : ObservableObject
	{
        #region VARIAVEIS 
        private bool _SemInternet = false;
        private bool _PageLoading = false;
        private string? _CorTema;
        private Button? _BackgroundBtn;
        #endregion
        #region CONSTRUTOR
        public AjudaViewModel()
		{
            BackgroundBtn = new Button { BackgroundColor = Color.FromArgb(CorTemaButtons()) };
        }
        #endregion
        #region OBJETOS
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
        #endregion
        #region COMANDOS
        public ICommand Btn_AvisoSemInternet => new Command(AvisoSemInternet);
        #endregion
    }
}
