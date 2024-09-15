using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Cronos.ApiClients;
using Cronos.Services;
using Cronos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using System.Reflection;
using CommunityToolkit.Mvvm.Input;

namespace Cronos.ViewModels
{
    public class ConfiguracaoViewModel : ObservableObject
    {
        #region VARIAVEIS 
        private readonly IPreferencesService _preferencesService;
        private readonly ObterDadosUsuarioApiClient _obterDadosUsuario;
        private readonly ObterLogosEmpresaUnidadeApiClient _obterLogosEmpresaUnidade;
        private readonly ObterEmailsDeNotificacoesApiClient _obterEmailsDeNotificacoes;
        private readonly AtualizarDadosEmpresaUnidadeApiClient _atualizarDadosEmpresaUnidade;
        private readonly AtualizarDadosUsuarioApiClient _atualizarDadosUsuario;
        private readonly AtualizarEmailsAdicionaisNotificacaoApiClient _atualizarEmailsAdicionais;
        private readonly VerificaSenhaApiClient _verificaSenhaApiClient;
        private ObservableCollection<EmailNotificacoesModel>? _emailNotificacoes;

        private string? _SaveIDempresa;
        private string? _SaveIDunidade;
        private string? _SaveIDusuario;
        private string? _SaveIDsetor;
        private string? _CorTema;
        private Button? _backgroundBtn;
        private string? _NomeUI;
        private string? _EmailUI;
        private string? _TelefoneUI;
        private string? _SetorUI;
        private string? _CargoUI;
        private string? _AvatarUI;
        private string? _Nome;
        private string? _Email;
        private string? _Telefone;
        private string? _Celular;
        private string? _Setor;
        private string? _Cargo;
        private string? _Senha;
        private string? _Avatar;

        private string? _EmpresaUI;
        private string? _CnpjUI;
        private string? _UnidadeUI;
        private string? _ResponsavelUI;
        private string? _EmailEmpresaUI;
        private string? _TelefoneEmpresaUI;
        private string? _CelularEmpresaUI;
        private string? _LongradouroEmpresaUI;
        private string? _NumeroEmpresaUI;
        private string? _CepEmpresaUI;
        private string? _BairroEmpresaUI;
        private string? _CidadeEmpresaUI;
        private string? _EstadoEmpresaUI;
        private string? _ComplementoEmpresaUI;
        private string? _Empresa;
        private string? _Cnpj;
        private string? _Unidade;
        private string? _Responsavel;
        private string? _EmailEmpresa;
        private string? _TelefoneEmpresa;
        private string? _CelularEmpresa;
        private string? _LongradouroEmpresa;
        private string? _NumeroEmpresa;
        private string? _CepEmpresa;
        private string? _BairroEmpresa;
        private string? _CidadeEmpresa;
        private string? _EstadoEmpresa;
        private string? _ComplementoEmpresa;

        private string? _LogoLateralUI;
        private string? _LogoSuperiorUI;
        private string? _LogoLateral;
        private string? _LogoSuperior;

        private string? _EmailAdicional_1;
        private string? _ResponsavelEmail_1;
        private string? _EmailAdicional_2;
        private string? _ResponsavelEmail_2;
        private string? _EmailAdicional_3;
        private string? _ResponsavelEmail_3;
        private bool _LoadingCircle = false;
        private bool _estadoButtons = true;
        private Label? _Mensagem;
        private string? _MsgErroEmail_1;
        private string? _MsgErroEmail_2;
        private string? _MsgErroEmail_3;

        private bool _PopupSenha = false;
        private string? _MensageSenha;
        private bool _PageLoading = false;
        private bool _SemInternet = false;
        private string? _NomeTipoEdicao = "EditarUsuario";
        #endregion
        #region CONSTRUTOR
        public ConfiguracaoViewModel()
        {
            _preferencesService = ServiceLocator.ServiceProvider.GetRequiredService<IPreferencesService>();
            _verificaSenhaApiClient = new VerificaSenhaApiClient();
            _obterDadosUsuario = new ObterDadosUsuarioApiClient(this);
            _obterLogosEmpresaUnidade = new ObterLogosEmpresaUnidadeApiClient(this);
            _obterEmailsDeNotificacoes = new ObterEmailsDeNotificacoesApiClient(this);
            _atualizarDadosEmpresaUnidade = new AtualizarDadosEmpresaUnidadeApiClient(this);
            _atualizarDadosUsuario = new AtualizarDadosUsuarioApiClient(this);
            _atualizarEmailsAdicionais = new AtualizarEmailsAdicionaisNotificacaoApiClient(this);
            CorTemaButtons();
            BackgroundBtn = new Button { BackgroundColor = Color.FromArgb(CorTemaButtons()) };
            EmailNotificacoes = new ObservableCollection<EmailNotificacoesModel>();
            Mensagem = new Label { };
            ShowDadosUI();
        }
        #endregion
        #region OBJETOS
        public ObservableCollection<EmailNotificacoesModel>? EmailNotificacoes
        {
            get => _emailNotificacoes;
            set => SetProperty(ref _emailNotificacoes, value);
        }
        public string MsgErroEmail_1
        {
            get => _MsgErroEmail_1 ??= string.Empty;
            set => SetProperty(ref _MsgErroEmail_1, value);
        }
        public string MsgErroEmail_2
        {
            get => _MsgErroEmail_2 ??= string.Empty;
            set => SetProperty(ref _MsgErroEmail_2, value);
        }
        public string MsgErroEmail_3
        {
            get => _MsgErroEmail_3 ??= string.Empty;
            set => SetProperty(ref _MsgErroEmail_3, value);
        }
        public Label Mensagem
        {
            get => _Mensagem ??= new Label();
            set => SetProperty(ref _Mensagem, value);
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
        public string NomeTipoEdicao
        {
            get => _NomeTipoEdicao ??= string.Empty;
            set => SetProperty(ref _NomeTipoEdicao, value);
        }
        public string MensageSenha
        {
            get => _MensageSenha ??= string.Empty;
            set => SetProperty(ref _MensageSenha, value);
        }
        public bool PopupSenha
        {
            get => _PopupSenha;
            set => SetProperty(ref _PopupSenha, value);
        }
        public bool LoadingCircle
        {
            get => _LoadingCircle;
            set => SetProperty(ref _LoadingCircle, value);
        }
        public bool EstadoButtons
        {
            get => _estadoButtons;
            set => SetProperty(ref _estadoButtons, value);
        }
        public string? SaveIDempresa
        {
            get => _SaveIDempresa;
            set => SetProperty(ref _SaveIDempresa, value);
        }
        public string? SaveIDunidade
        {
            get => _SaveIDunidade;
            set => SetProperty(ref _SaveIDunidade, value);
        }
        public string? SaveIDusuario
        {
            get => _SaveIDusuario;
            set => SetProperty(ref _SaveIDusuario, value);
        }
        public string? SaveIDsetor
        {
            get => _SaveIDsetor;
            set => SetProperty(ref _SaveIDsetor, value);
        }
        public string? NomeUI
        {
            get => _NomeUI;
            private set => SetProperty(ref _NomeUI, value);
        }
        public string? EmailUI
        {
            get => _EmailUI;
            private set => SetProperty(ref _EmailUI, value);
        }
        public string? TelefoneUI
        {
            get => _TelefoneUI;
            private set => SetProperty(ref _TelefoneUI, value);
        }
        public string? SetorUI
        {
            get => _SetorUI;
            private set => SetProperty(ref _SetorUI, value);
        }
        public string? CargoUI
        {
            get => _CargoUI;
            private set => SetProperty(ref _CargoUI, value);
        }
        public string? AvatarUI
        {
            get => _AvatarUI;
            private set => SetProperty(ref _AvatarUI, value);
        }
        public string? Nome
        {
            get => _Nome;
            set => SetProperty(ref _Nome, value);
        }
        public string? Email
        {
            get => _Email;
            set => SetProperty(ref _Email, value);
        }
        public string? Telefone
        {
            get => _Telefone;
            set => SetProperty(ref _Telefone, value);
        }
        public string? Celular
        {
            get => _Celular;
            set => SetProperty(ref _Celular, value);
        }
        public string? Setor
        {
            get => _Setor;
            set => SetProperty(ref _Setor, value);
        }
        public string? Cargo
        {
            get => _Cargo;
            set => SetProperty(ref _Cargo, value);
        }
        public string? Avatar
        {
            get => _Avatar;
            set => SetProperty(ref _Avatar, value);
        }
        public string? Senha
        {
            get => _Senha;
            set => SetProperty(ref _Senha, value);
        }
        public string? EmpresaUI
        {
            get => _EmpresaUI;
            private set => SetProperty(ref _EmpresaUI, value);
        }
        public string? CnpjUI
        {
            get => _CnpjUI;
            private set => SetProperty(ref _CnpjUI, value);
        }
        public string? UnidadeUI
        {
            get => _UnidadeUI;
            private set => SetProperty(ref _UnidadeUI, value);
        }
        public string? ResponsavelUI
        {
            get => _ResponsavelUI;
            private set => SetProperty(ref _ResponsavelUI, value);
        }
        public string? EmailEmpresaUI
        {
            get => _EmailEmpresaUI;
            private set => SetProperty(ref _EmailEmpresaUI, value);
        }
        public string? TelefoneEmpresaUI
        {
            get => _TelefoneEmpresaUI;
            private set => SetProperty(ref _TelefoneEmpresaUI, value);
        }
        public string? CelularEmpresaUI
        {
            get => _CelularEmpresaUI;
            private set => SetProperty(ref _CelularEmpresaUI, value);
        }
        public string? LongradouroEmpresaUI
        {
            get => _LongradouroEmpresaUI;
            private set => SetProperty(ref _LongradouroEmpresaUI, value);
        }
        public string? NumeroEmpresaUI
        {
            get => _NumeroEmpresaUI;
            private set => SetProperty(ref _NumeroEmpresaUI, value);
        }
        public string? CepEmpresaUI
        {
            get => _CepEmpresaUI;
            private set => SetProperty(ref _CepEmpresaUI, value);
        }
        public string? BairroEmpresaUI
        {
            get => _BairroEmpresaUI;
            private set => SetProperty(ref _BairroEmpresaUI, value);
        }
        public string? CidadeEmpresaUI
        {
            get => _CidadeEmpresaUI;
            private set => SetProperty(ref _CidadeEmpresaUI, value);
        }
        public string? EstadoEmpresaUI
        {
            get => _EstadoEmpresaUI;
            private set => SetProperty(ref _EstadoEmpresaUI, value);
        }
        public string? ComplementoEmpresaUI
        {
            get => _ComplementoEmpresaUI;
            private set => SetProperty(ref _ComplementoEmpresaUI, value);
        }
        public string? Empresa
        {
            get => _Empresa;
            set => SetProperty(ref _Empresa, value);
        }
        public string? Cnpj
        {
            get => _Cnpj;
            set => SetProperty(ref _Cnpj, value);
        }
        public string? Unidade
        {
            get => _Unidade;
            set => SetProperty(ref _Unidade, value);
        }
        public string? Responsavel
        {
            get => _Responsavel;
            set => SetProperty(ref _Responsavel, value);
        }
        public string? EmailEmpresa
        {
            get => _EmailEmpresa;
            set => SetProperty(ref _EmailEmpresa, value);
        }
        public string? TelefoneEmpresa
        {
            get => _TelefoneEmpresa;
            set => SetProperty(ref _TelefoneEmpresa, value);
        }
        public string? CelularEmpresa
        {
            get => _CelularEmpresa;
            set => SetProperty(ref _CelularEmpresa, value);
        }
        public string? LongradouroEmpresa
        {
            get => _LongradouroEmpresa;
            set => SetProperty(ref _LongradouroEmpresa, value);
        }
        public string? NumeroEmpresa
        {
            get => _NumeroEmpresa;
            set => SetProperty(ref _NumeroEmpresa, value);
        }
        public string? CepEmpresa
        {
            get => _CepEmpresa;
            set => SetProperty(ref _CepEmpresa, value);
        }
        public string? BairroEmpresa
        {
            get => _BairroEmpresa;
            set => SetProperty(ref _BairroEmpresa, value);
        }
        public string? CidadeEmpresa
        {
            get => _CidadeEmpresa;
            set => SetProperty(ref _CidadeEmpresa, value);
        }
        public string? EstadoEmpresa
        {
            get => _EstadoEmpresa;
            set => SetProperty(ref _EstadoEmpresa, value);
        }
        public string? ComplementoEmpresa
        {
            get => _ComplementoEmpresa;
            set => SetProperty(ref _ComplementoEmpresa, value);
        }
        public string? LogoLateralUI
        {
            get => _LogoLateralUI;
            private set => SetProperty(ref _LogoLateralUI, value);
        }
        public string? LogoSuperiorUI
        {
            get => _LogoSuperiorUI;
            private set => SetProperty(ref _LogoSuperiorUI, value);
        }
        public string? LogoLateral
        {
            get => _LogoLateral;
            set => SetProperty(ref _LogoLateral, value);
        }
        public string? LogoSuperior
        {
            get => _LogoSuperior;
            set => SetProperty(ref _LogoSuperior, value);
        }
        public string? EmailAdicional_1
        {
            get => _EmailAdicional_1;
            set => SetProperty(ref _EmailAdicional_1, value);
        }
        public string? ResponsavelEmail_1
        {
            get => _ResponsavelEmail_1;
            set => SetProperty(ref _ResponsavelEmail_1, value);
        }
        public string? EmailAdicional_2
        {
            get => _EmailAdicional_2;
            set => SetProperty(ref _EmailAdicional_2, value);
        }
        public string? ResponsavelEmail_2
        {
            get => _ResponsavelEmail_2;
            set => SetProperty(ref _ResponsavelEmail_2, value);
        }
        public string? EmailAdicional_3
        {
            get => _EmailAdicional_3;
            set => SetProperty(ref _EmailAdicional_3, value);
        }
        public string? ResponsavelEmail_3
        {
            get => _ResponsavelEmail_3;
            set => SetProperty(ref _ResponsavelEmail_3, value);
        }
        public string? CorTema
        {
            get => _CorTema;
            set => SetProperty(ref _CorTema, value);
        }
        public Button BackgroundBtn
        {
            get => _backgroundBtn ??= new Button();
            set => SetProperty(ref _backgroundBtn, value);
        }
        #endregion
        #region PROCESSOS         
        private async Task LoadPreferences()
        {
            if (_preferencesService == null) throw new InvalidOperationException("PreferencesService não foi configurado.");

            SaveIDusuario = await _preferencesService.GetIdUsuarioAsync();
            SaveIDempresa = await _preferencesService.GetIdEmpresaAsync();
            SaveIDunidade = await _preferencesService.GetIdUnidadeAsync();
            SaveIDsetor = await _preferencesService.GetIdSetorAsync();

            await Task.CompletedTask;
        }
        private void EstadoLoading()
        {
            LoadingCircle = !LoadingCircle;
            EstadoButtons = !EstadoButtons;
        }
        public void ShowPageLoading(bool onEfect)
        {
            PageLoading = onEfect;
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
        private void NovaCorTemaButtons(string hexcor)
        {
            Preferences.Default.Set("KeyBtnColor", hexcor);

            WeakReferenceMessenger.Default.Send(new MensageriaService("BtnPage7"));
        }
        private void RedefinirCorPadrao()
        {
            bool hasKey = Preferences.Default.ContainsKey("KeyBtnColor");

            if (hasKey)
            {
                Preferences.Default.Remove("KeyBtnColor");
            }

            WeakReferenceMessenger.Default.Send(new MensageriaService("BtnPage7"));
        }
        public async void ShowDadosUI()
        {
            try
            {
                await LoadPreferences();

                if (!string.IsNullOrEmpty(SaveIDusuario))
                {
                    var idusuario = int.Parse(SaveIDusuario);

                    var dados = await _obterDadosUsuario.ObterDadosUsuarioAsync(idusuario);

                    NomeUI = dados.Nome ?? string.Empty;
                    EmailUI = dados.Email ?? string.Empty;
                    TelefoneUI = dados.Telefone ?? string.Empty;
                    SetorUI = dados.Setor ?? string.Empty;
                    CargoUI = dados.Cargo ?? string.Empty;
                    AvatarUI = dados.URL_Image_Avatar ?? string.Empty;

                    await ShowDadosEmpresaUI();
                    await ShowEmailsNotificacoesUI();
                }

                WeakReferenceMessenger.Default.Send(new MensageriaService("PararLoading"));
            }
            catch (Exception)
            {
                // Exibe erro ou reinicia
            }
        }
        public async Task ShowDadosEmpresaUI()
        {
            await LoadPreferences();

            if (!string.IsNullOrEmpty(SaveIDempresa) && !string.IsNullOrEmpty(SaveIDunidade))
            {
                var idempresa = int.Parse(SaveIDempresa);
                var idunidade = int.Parse(SaveIDunidade);

                var dados = await _obterLogosEmpresaUnidade.EmpresaComUnidadeAsync(idempresa, idunidade);

                EmpresaUI = dados.NomeEmpresa ?? string.Empty;
                CnpjUI = dados.CNPJ ?? string.Empty;
                UnidadeUI = dados.NomeUnidade ?? string.Empty;
                LogoSuperiorUI = dados.URL_Imagem_Logo_Superior ?? string.Empty;
                LogoLateralUI = dados.URL_Imagem_Logo_Esquerdo ?? string.Empty;
                ResponsavelUI = dados.Responsavel ?? string.Empty;
                EmailEmpresaUI = dados.Email ?? string.Empty;
                TelefoneEmpresaUI = dados.Telefones ?? string.Empty;
                LongradouroEmpresaUI = dados.Logradouro ?? string.Empty;
                NumeroEmpresaUI = dados.Numero ?? string.Empty;
                CepEmpresaUI = dados.Cep ?? string.Empty;
                BairroEmpresaUI = dados.Bairro ?? string.Empty;
                CidadeEmpresaUI = dados.Cidade ?? string.Empty;
                EstadoEmpresaUI = dados.Estado ?? string.Empty;
                ComplementoEmpresaUI = dados.Complemento ?? string.Empty;
            }
        }
        public async Task ShowEmailsNotificacoesUI()
        {
            await LoadPreferences();

            if (!string.IsNullOrEmpty(SaveIDusuario))
            {
                var idusuario = int.Parse(SaveIDusuario);

                var listaEmail = await _obterEmailsDeNotificacoes.ObterEmailsDeNotificacoesAsync(idusuario);

                if (listaEmail.Count > 0)
                {
                    EmailNotificacoes?.Clear();

                    foreach (var emails in listaEmail)
                    {
                        EmailNotificacoes?.Add(emails);
                    }

                    var primeiroEmail = EmailNotificacoes?[0];
                    var segundoEmail = EmailNotificacoes?[1];
                    var terceiroEmail = EmailNotificacoes?[2];

                    EmailAdicional_1 = primeiroEmail?.Email;
                    ResponsavelEmail_1 = primeiroEmail?.Nome_Responsavel;
                    EmailAdicional_2 = segundoEmail?.Email;
                    ResponsavelEmail_2 = segundoEmail?.Nome_Responsavel;
                    EmailAdicional_3 = terceiroEmail?.Email;
                    ResponsavelEmail_3 = terceiroEmail?.Nome_Responsavel;
                }
            }
        }
        public async Task AtualizarDadosEmpresaUnidade()
        {
            await LoadPreferences();

            Mensagem.Text = "";
            Mensagem.TextColor = Color.FromArgb("f14343");

            if (!string.IsNullOrEmpty(SaveIDempresa) && !string.IsNullOrEmpty(SaveIDunidade))
            {
                ShowPageLoading(true);

                var dados = new EmpresaComUnidadeModel
                {
                    ID_Empresa = int.Parse(SaveIDempresa),
                    ID_Unidade = int.Parse(SaveIDunidade),
                    CNPJ = Cnpj,
                    NomeEmpresa = Empresa,
                    NomeUnidade = Unidade,
                    Responsavel = Responsavel,
                    Email = EmailEmpresa,
                    TelefoneFixo = TelefoneEmpresa,
                    TelefoneCelular = CelularEmpresa,
                    Logradouro = LongradouroEmpresa,
                    Numero = NumeroEmpresa,
                    Cep = CepEmpresa,
                    Bairro = BairroEmpresa,
                    Cidade = CidadeEmpresa,
                    Estado = EstadoEmpresa,
                    Complemento = ComplementoEmpresa

                };

                var resposta = await _atualizarDadosEmpresaUnidade.AtualizarDadosEmpresaUnidadeAsync(dados);

                if (resposta == "Informações atualizadas com sucesso!")
                {
                    Mensagem.TextColor = Color.FromArgb("089451");
                    Mensagem.Text = resposta;

                    LimparCampos();
                }
                else
                {
                    Mensagem.Text = resposta;
                }

                ShowPageLoading(false);
            }
            else
            {
                Mensagem.Text = "Erro ao localizar a empresa e unidade. Tente novamente!";
            }
        }
        public async Task AtualizarDadosUsuario()
        {
            await LoadPreferences();

            Mensagem.Text = "";
            Mensagem.TextColor = Color.FromArgb("f14343");

            if (!string.IsNullOrEmpty(SaveIDusuario))
            {
                if (!string.IsNullOrEmpty(Email))
                {
                    if (!EmailValidadorService.IsValidEmail(Email))
                    {
                        Mensagem.Text = "Email inválido.";

                        return;
                    }
                }

                ShowPageLoading(true);

                var dados = new UsuarioComTelefonesModel
                {
                    ID_Usuario = int.Parse(SaveIDusuario),

                    Nome = Nome,
                    Email = Email,
                    Cargo = Cargo,
                    Telefone_Fixo = Telefone,
                    Telefone_Celular = Celular,
                    Senha = Senha
                };

                var resposta = await _atualizarDadosUsuario.AtualizarDadosUsuarioAsync(dados);

                if (resposta == "Informações atualizadas com sucesso!")
                {
                    Mensagem.TextColor = Color.FromArgb("089451");
                    Mensagem.Text = resposta;

                    LimparCampos();
                }
                else
                {
                    Mensagem.Text = resposta;
                }

                ShowPageLoading(false);
            }
            else
            {
                Mensagem.Text = "Erro ao localizar o usuário. Tente novamente!";
            }
        }
        public async Task AtualizarEmailsAdicionaisNotificacao()
        {
            await LoadPreferences();

            Mensagem.Text = "";
            Mensagem.TextColor = Color.FromArgb("f14343");

            if (!string.IsNullOrEmpty(SaveIDusuario))
            {
                if (!string.IsNullOrEmpty(EmailAdicional_1))
                {
                    if (!EmailValidadorService.IsValidEmail(EmailAdicional_1))
                    {
                        MsgErroEmail_1 = "Email inválido.";

                        return;
                    }
                }
                if (!string.IsNullOrEmpty(EmailAdicional_2))
                {
                    if (!EmailValidadorService.IsValidEmail(EmailAdicional_2))
                    {
                        MsgErroEmail_2 = "Email inválido.";

                        return;
                    }
                }
                if (!string.IsNullOrEmpty(EmailAdicional_3))
                {
                    if (!EmailValidadorService.IsValidEmail(EmailAdicional_3))
                    {
                        MsgErroEmail_3 = "Email inválido.";

                        return;
                    }
                }

                ShowPageLoading(true);

                var dados = new EmailsNotificacaoComUsuarioModel
                {
                    ID_Usuarios = int.Parse(SaveIDusuario),
                    Nome_Responsavel_1 = ResponsavelEmail_1,
                    Email_1 = EmailAdicional_1,
                    Nome_Responsavel_2 = ResponsavelEmail_2,
                    Email_2 = EmailAdicional_2,
                    Nome_Responsavel_3 = ResponsavelEmail_3,
                    Email_3 = EmailAdicional_3,
                };

                var resposta = await _atualizarEmailsAdicionais.AtualizarEmailsAdicionaisNotificacaoAsync(dados);

                if (resposta == "Emails de notificações atualizados com sucesso!")
                {
                    Mensagem.Text = resposta;
                }
                else
                {
                    Mensagem.Text = resposta;
                }

                ShowPageLoading(false);
            }
            else
            {
                Mensagem.Text = "Erro ao localizar o usuário. Tente novamente!";
            }
        }
        public void LimparCampos()
        {
            var camposParaLimpar = new string[]
            {
                   nameof(Cnpj),
                   nameof(Unidade),
                   nameof(Responsavel),
                   nameof(EmailEmpresa),
                   nameof(TelefoneEmpresa),
                   nameof(CelularEmpresa),
                   nameof(LongradouroEmpresa),
                   nameof(NumeroEmpresa),
                   nameof(CepEmpresa),
                   nameof(BairroEmpresa),
                   nameof(CidadeEmpresa),
                   nameof(EstadoEmpresa),
                   nameof(ComplementoEmpresa),
                   nameof(Nome),
                   nameof(Email),
                   nameof(Telefone),
                   nameof(Celular),
                   nameof(Setor),
                   nameof(Cargo),
                   nameof(Senha),
                   nameof(Avatar),
            };

            foreach (var campo in camposParaLimpar)
            {
                PropertyInfo? propriedade = this.GetType().GetProperty(campo);
                if (propriedade != null && propriedade.CanWrite)
                {
                    if (propriedade.PropertyType == typeof(string))
                    {
                        propriedade.SetValue(this, string.Empty);
                    }
                    else if (propriedade.PropertyType == typeof(int) || propriedade.PropertyType == typeof(double) || propriedade.PropertyType == typeof(decimal))
                    {
                        propriedade.SetValue(this, default);
                    }
                }
            }
        }
        private async Task VerificaSenha()
        {
            if (!string.IsNullOrEmpty(SaveIDusuario))
            {
                if (!string.IsNullOrEmpty(Senha))
                {
                    var idusuario = int.Parse(SaveIDusuario);

                    var resultado = await _verificaSenhaApiClient.VerificarSenhaAsync(idusuario, Senha);

                    if (resultado == "Senha correta.")
                    {
                        switch (NomeTipoEdicao)
                        {
                            case "EditarUsuario":
                                await AtualizarDadosUsuario();
                                break;
                            case "EditarEmpresa":
                                await AtualizarDadosEmpresaUnidade();
                                break;
                            case "EditarEmailsNoficacao":
                                await AtualizarEmailsAdicionaisNotificacao();
                                break;

                        }
                        PopupSenha = false;
                        MensageSenha = Senha = "";
                        ShowDadosUI();
                    }
                    else
                    {
                        MensageSenha = resultado;
                    }
                }
                else
                {
                    MensageSenha = "Por favor! insira a senha para continuar.";
                }
            }
            else
            {
                MensageSenha = "Erro ao localizar o usuário. Tente novamente!";
            }
        }
        private void AvisoSemInternet()
        {
            if (VerificaInternetService.VerificaInternet())
            {
                SemInternet = !SemInternet;
            }
        }
        private void OpenPopupVerificarSenha() => PopupSenha = true;   
        private void ClosePopups()
        {
            PopupSenha = !PopupSenha;
            MensageSenha = "";
        }
        private void ExecutarEdicao(string parametro) => NomeTipoEdicao = parametro;
        #endregion
        #region COMANDOS   
        public ICommand Btn_ConfirmarSenha => new AsyncRelayCommand(VerificaSenha);
        public ICommand Btn_OpenPopupVerificarSenha => new Command(OpenPopupVerificarSenha);
        public ICommand Btn_AvisoSemInternet => new Command(AvisoSemInternet);
        public ICommand Btn_NovaCorTemaButtons => new Command<string>(NovaCorTemaButtons);
        public ICommand Btn_RedefinirCorPadrao => new Command(RedefinirCorPadrao);
        public ICommand Btn_ExecutarEdicao => new Command<string>(ExecutarEdicao);
        public ICommand Btn_ClosePopups => new Command(ClosePopups);
        #endregion
    }
}
