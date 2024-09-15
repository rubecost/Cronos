using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Cronos.ApiClients;
using Cronos.Models;
using Cronos.Services;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;

namespace Cronos.ViewModels
{
    public class ConfiguracaoEmpresaViewModel : ObservableObject
    {
        #region VARIAVEIS 
        private readonly CriarUnidadeApiClient _criarUnidadeApiClient;
        private readonly CriarSetorApiClient _criarSetorApiClient;
        private readonly ObterDetalhesApiClient _obterDetalhesApiClient;
        private readonly ObterUnidadesApiClient _obterUnidadesApiClient;
        private readonly ObterSetoresApiClient _obterSetoresApiClient;
        private readonly ObterIdUnidadeApiClient _obterIdUnidadeApiClient;
        private readonly ObterIdSetorApiClient _obterIdSetorApiClient;
        private readonly VerificaSenhaApiClient _verificaSenhaApiClient;
        private readonly IPreferencesService _preferencesService;
        private ObservableCollection<string>? _listaUnidades;
        private ObservableCollection<string>? _listaSetores;
        private string? _SaveIDempresa;
        private string? _SaveIDunidade;
        private string? _SaveIDusuario;
        private string? _SaveIDsetor;
        private string? _nomeEmpresa;
        private string? _CNPJ;
        private string? _nomeUnidade;
        private string? _responsavel;
        private string? _email;
        private string? _telefone;
        private string? _celular;
        private string? _numero;
        private string? _cep;
        private string? _bairro;
        private string? _cidade;
        private string? _estado;
        private string? _complemento;
        private string? _logradouro;
        private Label? _MensageUnidade;
        private Label? _MensageSetor;
        private string? _MensageSenha;
        private string? _senha;
        private string? _NomeSetor;
        private string? _Nome_Setor;
        private string? _ItemSelecionadoLista;
        private bool _LoadingCircle = false;
        private bool _estadoButtons = true;
        private string? _NomeEmpresaUI;
        private string? _NomeUnidadeUI;
        private string? _NomeSetorUI;
        private string? _ResponsavelUI;
        private string? _EmailUI;
        private string? _TelefoneUI;
        private string? _CelularUI;
        private string? _EnderecoCompletoUI;
        private int _IndiceUnidadeClicado = -1;
        private int _IndiceSetorClicado = -1;
        private bool _SemInternet = false;
        private bool _PageLoading = false;
        private bool _PopupSenha = false;
        private string? _ParametroMudanca;
        private string? _CorTema;
        private Button? _BackgroundBtn;
        #endregion
        #region CONSTRUTOR
        public ConfiguracaoEmpresaViewModel()
        {
            _preferencesService = ServiceLocator.ServiceProvider.GetRequiredService<IPreferencesService>();
            _obterDetalhesApiClient = new ObterDetalhesApiClient(this);
            _obterUnidadesApiClient = new ObterUnidadesApiClient(this);
            _obterSetoresApiClient = new ObterSetoresApiClient(this);
            _criarUnidadeApiClient = new CriarUnidadeApiClient(this);
            _criarSetorApiClient = new CriarSetorApiClient(this);
            _obterIdUnidadeApiClient = new ObterIdUnidadeApiClient(this);
            _obterIdSetorApiClient = new ObterIdSetorApiClient(this);
            _verificaSenhaApiClient = new VerificaSenhaApiClient();
            BackgroundBtn = new Button { BackgroundColor = Color.FromArgb(CorTemaButtons()) };
            MensageUnidade = new Label { TextColor = Color.FromArgb("F14343") };
            MensageSetor = new Label { TextColor = Color.FromArgb("F14343") };
            ShowDetalhesUI();
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
        public bool PopupSenha
        {
            get => _PopupSenha;
            set => SetProperty(ref _PopupSenha, value);
        }
        public string? CorTema
        {
            get => _CorTema ??= string.Empty;
            private set => SetProperty(ref _CorTema, value);
        }
        public string? ParametroMudanca
        {
            get => _ParametroMudanca ??= string.Empty;
            private set => SetProperty(ref _ParametroMudanca, value);
        }
        public Button BackgroundBtn
        {
            get => _BackgroundBtn ??= new Button();
            set => SetProperty(ref _BackgroundBtn, value);
        }
        public ObservableCollection<string> ListaUnidades
        {
            get => _listaUnidades ??= new ObservableCollection<string>();
            set => SetProperty(ref _listaUnidades, value);
        }
        public ObservableCollection<string> ListaSetores
        {
            get => _listaSetores ??= new ObservableCollection<string>();
            set => SetProperty(ref _listaSetores, value);
        }
        public int IndiceUnidadeClicado
        {
            get => _IndiceUnidadeClicado;
            set => SetProperty(ref _IndiceUnidadeClicado, value);
        }
        public int IndiceSetorClicado
        {
            get => _IndiceSetorClicado;
            set => SetProperty(ref _IndiceSetorClicado, value);
        }
        public string SaveIDempresa
        {
            get => _SaveIDempresa ??= string.Empty;
            set => SetProperty(ref _SaveIDempresa, value);
        }
        public string SaveIDunidade
        {
            get => _SaveIDunidade ??= string.Empty;
            set => SetProperty(ref _SaveIDunidade, value);
        }
        public string SaveIDusuario
        {
            get => _SaveIDusuario ??= string.Empty;
            set => SetProperty(ref _SaveIDusuario, value);
        }
        public string SaveIDsetor
        {
            get => _SaveIDsetor ??= string.Empty;
            set => SetProperty(ref _SaveIDsetor, value);
        }
        public string ItemSelecionadoLista
        {
            get => _ItemSelecionadoLista ??= string.Empty;
            set => SetProperty(ref _ItemSelecionadoLista, value);
        }
        public string NomeEmpresaUI
        {
            get => _NomeEmpresaUI ??= string.Empty;
            set => SetProperty(ref _NomeEmpresaUI, value);
        }
        public string NomeUnidadeUI
        {
            get => _NomeUnidadeUI ??= string.Empty;
            set => SetProperty(ref _NomeUnidadeUI, value);
        }
        public string NomeSetorUI
        {
            get => _NomeSetorUI ??= string.Empty;
            set => SetProperty(ref _NomeSetorUI, value);
        }
        public string ResponsavelUI
        {
            get => _ResponsavelUI ??= string.Empty;
            set => SetProperty(ref _ResponsavelUI, value);
        }
        public string EmailUI
        {
            get => _EmailUI ??= string.Empty;
            set => SetProperty(ref _EmailUI, value);
        }
        public string TelefoneUI
        {
            get => _TelefoneUI ??= string.Empty;
            set => SetProperty(ref _TelefoneUI, value);
        }
        public string CelularUI
        {
            get => _CelularUI ??= string.Empty;
            set => SetProperty(ref _CelularUI, value);
        }
        public string EnderecoCompletoUI
        {
            get => _EnderecoCompletoUI ??= string.Empty;
            set => SetProperty(ref _EnderecoCompletoUI, value);
        }
        public string NomeSetor
        {
            get => _NomeSetor ??= string.Empty;
            set => SetProperty(ref _NomeSetor, value);
        }
        public string Nome_Setor
        {
            get => _Nome_Setor ??= string.Empty;
            set => SetProperty(ref _Nome_Setor, value);
        }

        public string NomeEmpresa
        {
            get => _nomeEmpresa ??= string.Empty;
            set => SetProperty(ref _nomeEmpresa, value);
        }
        public string CNPJ
        {
            get => _CNPJ ??= string.Empty;
            set => SetProperty(ref _CNPJ, value);
        }
        public string NomeUnidade
        {
            get => _nomeUnidade ??= string.Empty;
            set => SetProperty(ref _nomeUnidade, value);
        }
        public string Responsavel
        {
            get => _responsavel ??= string.Empty;
            set => SetProperty(ref _responsavel, value);
        }
        public string Email
        {
            get => _email ??= string.Empty;
            set => SetProperty(ref _email, value);
        }
        public string Telefone
        {
            get => _telefone ??= string.Empty;
            set => SetProperty(ref _telefone, value);
        }
        public string Celular
        {
            get => _celular ??= string.Empty;
            set => SetProperty(ref _celular, value);
        }
        public string Numero
        {
            get => _numero ??= string.Empty;
            set => SetProperty(ref _numero, value);
        }
        public string CEP
        {
            get => _cep ??= string.Empty;
            set => SetProperty(ref _cep, value);
        }
        public string Bairro
        {
            get => _bairro ??= string.Empty;
            set => SetProperty(ref _bairro, value);
        }
        public string Cidade
        {
            get => _cidade ??= string.Empty;
            set => SetProperty(ref _cidade, value);
        }
        public string Estado
        {
            get => _estado ??= string.Empty;
            set => SetProperty(ref _estado, value);
        }
        public string Complemento
        {
            get => _complemento ??= string.Empty;
            set => SetProperty(ref _complemento, value);
        }
        public string Logradouro
        {
            get => _logradouro ??= string.Empty;
            set => SetProperty(ref _logradouro, value);
        }
        public Label MensageUnidade
        {
            get => _MensageUnidade ??= new Label();
            set => SetProperty(ref _MensageUnidade, value);
        }
        public Label MensageSetor
        {
            get => _MensageSetor ??= new Label();
            set => SetProperty(ref _MensageSetor, value);
        }
        public string MensageSenha
        {
            get => _MensageSenha ??= string.Empty;
            set => SetProperty(ref _MensageSenha, value);
        }
        public string Senha
        {
            get => _senha ??= string.Empty;
            set => SetProperty(ref _senha, value);
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
        private void EstadoLoading()
        {
            LoadingCircle = !LoadingCircle;
            EstadoButtons = !EstadoButtons;
        }
        private async void ShowDetalhesUI()
        {
            try
            {
                await LoadPreferences();

                if (!string.IsNullOrEmpty(SaveIDempresa) && !string.IsNullOrEmpty(SaveIDunidade) && !string.IsNullOrEmpty(SaveIDsetor))
                {
                    var iDs = new UnidadesModel
                    {
                        ID_Empresa = int.Parse(SaveIDempresa),
                        ID_Unidade = int.Parse(SaveIDunidade),
                        ID_Setor = int.Parse(SaveIDsetor)
                    };

                    // Obter detalhes unidade e setor
                    var alldetails = await _obterDetalhesApiClient.ObterDetalhesEmpresaUnidadeAsync(iDs);

                    NomeEmpresaUI = alldetails.NomeEmpresa ?? string.Empty;
                    NomeUnidadeUI = alldetails.NomeUnidade ?? string.Empty;
                    ResponsavelUI = alldetails.Responsavel ?? string.Empty;
                    EmailUI = alldetails.Email ?? string.Empty;
                    TelefoneUI = alldetails.Telefone ?? string.Empty;
                    CelularUI = alldetails.Celular ?? string.Empty;
                    EnderecoCompletoUI = alldetails.Logradouro ?? string.Empty;
                    NomeSetorUI = alldetails.NomeSetor ?? string.Empty;

                    // Obter lista de unidades
                    var unidades = await _obterUnidadesApiClient.ObterUnidadesAsync(iDs.ID_Empresa);

                    ListaUnidades.Clear();

                    foreach (var unidade in unidades)
                    {
                        ListaUnidades.Add(unidade);
                    }

                    // Obter lista de setores
                    var setores = await _obterSetoresApiClient.ObterSetoresAsync(iDs.ID_Empresa, iDs.ID_Unidade);

                    ListaSetores.Clear();

                    foreach (var setor in setores)
                    {
                        ListaSetores.Add(setor);
                    }

                    await _obterIdSetorApiClient.ObterIdSetorPorNomeAsync(iDs.ID_Empresa, iDs.ID_Unidade, NomeSetorUI);
                }

                WeakReferenceMessenger.Default.Send(new MensageriaService("PararLoading"));
            }
            catch (Exception)
            {
                // Exibe erro ou recarrega a pagina
            }      
        }
        private async Task CriarUnidade()
        {
            await LoadPreferences();
            MensageUnidade.Text = MensageSetor.Text = "";
            MensageUnidade.TextColor = Color.FromArgb("f14343");

            EstadoLoading();

            if (!string.IsNullOrEmpty(SaveIDusuario) && !string.IsNullOrEmpty(SaveIDempresa))
            {
                if (!string.IsNullOrEmpty(NomeUnidade) && !string.IsNullOrEmpty(Logradouro) &&
                    !string.IsNullOrEmpty(Numero) && !string.IsNullOrEmpty(Cidade) &&
                    !string.IsNullOrEmpty(Estado) && !string.IsNullOrEmpty(CEP) &&
                    !string.IsNullOrEmpty(Nome_Setor))
                {
                    var criarUnidade = new UnidadesModel
                    {
                        ID_Empresa = int.Parse(SaveIDempresa),
                        ID_Usuario = int.Parse(SaveIDusuario),
                        NomeUnidade = NomeUnidade,
                        Responsavel = Responsavel,
                        Email = Email,
                        Telefone = Telefone,
                        Celular = Celular,
                        Numero = Numero,
                        CEP = CEP,
                        Bairro = Bairro,
                        Cidade = Cidade,
                        Estado = Estado,
                        Complemento = Complemento,
                        Logradouro = Logradouro,
                        NomeSetor = Nome_Setor
                    };

                    var resultado = await _criarUnidadeApiClient.CriarUnidadeAsync(criarUnidade);

                    if (resultado.Contains("sucesso"))
                    {
                        await LimparCampos();
                        MensageUnidade.TextColor = Color.FromArgb("089451");
                        MensageUnidade.Text = resultado;
                    }
                    else
                    {
                        MensageUnidade.Text = resultado;
                    }

                    ShowDetalhesUI();
                }
                else
                {
                    MensageUnidade.Text = "O preechimento dos campos com asteriscos \"*\" são obrigatórios!";
                }
            }
            else
            {
                MensageUnidade.Text = "Erro! Obtendo IDs, tente novamente!";
            }
            EstadoLoading();
        }
        private async Task CriarSetor()
        {
            await LoadPreferences();
            MensageUnidade.Text = MensageSetor.Text = "";
            MensageSetor.TextColor = Color.FromArgb("f14343");

            EstadoLoading();

            if (!string.IsNullOrEmpty(SaveIDempresa) && !string.IsNullOrEmpty(SaveIDunidade) && !string.IsNullOrEmpty(SaveIDusuario))
            {
                if (!string.IsNullOrEmpty(NomeSetor))
                {
                    var criarSetor = new CriarSetorModel
                    {
                        ID_Empresa = int.Parse(SaveIDempresa),
                        ID_Unidade = int.Parse(SaveIDunidade),
                        Nome_Setor = NomeSetor,
                        ID_Usuario = int.Parse(SaveIDusuario),
                    };

                    var resultado = await _criarSetorApiClient.CriarSetorAsync(criarSetor);

                    if (resultado == "Setor criado com sucesso!")
                    {
                        await LimparCampos();
                        MensageSetor.TextColor = Color.FromArgb("089451");
                        MensageSetor.Text = resultado;
                    }
                    else
                    {
                        MensageSetor.Text = resultado;
                    }

                    ShowDetalhesUI();
                }
                else
                {
                    MensageSetor.Text = "O nome do setor deve ser preenchido!";
                }
            }
            else
            {
                MensageSetor.Text = "Erro! Obtendo IDs, tente novamente!";
            }

            EstadoLoading();
        }
        public async Task LimparCampos()
        {
            var camposParaLimpar = new string[]
            {
            nameof(NomeSetor),
            nameof(NomeEmpresa),
            nameof(CNPJ),
            nameof (NomeUnidade),
            nameof(Responsavel),
            nameof(Email),
            nameof(Telefone),
            nameof(Celular),
            nameof(Numero),
            nameof(CEP),
            nameof(Bairro),
            nameof(Cidade),
            nameof(Estado),
            nameof(Complemento),
            nameof(Logradouro)
            };

            foreach (var campo in camposParaLimpar)
            {
                PropertyInfo? propriedade = this.GetType().GetProperty(campo);
                if (propriedade != null && propriedade.CanWrite && propriedade.PropertyType == typeof(string))
                {
                    propriedade.SetValue(this, string.Empty);
                }
            }
            await Task.CompletedTask;
        }
        private void SalvarMudanca(string parametro)
        {
            if (IndiceSetorClicado != -1 && parametro == "Setor")
            {
                PopupSenha = true;
                ParametroMudanca = parametro;
            }
            if (IndiceUnidadeClicado != -1 && parametro == "Unidade")
            {
                PopupSenha = true;
                ParametroMudanca = parametro;
            }
        }
        private async Task MudarSetorOuUnidade()
        {
            await LoadPreferences();

            var idempresa = int.Parse(SaveIDempresa);
            var idunidade = int.Parse(SaveIDunidade);
            var idusuario = int.Parse(SaveIDusuario);

            var parametro = ParametroMudanca;

            if (!string.IsNullOrEmpty(Senha))
            {
                var resultado = await _verificaSenhaApiClient.VerificarSenhaAsync(idusuario, Senha);

                if (resultado == "Senha correta.")
                {
                    switch (parametro)
                    {
                        case "Setor":
                            if (IndiceSetorClicado != -1)
                            {
                                ItemSelecionadoLista = ListaSetores[IndiceSetorClicado];
                                await _obterIdSetorApiClient.ObterIdSetorPorNomeAsync(idempresa, idunidade, ItemSelecionadoLista);
                            }
                            break;
                        case "Unidade":
                            if (IndiceUnidadeClicado != -1)
                            {
                                ItemSelecionadoLista = ListaUnidades[IndiceUnidadeClicado];
                                await _obterIdUnidadeApiClient.ObterIdUnidadePorNomeAsync(idempresa, ItemSelecionadoLista);
                            }
                            break;
                        default:
                            break;

                    }
                    PopupSenha = false;
                    IndiceUnidadeClicado = -1;
                    ItemSelecionadoLista = MensageSenha = Senha = "";
                    ShowDetalhesUI();
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
        private void ClosePopups()
        {
            PopupSenha = !PopupSenha;
            MensageSenha = "";
        }
        #endregion
        #region COMANDOS
        // Ao clicar no Btn_ConfirmarSenha ele verifica a senha é valida, e executa o metodo.
        public ICommand Btn_ConfirmarSenha => new AsyncRelayCommand(MudarSetorOuUnidade);
        public ICommand Btn_AvisoSemInternet => new Command(AvisoSemInternet);
        public ICommand Btn_MudarUnidade => new Command<string>(SalvarMudanca);
        public ICommand Btn_MudarSetor => new Command<string>(SalvarMudanca);
        public ICommand Btn_CriarUnidade => new AsyncRelayCommand(CriarUnidade);
        public ICommand Btn_CriarSetor => new AsyncRelayCommand(CriarSetor);
        public ICommand Btn_ClosePopups => new Command(ClosePopups);
        #endregion
    }
}