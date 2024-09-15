using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Cronos.Services;
using Cronos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;
using System.Collections.ObjectModel;

namespace Cronos.ViewModels
{
	public class QrCodeViewModel : ObservableObject
	{
		#region VARIAVEIS 
		private string _TagDeBusca;
		private ObservableCollection<EmpresasModel>? _empresa;
        private bool _SemInternet = false;

        #endregion
        #region CONSTRUTOR
        public QrCodeViewModel()
		{
			_TagDeBusca = Preferences.Default.Get("TagClicada", "");
			_empresa = [];
		}
		#endregion
		#region OBJETOS
		public ObservableCollection<EmpresasModel> Empresa
		{
			get => _empresa ??= [];
			set => SetProperty(ref _empresa, value);
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
        #endregion
        #region PROCESSOS               
        private void ObterUrlQrCode()
		{
			Empresa =
		    [
				 new EmpresasModel
				 {
					 Img_QRCode = "https://i.postimg.cc/nrMKWW5f/QrCode.png"
				 }
		    ];
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
        public ICommand Btn_Voltar => new AsyncRelayCommand(Voltar);
        public ICommand Btn_AvisoSemInternet => new Command(AvisoSemInternet);

        #endregion
    }
}
