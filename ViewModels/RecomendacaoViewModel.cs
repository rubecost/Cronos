using System;
using Cronos.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Cronos.Services;
using System.Windows.Input;
using Windows.ApplicationModel.UserDataTasks;
using CommunityToolkit.Mvvm.Input;

namespace Cronos.ViewModels
{
	public class RecomendacaoViewModel : ObservableObject
	{
		#region VARIAVEIS 
		private string _TagDeBusca;
		private bool _SemInternet = false;
		private ObservableCollection<RecomendacoesCompostoModel>? _recomendacoes;
		private bool _IsEntryEnabled;
		private string? _CorTema;
		private Button? _backgroundBtn;
        private bool _PopupEditarRecomendacao = false;
        #endregion
        #region CONSTRUTOR
        public RecomendacaoViewModel()
		{
			_TagDeBusca = Preferences.Default.Get("TagClicada", "");
			_recomendacoes = new ObservableCollection<RecomendacoesCompostoModel>();
			IsEntryEnabled = false;
			InicializadorDadosFicticios();

			CorTemaButtons();
			BackgroundBtn = new Button { BackgroundColor = Color.FromArgb(CorTemaButtons()) };
		}
		#endregion
		#region OBJETOS
		public bool IsEntryEnabled
		{
			get => _IsEntryEnabled;
			set => SetProperty(ref _IsEntryEnabled, value);
		}
		public string TagDeBusca
		{
			get => _TagDeBusca;
			private set => SetProperty(ref _TagDeBusca, value);
		}
		public ObservableCollection<RecomendacoesCompostoModel> Recomendacao
		{
			get => _recomendacoes ??= new ObservableCollection<RecomendacoesCompostoModel>();
			set => SetProperty(ref _recomendacoes, value);
		}
		public string? CorTema
		{
			get => _CorTema ??= string.Empty;
			private set => SetProperty(ref _CorTema, value);
		}
		public Button BackgroundBtn
		{
			get => _backgroundBtn ??= new Button();
			set => SetProperty(ref _backgroundBtn, value);
		}
		public bool PopupEditarRecomendacao
		{
			get => _PopupEditarRecomendacao;
			set => SetProperty(ref _PopupEditarRecomendacao, value);
        }
        public bool SemInternet
        {
            get => _SemInternet;
            set => SetProperty(ref _SemInternet, value);
        }
        #endregion
        #region PROCESSOS  
        public void InicializadorDadosFicticios()
		{
			Recomendacao = new ObservableCollection<RecomendacoesCompostoModel>
			{
				new RecomendacoesCompostoModel
				{
					Recomendacoes = new RecomendacoesModel
					{
						Data_Criacao = DateTime.Now,
						Texto = "1- SUBSTITUIR MANÔMETRO DANIFICADO. O MANÔMETRO NOVO DEVERÁ SER CALIBRADO E SEU CERTIFICADO DE CALIBRAÇÃO ANEXO AO DATA BOOK NR-13; 2- RECUPERAR PINTURA ONDE INDICADO NO RELATÓRIO; 3- LIMPAR CANALETAS; 4- IDENTIFICAR VÁLVULAS DE BLOQUEIO; 5- SOLICITAR À COMGÁS OS CERTIFICADOS DE CALIBRAÇÕES DAS PSV's.",
						Status_Recomendacao = StatusRecomendacao.Atendida,
						Data_Atualizacao_Status = DateTime.Now,
						Detalhes_Parcial = "4- IDENTIFICAR VÁLVULAS DE BLOQUEIO; \r\n5- SOLICITAR À COMGÁS OS CERTIFICADOS DE CALIBRAÇÕES DAS PSV's."

					},
					Equipamentos = new EquipamentosModel
					{
						Tag = "GN-MC-001-Ba-NI"
					}
				},

				new RecomendacoesCompostoModel
				{
					Recomendacoes = new RecomendacoesModel
					{
						Data_Criacao = DateTime.Now,
						Texto = "1- SUBSTITUIR MANÔMETRO DANIFICADO. O MANÔMETRO NOVO DEVERÁ SER CALIBRADO E SEU CERTIFICADO DE CALIBRAÇÃO ANEXO AO DATA BOOK NR-13; 2- RECUPERAR PINTURA ONDE INDICADO NO RELATÓRIO; 3- LIMPAR CANALETAS; 4- IDENTIFICAR VÁLVULAS DE BLOQUEIO; 5- SOLICITAR À COMGÁS OS CERTIFICADOS DE CALIBRAÇÕES DAS PSV's.",
						Status_Recomendacao = StatusRecomendacao.ParcialmenteAtendida,
						Data_Atualizacao_Status = DateTime.Now,
						Detalhes_Parcial = "4- IDENTIFICAR VÁLVULAS DE BLOQUEIO; \r\n5- SOLICITAR À COMGÁS OS CERTIFICADOS DE CALIBRAÇÕES DAS PSV's."

					},
					Equipamentos = new EquipamentosModel
					{
						Tag = "GN-MC-001-Ba-NI"
					}
				},
                new RecomendacoesCompostoModel
                {
                    Recomendacoes = new RecomendacoesModel
                    {
                        Data_Criacao = DateTime.Now,
                        Texto = "1- SUBSTITUIR MANÔMETRO DANIFICADO. O MANÔMETRO NOVO DEVERÁ SER CALIBRADO E SEU CERTIFICADO DE CALIBRAÇÃO ANEXO AO DATA BOOK NR-13; 2- RECUPERAR PINTURA ONDE INDICADO NO RELATÓRIO; 3- LIMPAR CANALETAS; 4- IDENTIFICAR VÁLVULAS DE BLOQUEIO; 5- SOLICITAR À COMGÁS OS CERTIFICADOS DE CALIBRAÇÕES DAS PSV's.",
                        Status_Recomendacao = StatusRecomendacao.NaoAtendida,
                        Data_Atualizacao_Status = DateTime.Now,
                        Detalhes_Parcial = "4- IDENTIFICAR VÁLVULAS DE BLOQUEIO; \r\n5- SOLICITAR À COMGÁS OS CERTIFICADOS DE CALIBRAÇÕES DAS PSV's."

                    },
                    Equipamentos = new EquipamentosModel
                    {
                        Tag = "GN-MC-001-Ba-NI"
                    }
                }

            };	
		}
		private void BtnStatus(string parametro)
		{
			if(parametro == "Atendida")
			{
				IsEntryEnabled = false;
			}
			else if(parametro == "Parcialmente")
			{
				IsEntryEnabled = true;
			}	
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
		private async Task Voltar()
		{
            await Shell.Current.GoToAsync("TransitionPage", false);

            await Task.Delay(300);

            await Shell.Current.GoToAsync("MenuEquipamentoPage", false);
        }
        private void OpenPopupEditarRecomendacao() => PopupEditarRecomendacao = true;
        private void ClosePopupEditarRecomendacao() => PopupEditarRecomendacao = !PopupEditarRecomendacao;
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
		public ICommand Btn_Status => new Command<string>(BtnStatus);
        public ICommand Btn_AvisoSemInternet => new Command(AvisoSemInternet);

        public ICommand Btn_OpenPopupEditarRecomendacao => new Command(OpenPopupEditarRecomendacao);
        public ICommand Btn_ClosePopupEditarRecomendacao => new Command(ClosePopupEditarRecomendacao);
        #endregion
    }
}
