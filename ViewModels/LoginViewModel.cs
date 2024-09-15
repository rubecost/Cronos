using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Cronos.Views;
using Cronos.Services;
using Cronos.ApiClients;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Extensions.Options;
using Microsoft.Maui.ApplicationModel.Communication;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Networking;
using System.ComponentModel;
using Microsoft.Maui.Controls;

namespace Cronos.ViewModels
{
	public  class LoginViewModel : ObservableObject
	{
		#region VARIAVEIS 
		private readonly HttpClient _httpClient;
		private readonly EmailRecuperacaoApiClient _emailRecuperacaoClient;
		private readonly FazerLoginApiClient _fazerLoginClient;
		private readonly ValidarCodigoApiClient _validarCodigoClient;
		private readonly AlterarSenhaApiClient _alterarSenhaClient;
		private readonly ObterInformacoesUsuarioApiClient _obterInformacoesUsuarioClient;
        private string? _CorTema;
		private Button? _backgroundBtn;
		private string? _email;
		private string? _senha;
		private string? _message;
		private bool _popupAddEmail = false;
		private bool _popupAddCode = false;
		private bool _popupAddNovaSenha = false;
		private bool _popupSucesso = false;
		private string? _emailRecuperacao;
		private string? _codeConfirmacao;
		private Label? _messageRedefinicao;
		private string? _senhaNova1;
		private string? _senhaNova2;
		private bool _loadingCircle = false;
		private bool _loadingVisible = true;
		private bool _estadoButtons = true;
        private bool _SemInternet = false;

        #endregion
        #region CONSTRUTOR
        public LoginViewModel()
		{		
			_httpClient = new HttpClient();
			_emailRecuperacaoClient = new EmailRecuperacaoApiClient(this);
			_fazerLoginClient = new FazerLoginApiClient(this);
			_validarCodigoClient = new ValidarCodigoApiClient(this);
			_alterarSenhaClient = new AlterarSenhaApiClient(this);
			
			_obterInformacoesUsuarioClient = new ObterInformacoesUsuarioApiClient(this);
			CorTemaButtons();
			BackgroundBtn = new Button { BackgroundColor = Color.FromArgb(CorTemaButtons()) };
			MessageRedefinicao = new Label { };

        }
        #endregion
        #region OBJETOS
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
		public bool EstadoButtons
		{
			get => _estadoButtons;
			set => SetProperty(ref _estadoButtons, value);
		}
		public bool LoadingCircle
		{
			get => _loadingCircle;
			set => SetProperty(ref _loadingCircle, value);
		}
		public bool LoadingVisible
		{
			get => _loadingVisible;
			set => SetProperty(ref _loadingVisible, value);
		}
		public string? Email
		{
			get => _email ??= string.Empty;
			set => SetProperty(ref _email, value);
		}
		public string? Senha
		{
			get => _senha ??= string.Empty;
			set => SetProperty(ref _senha, value);
		}
		public string? Message
		{
			get => _message ??= string.Empty;
			set => SetProperty(ref _message, value);
		}
		public bool PopupAddEmail
		{
			get => _popupAddEmail;
			set => SetProperty(ref _popupAddEmail, value);
		}
		public bool PopupAddCode
		{
			get => _popupAddCode;
			set => SetProperty(ref _popupAddCode, value);
		}
		public bool PopupAddNovaSenha
		{
			get => _popupAddNovaSenha;
			set => SetProperty(ref _popupAddNovaSenha, value);
		}
		public bool PopupSucesso
		{
			get => _popupSucesso;
			set => SetProperty(ref _popupSucesso, value);
		}
		public string? EmailRecuperacao
		{
			get => _emailRecuperacao ??= string.Empty;
			set => SetProperty(ref _emailRecuperacao, value);
		}
		public string? CodeConfirmacao
		{
			get => _codeConfirmacao ??= string.Empty;
			set => SetProperty(ref _codeConfirmacao, value);
		}
		public Label MessageRedefinicao
		{
			get => _messageRedefinicao ??= new Label();
			set => SetProperty(ref _messageRedefinicao, value);
		}
		public string? SenhaNova1
		{
			get => _senhaNova1 ??= string.Empty;
			set => SetProperty(ref _senhaNova1, value);
		}
		public string? SenhaNova2
		{
			get => _senhaNova2 ??= string.Empty;
			set => SetProperty(ref _senhaNova2, value);
		}
        public bool SemInternet
        {
            get => _SemInternet;
            set => SetProperty(ref _SemInternet, value);
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
		private void ClearValores()
		{
			Email = null;
			Senha = null;
		}
		private void EstadoLoading()
		{
			LoadingCircle = !LoadingCircle;
			EstadoButtons = !EstadoButtons;
		}
		private void MessagePopup(string message)
		{
			if (message == "O Código de Recuperação foi enviado para seu e-mail.")
			{
				MessageRedefinicao.Text = "";
				PopupAddEmail = false;
				PopupAddCode = true;
			}
			else if (message == "Código válido")
			{
				MessageRedefinicao.Text = "";
				PopupAddCode = false;
				PopupAddNovaSenha = true;
			}
			else if(message == "Senha alterada com sucesso.")
			{
				PopupAddNovaSenha = false;
				PopupSucesso = true;
			}
			else
			{
				MessageRedefinicao.TextColor = Color.FromArgb("F14343");
				MessageRedefinicao.Text = message;
			}
		}
		private async Task FazerLogin()
		{
            EstadoLoading();

			if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Senha))
			{
				if (!EmailValidadorService.IsValidEmail(Email))
				{
					Message = "Email inválido.";

					ClearValores();

					EstadoLoading();

					return;
				}

				var resposta = await _fazerLoginClient.FazerLoginAsync(Email,Senha);

				if(resposta == "Login bem-sucedido.")
				{
					await _obterInformacoesUsuarioClient.ObterInformacoesUsuarioAsync(Email);

					if (Application.Current != null)
					{
						Application.Current.MainPage = new AppShell();
					}
				}

				Message = resposta;
			}
			else
			{
				Message = "Insira seu e-mail e senha de login.";
				ClearValores();
			}

			EstadoLoading();
		}
		private async Task EmailRecuperacaoPopup()
		{
			EstadoLoading();

			if (!string.IsNullOrEmpty(EmailRecuperacao))
			{
				if (!EmailValidadorService.IsValidEmail(EmailRecuperacao))
				{
					MessagePopup("Email inválido.");

					EstadoLoading();

					return;
				}

				MessagePopup(await _emailRecuperacaoClient.RecuperarSenhaAsync(EmailRecuperacao));		
			}
			else
			{
				MessagePopup("Insira seu e-mail cadastrado.");
			}

			EstadoLoading();
		}
		private async Task CodeConfirmacaoPopup()
		{
			EstadoLoading();

			if (!string.IsNullOrEmpty(CodeConfirmacao) && !string.IsNullOrEmpty(EmailRecuperacao))
			{
				MessagePopup(await _validarCodigoClient.ValidarCodigoAsync(EmailRecuperacao, CodeConfirmacao));
			}
			else
			{
				MessagePopup("Insira o código enviado ao seu e-mail.");
			}

			EstadoLoading();
		}
		private async Task NovaSenhaPopup()
		{
			EstadoLoading();

			if (!string.IsNullOrEmpty(SenhaNova1) && !string.IsNullOrEmpty(SenhaNova2) && !string.IsNullOrEmpty(EmailRecuperacao))
			{
				if (SenhaNova1 == SenhaNova2)
				{
					MessagePopup(await _alterarSenhaClient.AlterarSenhaAsync(EmailRecuperacao, SenhaNova1));
				}
				else
				{
					MessagePopup("As senhas não coincidem.");
				}
			}
			else
			{
				MessagePopup("Insira a nova senha e confirme.");
			}

			EstadoLoading();
		}
		private void MostrarPopupRecuperarSenha()
		{
			LoadingVisible = false;
			PopupAddEmail = true;
		}
		private void ClosePopups(string nomePopup)
		{
			switch (nomePopup)
			{
				case "popupemail":
					PopupAddEmail = false;
					break;
				case "popupcode":
					PopupAddCode = false;
					break;
				case "popupnovasenha":
					PopupAddNovaSenha = false;
					break;
				case "popupsucesso":
					PopupSucesso = false;
					break;
			}
			MessagePopup("");
			LoadingVisible = true;
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
        public ICommand Btn_FazerLogin => new AsyncRelayCommand(FazerLogin);
		public ICommand Btn_MostrarPopupRecuperarSenha => new Command(MostrarPopupRecuperarSenha);
		public ICommand Btn_EmailRecuperacaoPopup => new AsyncRelayCommand(EmailRecuperacaoPopup);
		public ICommand Btn_CodeConfirmacaoPopup => new AsyncRelayCommand(CodeConfirmacaoPopup);
		public ICommand Btn_NovaSenhaPopup => new AsyncRelayCommand(NovaSenhaPopup);
		public ICommand Btn_ClosePopups => new Command<string>(ClosePopups);
		#endregion
	}
}