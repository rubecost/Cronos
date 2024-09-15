using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Cronos.ApiClients;
using Cronos.Services;
using Cronos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Data;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace Cronos.ViewModels
{
    public class InserirInformacaoEqViewModel : ObservableObject
    {
        #region VARIAVEIS
        private readonly IPreferencesService _preferencesService;
        private readonly ObterNomeEmpresaUnidadeApiClient _obterNomeEmpUnidApiClient;
        private readonly CriarEquipamentoApiClient _criarEquipamentoApiClient;
        private readonly SalvarImagensRelatoriosApiClient _salvarImagensRelatoriosApi;
        private readonly InserirInfoEquipamentoApiClient _inserirInfoEquipamentoApiClient;
        private readonly VerificaSenhaApiClient _verificaSenhaApiClient;
        private string? _SaveIDempresa;
        private string? _SaveIDunidade;
        private string? _SaveIDusuario;
        private string? _SaveIDsetor;
        private string? _NomeEmpresaUI;
        private string? _NomeUnidadeUI;
        private string? _NomeSetorUI;
        private bool _LoadingCircle = false;
        private bool _estadoButtons = true;
        private string? _CorTema;
        private Button? _backgroundBtn;
        private Label? _Mensagem;
        private Label? _MensagemArquivos;
        private bool _RadioTipoEdicao = true;
        private string? _TipoEdicao = "InserirInformacoes";
        private bool _RadioSituacao = true;

        private bool _PopupSenha = false;
        private string? _MensageSenha;
        private string? _senha;
        private bool _PageLoading = false;
        private bool _SemInternet = false;

        private string? _Tag;
        private string? _Localizacao;
        private string? _Servico;
        private string? _Numero_Fabricacao;
        private string? _Status_Equipamento = "ATIVO";
        private string? _Volume;
        private string? _PMTA;
        private string? _Pressao_Teste_Hidrosatico;
        private string? _Fluido;
        private string? _Pressao_Maxima;
        private string? _Identificacao_Tag_Pressao;
        private DateTime? _Ultima_Calibracao_Pressao;
        private string? _Numero_Certificado_Pressao;
        private string? _Identificacao_Tag_Valvula;
        private string? _Pressao_Abertura;
        private string? _Bitola;
        private string? _DCBI;
        private DateTime? _Ultima_Calibracao_Valvula;
        private DateTime? _Proxima_Calibracao_Valvula;

        private string? _Identificacao_Tag_Manometro;
        private string? _Faixa_Pressao_Maxima_Manometro;
        private string? _Faixa_Pressao_Minima_Manometro;
        private string? _Unidade_Pressao_Manometro;
        private DateTime? _Ultima_Calibracao_Manometro;
        private DateTime? _Proxima_Calibracao_Manometro;
        private string? _Numero_Certificado_Manometro;

        private string? _Numero_Certificado_Valvula;
        private string? _Classe_Fluido;
        private string? _Grupo_Potencial_Risco;
        private string? _Categoria;
        private string? _Numero_Documento_Inspecao;
        private DateTime? _Data_Ultima_Externa;
        private DateTime? _Data_Ultima_Interna;
        private DateTime? _Data_Ultima_Medicao_Espessura;
        private DateTime? _Data_Ultimo_Teste_e_Ensaio;
        private string? _Observacao_Inspecao;
        private DateTime? _Data_Proxima_Interna;
        private DateTime? _Data_Proxima_Externa;
        private DateTime? _Data_Proxima_Medicao_Espessura;
        private DateTime? _Data_Proximo_Teste_e_Ensaio;
        private string? _Observacao_Proximas_Inspecoes;
        private string? _Taxa_Corrosao_Atual;
        private string? _Taxa_Corrosao_Historica;
        private DateTime? _Data_Maior_Taxa;
        private string? _Texto_Recomendacao;

        private string[] _RelatoriosDigitais = new string[5];
        private string[] _CaminhoRelatorio = new string[5];
        private string[] _TituloRelatorios = new string[5];
        private string[] _DescricaoRelatorios = new string[5];

        private string[] _ImagensEquipamento = new string[6];
        private string[] _CaminhoImagens = new string[6];
        private string? _DescricaoImagens;

        private bool _mostrarTodosCampos = false;

        #endregion

        #region CONSTRUTOR
        public InserirInformacaoEqViewModel()
        {
            _preferencesService = ServiceLocator.ServiceProvider.GetRequiredService<IPreferencesService>();
            _verificaSenhaApiClient = new VerificaSenhaApiClient();
            _obterNomeEmpUnidApiClient = new ObterNomeEmpresaUnidadeApiClient(this);
            _criarEquipamentoApiClient = new CriarEquipamentoApiClient(this);
            _salvarImagensRelatoriosApi = new SalvarImagensRelatoriosApiClient(this);
            _inserirInfoEquipamentoApiClient = new InserirInfoEquipamentoApiClient(this);
            BackgroundBtn = new Button { BackgroundColor = Color.FromArgb(CorTemaButtons()) };
            Mensagem = new Label { };
            MensagemArquivos = new Label { };
            ShowDetalhesUI();
        }
        #endregion
        #region OBJETOS
        public bool MostrarTodosCampos
        {
            get => _mostrarTodosCampos;
            set => SetProperty(ref _mostrarTodosCampos, value);
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
        public bool PopupSenha
        {
            get => _PopupSenha;
            set => SetProperty(ref _PopupSenha, value);
        }
        public string? TipoEdicao
        {
            get => _TipoEdicao;
            set => SetProperty(ref _TipoEdicao, value);
        }
        public bool RadioTipoEdicao
        {
            get => _RadioTipoEdicao;
            set => SetProperty(ref _RadioTipoEdicao, value);
        }
        public bool RadioSituacao
        {
            get => _RadioSituacao;
            set => SetProperty(ref _RadioSituacao, value);
        }
        public Label Mensagem
        {
            get => _Mensagem ??= new Label();
            set => SetProperty(ref _Mensagem, value);
        }
        public Label MensagemArquivos
        {
            get => _MensagemArquivos ??= new Label();
            set => SetProperty(ref _MensagemArquivos, value);
        }
        public string[] RelatoriosDigitais
        {
            get => _RelatoriosDigitais;
            set => SetProperty(ref _RelatoriosDigitais, value);
        }
        public string[] CaminhoRelatorio
        {
            get => _CaminhoRelatorio;
            set => SetProperty(ref _CaminhoRelatorio, value);
        }
        public string[] TituloRelatorios
        {
            get => _TituloRelatorios;
            set => SetProperty(ref _TituloRelatorios, value);
        }
        public string[] DescricaoRelatorios
        {
            get => _DescricaoRelatorios;
            set => SetProperty(ref _DescricaoRelatorios, value);
        }
        public string[] ImagensEquipamento
        {
            get => _ImagensEquipamento;
            set => SetProperty(ref _ImagensEquipamento, value);
        }
        public string[] CaminhoImagens
        {
            get => _CaminhoImagens;
            set => SetProperty(ref _CaminhoImagens, value);
        }
        public string? DescricaoImagens
        {
            get => _DescricaoImagens;
            set => SetProperty(ref _DescricaoImagens, value);
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
        public string? NomeEmpresaUI
        {
            get => _NomeEmpresaUI;
            set => SetProperty(ref _NomeEmpresaUI, value);
        }
        public string? NomeUnidadeUI
        {
            get => _NomeUnidadeUI;
            set => SetProperty(ref _NomeUnidadeUI, value);
        }
        public string? NomeSetorUI
        {
            get => _NomeSetorUI;
            set => SetProperty(ref _NomeSetorUI, value);
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
        public string? CorTema
        {
            get => _CorTema;
            private set => SetProperty(ref _CorTema, value);
        }
        public Button BackgroundBtn
        {
            get => _backgroundBtn ??= new Button();
            set => SetProperty(ref _backgroundBtn, value);
        }
        public string? Tag
        {
            get => _Tag;
            set => SetProperty(ref _Tag, value);
        }
        public string? Localizacao
        {
            get => _Localizacao;
            set => SetProperty(ref _Localizacao, value);
        }
        public string? Servico
        {
            get => _Servico;
            set => SetProperty(ref _Servico, value);
        }
        public string? Numero_Fabricacao
        {
            get => _Numero_Fabricacao;
            set => SetProperty(ref _Numero_Fabricacao, value);
        }
        public string? Status_Equipamento
        {
            get => _Status_Equipamento;
            set => SetProperty(ref _Status_Equipamento, value);
        }
        public string? Volume
        {
            get => _Volume;
            set => SetProperty(ref _Volume, value);
        }
        public string? PMTA
        {
            get => _PMTA;
            set => SetProperty(ref _PMTA, value);
        }
        public string? Pressao_Teste_Hidrosatico
        {
            get => _Pressao_Teste_Hidrosatico;
            set => SetProperty(ref _Pressao_Teste_Hidrosatico, value);
        }
        public string? Fluido
        {
            get => _Fluido;
            set => SetProperty(ref _Fluido, value);
        }
        public string? Pressao_Maxima
        {
            get => _Pressao_Maxima;
            set => SetProperty(ref _Pressao_Maxima, value);
        }
        public string? Identificacao_Tag_Pressao
        {
            get => _Identificacao_Tag_Pressao;
            set => SetProperty(ref _Identificacao_Tag_Pressao, value);
        }
        public DateTime? Ultima_Calibracao_Pressao
        {
            get => _Ultima_Calibracao_Pressao;
            set => SetProperty(ref _Ultima_Calibracao_Pressao, value);
        }
        public string? Numero_Certificado_Pressao
        {
            get => _Numero_Certificado_Pressao;
            set => SetProperty(ref _Numero_Certificado_Pressao, value);
        }
        public string? Identificacao_Tag_Valvula
        {
            get => _Identificacao_Tag_Valvula;
            set => SetProperty(ref _Identificacao_Tag_Valvula, value);
        }
        public string? Pressao_Abertura
        {
            get => _Pressao_Abertura;
            set => SetProperty(ref _Pressao_Abertura, value);
        }
        public string? Bitola
        {
            get => _Bitola;
            set => SetProperty(ref _Bitola, value);
        }
        public string? DCBI
        {
            get => _DCBI;
            set => SetProperty(ref _DCBI, value);
        }
        public string? Identificacao_Tag_Manometro
        {
            get => _Identificacao_Tag_Manometro;
            set => SetProperty(ref _Identificacao_Tag_Manometro, value);
        }
        public string? Faixa_Pressao_Maxima_Manometro
        {
            get => _Faixa_Pressao_Maxima_Manometro;
            set => SetProperty(ref _Faixa_Pressao_Maxima_Manometro, value);
        }
        public string? Faixa_Pressao_Minima_Manometro
        {
            get => _Faixa_Pressao_Minima_Manometro;
            set => SetProperty(ref _Faixa_Pressao_Minima_Manometro, value);
        }
        public string? Unidade_Pressao_Manometro
        {
            get => _Unidade_Pressao_Manometro;
            set => SetProperty(ref _Unidade_Pressao_Manometro, value);
        }
        public DateTime? Ultima_Calibracao_Manometro
        {
            get => _Ultima_Calibracao_Manometro;
            set => SetProperty(ref _Ultima_Calibracao_Manometro, value);
        }
        public DateTime? Proxima_Calibracao_Manometro
        {
            get => _Proxima_Calibracao_Manometro;
            set => SetProperty(ref _Proxima_Calibracao_Manometro, value);
        }
        public string? Numero_Certificado_Manometro
        {
            get => _Numero_Certificado_Manometro;
            set => SetProperty(ref _Numero_Certificado_Manometro, value);
        }
        public DateTime? Ultima_Calibracao_Valvula
        {
            get => _Ultima_Calibracao_Valvula;
            set => SetProperty(ref _Ultima_Calibracao_Valvula, value);
        }
        public DateTime? Proxima_Calibracao_Valvula
        {
            get => _Proxima_Calibracao_Valvula;
            set => SetProperty(ref _Proxima_Calibracao_Valvula, value);
        }
        public string? Numero_Certificado_Valvula
        {
            get => _Numero_Certificado_Valvula;
            set => SetProperty(ref _Numero_Certificado_Valvula, value);

        }
        public string? Classe_Fluido
        {
            get => _Classe_Fluido;
            set => SetProperty(ref _Classe_Fluido, value);
        }
        public string? Grupo_Potencial_Risco
        {
            get => _Grupo_Potencial_Risco;
            set => SetProperty(ref _Grupo_Potencial_Risco, value);
        }
        public string? Categoria
        {
            get => _Categoria;
            set => SetProperty(ref _Categoria, value);
        }
        public string? Numero_Documento_Inspecao
        {
            get => _Numero_Documento_Inspecao;
            set => SetProperty(ref _Numero_Documento_Inspecao, value);
        }
        public DateTime? Data_Ultima_Externa
        {
            get => _Data_Ultima_Externa;
            set => SetProperty(ref _Data_Ultima_Externa, value);
        }
        public DateTime? Data_Ultima_Interna
        {
            get => _Data_Ultima_Interna;
            set => SetProperty(ref _Data_Ultima_Interna, value);
        }
        public DateTime? Data_Ultima_Medicao_Espessura
        {
            get => _Data_Ultima_Medicao_Espessura;
            set => SetProperty(ref _Data_Ultima_Medicao_Espessura, value);
        }
        public DateTime? Data_Ultimo_Teste_e_Ensaio
        {
            get => _Data_Ultimo_Teste_e_Ensaio;
            set => SetProperty(ref _Data_Ultimo_Teste_e_Ensaio, value);
        }
        public string? Observacao_Inspecao
        {
            get => _Observacao_Inspecao;
            set => SetProperty(ref _Observacao_Inspecao, value);
        }
        public DateTime? Data_Proxima_Interna
        {
            get => _Data_Proxima_Interna;
            set => SetProperty(ref _Data_Proxima_Interna, value);
        }
        public DateTime? Data_Proxima_Externa
        {
            get => _Data_Proxima_Externa;
            set => SetProperty(ref _Data_Proxima_Externa, value);
        }
        public DateTime? Data_Proxima_Medicao_Espessura
        {
            get => _Data_Proxima_Medicao_Espessura;
            set => SetProperty(ref _Data_Proxima_Medicao_Espessura, value);
        }
        public DateTime? Data_Proximo_Teste_e_Ensaio
        {
            get => _Data_Proximo_Teste_e_Ensaio;
            set => SetProperty(ref _Data_Proximo_Teste_e_Ensaio, value);
        }
        public string? Observacao_Proximas_Inspecoes
        {
            get => _Observacao_Proximas_Inspecoes;
            set => SetProperty(ref _Observacao_Proximas_Inspecoes, value);
        }
        public string? Taxa_Corrosao_Atual
        {
            get => _Taxa_Corrosao_Atual;
            set => SetProperty(ref _Taxa_Corrosao_Atual, value);
        }
        public string? Taxa_Corrosao_Historica
        {
            get => _Taxa_Corrosao_Historica;
            set => SetProperty(ref _Taxa_Corrosao_Historica, value);
        }
        public DateTime? Data_Maior_Taxa
        {
            get => _Data_Maior_Taxa;
            set => SetProperty(ref _Data_Maior_Taxa, value);
        }
        public string? Texto_Recomendacao
        {
            get => _Texto_Recomendacao;
            set => SetProperty(ref _Texto_Recomendacao, value);
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
        private async void ShowDetalhesUI()
        {
            try
            {
                await LoadPreferences();

                if (!string.IsNullOrEmpty(SaveIDempresa) && !string.IsNullOrEmpty(SaveIDunidade) && !string.IsNullOrEmpty(SaveIDsetor))
                {
                    var iDs = new EquipamentoModel
                    {
                        ID_Empresa = int.Parse(SaveIDempresa),
                        ID_Unidade = int.Parse(SaveIDunidade),
                        ID_Setor = int.Parse(SaveIDsetor)
                    };

                    // Obter nome empresa unidade setor
                    var dadosEmpresa = await _obterNomeEmpUnidApiClient.ObterNomeEmpresaUnidadeAsync(iDs);

                    NomeEmpresaUI = dadosEmpresa.NomeEmpresa ?? string.Empty;
                    NomeUnidadeUI = dadosEmpresa.NomeUnidade ?? string.Empty;
                    NomeSetorUI = dadosEmpresa.NomeSetor ?? string.Empty;

                    WeakReferenceMessenger.Default.Send(new MensageriaService("PararLoading"));

                }
            }
            catch (Exception)
            {
                // Exibe um erro ou reinia a pagina
            }
        }
        public async Task ObterRelatorioDigital(string? relatorio)
        {
            // Abre o explorador de arquivos para seleção de arquivos
            var fileResult = await FilePicker.Default.PickAsync();

            if (fileResult != null)
            {
                switch (relatorio)
                {
                    case "Arquivo1":
                        RelatoriosDigitais[0] = fileResult.FileName;
                        CaminhoRelatorio[0] = fileResult.FullPath;
                        break;
                    case "Arquivo2":
                        RelatoriosDigitais[1] = fileResult.FileName;
                        CaminhoRelatorio[1] = fileResult.FullPath;
                        break;
                    case "Arquivo3":
                        RelatoriosDigitais[2] = fileResult.FileName;
                        CaminhoRelatorio[2] = fileResult.FullPath;
                        break;
                    case "Arquivo4":
                        RelatoriosDigitais[3] = fileResult.FileName;
                        CaminhoRelatorio[3] = fileResult.FullPath;
                        break;
                    case "Arquivo5":
                        RelatoriosDigitais[4] = fileResult.FileName;
                        CaminhoRelatorio[4] = fileResult.FullPath;
                        break;
                    default:
                        break;
                }

                OnPropertyChanged(nameof(RelatoriosDigitais));
            }
            else
            {
                // "Nenhum arquivo selecionado";
            }
        }
        public async Task ObterImagemEquipamento(string? imagem)
        {
            var fileResult = await FilePicker.Default.PickAsync();

            if (fileResult != null)
            {
                switch (imagem)
                {
                    case "Imagem1":
                        ImagensEquipamento[0] = fileResult.FileName;
                        CaminhoImagens[0] = fileResult.FullPath;
                        break;
                    case "Imagem2":
                        ImagensEquipamento[1] = fileResult.FileName;
                        CaminhoImagens[1] = fileResult.FullPath;
                        break;
                    case "Imagem3":
                        ImagensEquipamento[2] = fileResult.FileName;
                        CaminhoImagens[2] = fileResult.FullPath;
                        break;
                    case "Imagem4":
                        ImagensEquipamento[3] = fileResult.FileName;
                        CaminhoImagens[3] = fileResult.FullPath;
                        break;
                    case "Imagem5":
                        ImagensEquipamento[4] = fileResult.FileName;
                        CaminhoImagens[4] = fileResult.FullPath;
                        break;
                    case "Imagem6":
                        ImagensEquipamento[5] = fileResult.FileName;
                        CaminhoImagens[5] = fileResult.FullPath;
                        break;
                    default:
                        break;
                }

                OnPropertyChanged(nameof(ImagensEquipamento));
            }
            else
            {
                // "Nenhuma imagem selecionado";
            }
        }
        public async Task RegistrarInformacoes()
        {
            EstadoLoading();

            await LoadPreferences();

            Mensagem.Text = "";
            Mensagem.TextColor = Color.FromArgb("f14343");

            if (!string.IsNullOrEmpty(SaveIDempresa) && !string.IsNullOrEmpty(SaveIDunidade) && !string.IsNullOrEmpty(SaveIDsetor))
            {
                var equipamento = new EquipamentoModel
                {
                    ID_Empresa = int.Parse(SaveIDempresa),
                    ID_Unidade = int.Parse(SaveIDunidade),
                    ID_Setor = int.Parse(SaveIDsetor),
                    Tag = Tag,
                    Localizacao = Localizacao,
                    Servico = Servico,
                    Numero_Fabricacao = Numero_Fabricacao,
                    Status_Equipamento = Status_Equipamento,
                    Volume = Volume,
                    PMTA = PMTA,
                    Pressao_Teste_Hidrosatico = Pressao_Teste_Hidrosatico,
                    Fluido = Fluido,
                    Pressao_Maxima = Pressao_Maxima,
                    Identificacao_Tag_Pressao = Identificacao_Tag_Pressao,
                    Ultima_Calibracao_Pressao = Ultima_Calibracao_Pressao,
                    Numero_Certificado_Pressao = Numero_Certificado_Pressao,
                    Identificacao_Tag_Valvula = Identificacao_Tag_Valvula,
                    Pressao_Abertura = Pressao_Abertura,
                    Bitola = Bitola,
                    DCBI = DCBI,
                    Ultima_Calibracao_Valvula = Ultima_Calibracao_Valvula,
                    Proxima_Calibracao_Valvula = Proxima_Calibracao_Valvula,
                    Numero_Certificado_Valvula = Numero_Certificado_Valvula,

                    Identificacao_Tag_Manometro = Identificacao_Tag_Manometro,
                    Faixa_Pressao_Maxima_Manometro = Faixa_Pressao_Maxima_Manometro,
                    Faixa_Pressao_Minima_Manometro = Faixa_Pressao_Minima_Manometro,
                    Unidade_Pressao_Manometro = Unidade_Pressao_Manometro,
                    Ultima_Calibracao_Manometro = Ultima_Calibracao_Manometro,
                    Proxima_Calibracao_Manometro = Proxima_Calibracao_Manometro,
                    Numero_Certificado_Manometro = Numero_Certificado_Manometro,

                    Classe_Fluido = Classe_Fluido,
                    Grupo_Potencial_Risco = Grupo_Potencial_Risco,
                    Categoria = Categoria,
                    Numero_Documento_Inspecao = Numero_Documento_Inspecao,
                    Data_Ultima_Externa = Data_Ultima_Externa,
                    Data_Ultima_Interna = Data_Ultima_Interna,
                    Data_Ultima_Medicao_Espessura = Data_Ultima_Medicao_Espessura,
                    Data_Ultimo_Teste_e_Ensaio = Data_Ultimo_Teste_e_Ensaio,
                    Observacao_Inspecao = Observacao_Inspecao,
                    Data_Proxima_Interna = Data_Proxima_Interna,
                    Data_Proxima_Externa = Data_Proxima_Externa,
                    Data_Proxima_Medicao_Espessura = Data_Proxima_Medicao_Espessura,
                    Data_Proximo_Teste_e_Ensaio = Data_Proximo_Teste_e_Ensaio,
                    Observacao_Proximas_Inspecoes = Observacao_Proximas_Inspecoes,
                    Taxa_Corrosao_Atual = Taxa_Corrosao_Atual,
                    Taxa_Corrosao_Historica = Taxa_Corrosao_Historica,
                    Data_Maior_Taxa = Data_Maior_Taxa,
                    Texto_Recomendacao = Texto_Recomendacao,
                };

                if (!string.IsNullOrEmpty(equipamento.Tag))
                {
                    try
                    {
                        if (TipoEdicao == "InserirInformacoes")
                        {
                            var alldetails = await _inserirInfoEquipamentoApiClient.InserirInfoEquipamentoAsync(equipamento);

                            if (alldetails == "Informações inseridas com sucesso!")
                            {
                                Mensagem.TextColor = Color.FromArgb("089451");
                                Mensagem.Text = alldetails;

                                // Salva imagens e relatorios do equipamento
                                await SalvarImgensRelatorios(equipamento.ID_Empresa, equipamento.ID_Unidade, equipamento.ID_Setor);

                                LimparCampos();
                            }
                            else
                            {
                                Mensagem.Text = alldetails;
                            }
                        }
                        else if (TipoEdicao == "CadastrarEquipamento")
                        {
                            var alldetails = await _criarEquipamentoApiClient.CriarEquipamentoAsync(equipamento);

                            if (alldetails == "Equipamento cadastrado com sucesso!")
                            {
                                Mensagem.TextColor = Color.FromArgb("089451");
                                Mensagem.Text = alldetails;

                                await SalvarImgensRelatorios(equipamento.ID_Empresa, equipamento.ID_Unidade, equipamento.ID_Setor);

                                LimparCampos();
                            }
                            else
                            {
                                Mensagem.Text = alldetails;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Mensagem.Text = $"Erro Inesperado: {ex}";
                    }

                }
                else
                {
                    Mensagem.Text = "O preenchimento da Tag de identificação é obrigatório.";
                }
            }
            else
            {
                Mensagem.Text = "Erro ao localizar a empresa, unidade, setor. Tente novamente!";
            }

            EstadoLoading();
        }
        public void Situacao_Equipamento(string situacao) => Status_Equipamento = situacao;
        public void Tipo_Edicao(string tipo)
        {
            if (tipo == "InserirInformacoes") MostrarTodosCampos = false;
            else MostrarTodosCampos = true;

            TipoEdicao = tipo;
        }
        private async Task SalvarImgensRelatorios(int Id_empresa, int Id_unidade, int Id_setor)
        {
            MensagemArquivos.Text = "";
            MensagemArquivos.TextColor = Color.FromArgb("f14343");

            var imagensArquivos = new ImagensRelatoriosModel
            {
                ID_Empresa = Id_empresa,
                ID_Unidade = Id_unidade,
                ID_Setor = Id_setor,
                Tag = Tag,
                UrlImagem1 = CaminhoImagens[0],
                UrlImagem2 = CaminhoImagens[1],
                UrlImagem3 = CaminhoImagens[2],
                UrlImagem4 = CaminhoImagens[3],
                UrlImagem5 = CaminhoImagens[4],
                UrlImagem6 = CaminhoImagens[5],
                DescricaoImagens = DescricaoImagens,
                Anexo1 = CaminhoRelatorio[0],
                Anexo2 = CaminhoRelatorio[1],
                Anexo3 = CaminhoRelatorio[2],
                Anexo4 = CaminhoRelatorio[3],
                Nome1 = TituloRelatorios[0],
                Nome2 = TituloRelatorios[1],
                Nome3 = TituloRelatorios[2],
                Nome4 = TituloRelatorios[3],
                Descricao1 = DescricaoRelatorios[0],
                Descricao2 = DescricaoRelatorios[1],
                Descricao3 = DescricaoRelatorios[2],
                Descricao4 = DescricaoRelatorios[3],
            };

            try
            {
                var allarquivos = await _salvarImagensRelatoriosApi.CriarImagensRelatoriosAsync(imagensArquivos);

                if (allarquivos == "Imagens e relatórios salvos com sucesso!")
                {
                    MensagemArquivos.TextColor = Color.FromArgb("089451");
                    MensagemArquivos.Text = allarquivos;
                }
            }
            catch (Exception ex)
            {
                MensagemArquivos.Text = $"Erro Inesperado: {ex}";
            }
        }
        public void LimparCampos()
        {
            var camposParaLimpar = new string[]
            {
        nameof(Tag),
        nameof(Localizacao),
        nameof(Servico),
        nameof(Numero_Fabricacao),
        nameof(Status_Equipamento),
        nameof(Volume),
        nameof(PMTA),
        nameof(Pressao_Teste_Hidrosatico),
        nameof(Fluido),
        nameof(Pressao_Maxima),
        nameof(Identificacao_Tag_Pressao),
        nameof(Ultima_Calibracao_Pressao),
        nameof(Numero_Certificado_Pressao),
        nameof(Identificacao_Tag_Valvula),
        nameof(Pressao_Abertura),
        nameof(Bitola),
        nameof(DCBI),
        nameof(Ultima_Calibracao_Valvula),
        nameof(Proxima_Calibracao_Valvula),
        nameof(Identificacao_Tag_Manometro),
        nameof(Faixa_Pressao_Maxima_Manometro),
        nameof(Faixa_Pressao_Minima_Manometro),
        nameof(Unidade_Pressao_Manometro),
        nameof(Ultima_Calibracao_Manometro),
        nameof(Proxima_Calibracao_Manometro),
        nameof(Numero_Certificado_Manometro),
        nameof(Numero_Certificado_Valvula),
        nameof(Classe_Fluido),
        nameof(Grupo_Potencial_Risco),
        nameof(Categoria),
        nameof(Numero_Documento_Inspecao),
        nameof(Data_Ultima_Externa),
        nameof(Data_Ultima_Interna),
        nameof(Data_Ultima_Medicao_Espessura),
        nameof(Data_Ultimo_Teste_e_Ensaio),
        nameof(Observacao_Inspecao),
        nameof(Data_Proxima_Interna),
        nameof(Data_Proxima_Externa),
        nameof(Data_Proxima_Medicao_Espessura),
        nameof(Data_Proximo_Teste_e_Ensaio),
        nameof(Observacao_Proximas_Inspecoes),
        nameof(Taxa_Corrosao_Atual),
        nameof(Taxa_Corrosao_Historica),
        nameof(Data_Maior_Taxa),
        nameof(Texto_Recomendacao),
        nameof(CaminhoImagens) + "[0]",
        nameof(CaminhoImagens) + "[1]",
        nameof(CaminhoImagens) + "[2]",
        nameof(CaminhoImagens) + "[3]",
        nameof(CaminhoImagens) + "[4]",
        nameof(CaminhoImagens) + "[5]",
        nameof(DescricaoImagens),
        nameof(CaminhoRelatorio) + "[0]",
        nameof(CaminhoRelatorio) + "[1]",
        nameof(CaminhoRelatorio) + "[2]",
        nameof(CaminhoRelatorio) + "[3]",
        nameof(TituloRelatorios) + "[0]",
        nameof(TituloRelatorios) + "[1]",
        nameof(TituloRelatorios) + "[2]",
        nameof(TituloRelatorios) + "[3]",
        nameof(DescricaoRelatorios) + "[0]",
        nameof(DescricaoRelatorios) + "[1]",
        nameof(DescricaoRelatorios) + "[2]",
        nameof(DescricaoRelatorios) + "[3]",
        nameof(RelatoriosDigitais) + "[0]",
        nameof(RelatoriosDigitais) + "[1]",
        nameof(RelatoriosDigitais) + "[2]",
        nameof(RelatoriosDigitais) + "[3]",
        nameof(ImagensEquipamento) + "[0]",
        nameof(ImagensEquipamento) + "[1]",
        nameof(ImagensEquipamento) + "[2]",
        nameof(ImagensEquipamento) + "[3]",
        nameof(ImagensEquipamento) + "[4]",
        nameof(ImagensEquipamento) + "[5]",
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
            if (string.IsNullOrEmpty(SaveIDusuario))
            {
                MensageSenha = "Erro! Obtendo IDs, tente novamente!";
                return;
            }

            if (!string.IsNullOrEmpty(Senha))
            {
                var idusuario = int.Parse(SaveIDusuario);

                // Verifica senha 
                var resultado = await _verificaSenhaApiClient.VerificarSenhaAsync(idusuario, Senha);

                if (resultado == "Senha correta.")
                {
                    await RegistrarInformacoes();

                    PopupSenha = false;
                    MensageSenha = Senha = "";
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
        private async Task NavegarParaConfiguracaoEmpresa()
        {
            await Shell.Current.GoToAsync("../ConfiguracaoEmpresaPage");
        }
        private void OpenPopupVerificarSenha() => PopupSenha = true;
        private void ClosePopups()
        {
            PopupSenha = !PopupSenha;
            MensageSenha = "";
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
        public ICommand Btn_NavegarParaConfiguracaoEmpresa => new AsyncRelayCommand(NavegarParaConfiguracaoEmpresa);
        public ICommand Btn_ObterRelarioDigital => new AsyncRelayCommand<string?>(ObterRelatorioDigital);
        public ICommand Btn_ObterImagemEquipamento => new AsyncRelayCommand<string?>(ObterImagemEquipamento);
        public ICommand Btn_AvisoSemInternet => new Command(AvisoSemInternet);
        public ICommand Btn_TipoEdicao => new Command<string>(Tipo_Edicao);
        public ICommand Btn_Status_Equipamento => new Command<string>(Situacao_Equipamento);
        public ICommand Btn_OpenPopupVerificarSenha => new Command(OpenPopupVerificarSenha);
        public ICommand Btn_ConfirmarSenha => new AsyncRelayCommand(VerificaSenha);
        public ICommand Btn_ClosePopups => new Command(ClosePopups);
        #endregion
    }
}
