using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Cronos.Services;
using Cronos.ApiClients;
using Cronos.Bases;
using Cronos.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Security.Cryptography.Core;
using Windows.UI.Notifications;
using Microsoft.Maui.ApplicationModel.Communication;


namespace Cronos.ViewModels
{
    public class MainPageViewModel : ObservableObject
    {
        #region VARIAVEIS 
        private readonly IPreferencesService _preferencesService;
        private readonly ObterResumoStatusEquipamentosApiClient _obterResumoStatus;
        private readonly ObterRecomendacoesNaoAtendidasApiClient _obterRecomendacoesNaoAtendidas;
        private ObservableCollection<RecomendacaoComEquipamentoModel>? _recomendacoesNaoAtendidas;
        private string? _SaveIDempresa;
        private string? _SaveIDunidade;
        private string? _SaveIDusuario;
        private string? _SaveIDsetor;

        private int _Total_Equipamentos;
        private int _Total_Equipamentos_Vencidos;
        private int _Total_Vence_Em_Menos_De_2_Meses;
        private int _Total_Dentro_Do_Prazo;
        private int _Total_NR13_Nao_Obrigatorio;

        private string? _TituloRecomendacao;
        private int _Total_Valvulas_Seguranca;
        private int _Total_Valvulas_Vencidas;
        private int _Total_Valvulas_No_Prazo;
        private int _Total_Manometros;
        private int _Total_Manometros_Vencidos;
        private int _Total_Manometros_No_Prazo;

        private bool _SemInternet = false;
        private bool _PageLoading = false;
        private string? _CorTema;
        private Button? _BackgroundBtn;
        #endregion
        #region CONSTRUTOR
        public MainPageViewModel()
        {
            _preferencesService = ServiceLocator.ServiceProvider.GetRequiredService<IPreferencesService>();
            _obterResumoStatus = new ObterResumoStatusEquipamentosApiClient(this);
            _obterRecomendacoesNaoAtendidas = new ObterRecomendacoesNaoAtendidasApiClient(this);
            RecomendacoesNaoAtendidas = new ObservableCollection<RecomendacaoComEquipamentoModel>();
            BackgroundBtn = new Button { BackgroundColor = Color.FromArgb(CorTemaButtons()) };
            ShowDadosUI();
        }
        #endregion
        #region OBJETOS  
        public ObservableCollection<RecomendacaoComEquipamentoModel>? RecomendacoesNaoAtendidas
        {
            get => _recomendacoesNaoAtendidas;
            set => SetProperty(ref _recomendacoesNaoAtendidas, value);
        }

        public string? TituloRecomendacao
        {
            get => _TituloRecomendacao;
            set => SetProperty(ref _TituloRecomendacao, value);
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
        public int Total_Equipamentos
        {
            get => _Total_Equipamentos;
            set => SetProperty(ref _Total_Equipamentos, value);
        }
        public int Total_Equipamentos_Vencidos
        {
            get => _Total_Equipamentos_Vencidos;
            set => SetProperty(ref _Total_Equipamentos_Vencidos, value);
        }
        public int Total_Vence_Em_Menos_De_2_Meses
        {
            get => _Total_Vence_Em_Menos_De_2_Meses;
            set => SetProperty(ref _Total_Vence_Em_Menos_De_2_Meses, value);
        }
        public int Total_Dentro_Do_Prazo
        {
            get => _Total_Dentro_Do_Prazo;
            set => SetProperty(ref _Total_Dentro_Do_Prazo, value);
        }
        public int Total_NR13_Nao_Obrigatorio
        {
            get => _Total_NR13_Nao_Obrigatorio;
            set => SetProperty(ref _Total_NR13_Nao_Obrigatorio, value);
        }
        public int Total_Valvulas_Seguranca
        {
            get => _Total_Valvulas_Seguranca;
            set => SetProperty(ref _Total_Valvulas_Seguranca, value);
        }
        public int Total_Valvulas_Vencidas
        {
            get => _Total_Valvulas_Vencidas;
            set => SetProperty(ref _Total_Valvulas_Vencidas, value);
        }
        public int Total_Valvulas_No_Prazo
        {
            get => _Total_Valvulas_No_Prazo;
            set => SetProperty(ref _Total_Valvulas_No_Prazo, value);
        }
        public int Total_Manometros
        {
            get => _Total_Manometros;
            set => SetProperty(ref _Total_Manometros, value);
        }
        public int Total_Manometros_Vencidos
        {
            get => _Total_Manometros_Vencidos;
            set => SetProperty(ref _Total_Manometros_Vencidos, value);
        }
        public int Total_Manometros_No_Prazo
        {
            get => _Total_Manometros_No_Prazo;
            set => SetProperty(ref _Total_Manometros_No_Prazo, value);
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
        public async void ShowDadosUI()
        {
            await ShowStatusResumoEquipamentosUI();
            await ShowRecomendacoesNaoAtendidas();
        }
        private async Task LoadPreferences()
        {
            if (_preferencesService == null) throw new InvalidOperationException("PreferencesService não foi configurado.");

            SaveIDusuario = await _preferencesService.GetIdUsuarioAsync();
            SaveIDempresa = await _preferencesService.GetIdEmpresaAsync();
            SaveIDunidade = await _preferencesService.GetIdUnidadeAsync();
            SaveIDsetor = await _preferencesService.GetIdSetorAsync();

            await Task.CompletedTask;
        }
        private async Task ShowStatusResumoEquipamentosUI()
        {
            try
            {
                await LoadPreferences();

                if (!string.IsNullOrEmpty(SaveIDsetor))
                {
                    var idSetor = int.Parse(SaveIDsetor);

                    var dados = await _obterResumoStatus.ObterResumoStatusEquipamentosAsync(idSetor);

                    Total_Equipamentos = dados.Total_Equipamentos;
                    Total_Equipamentos_Vencidos = dados.Total_Equipamentos_Vencidos;
                    Total_Vence_Em_Menos_De_2_Meses = dados.Total_Vence_Em_Menos_De_2_Meses;
                    Total_Dentro_Do_Prazo = dados.Total_Dentro_Do_Prazo;
                    Total_NR13_Nao_Obrigatorio = dados.Total_NR13_Nao_Obrigatorio;
                    Total_Valvulas_Seguranca = dados.Total_Valvulas_Seguranca;
                    Total_Valvulas_Vencidas = dados.Total_Valvulas_Vencidas;
                    Total_Valvulas_No_Prazo = dados.Total_Valvulas_No_Prazo;
                    Total_Manometros = dados.Total_Manometros;
                    Total_Manometros_Vencidos = dados.Total_Manometros_Vencidos;
                    Total_Manometros_No_Prazo = dados.Total_Manometros_No_Prazo;

                    WeakReferenceMessenger.Default.Send(new MensageriaService("PararLoading"));
                }
            }
            catch (Exception)
            {
                // Erro fechar a pagina ou reiniciar a aplicação
            }
        }
        private async Task ShowRecomendacoesNaoAtendidas()
        {
            try
            {
                await LoadPreferences();

                if (!string.IsNullOrEmpty(SaveIDsetor))
                {
                    var idSetor = int.Parse(SaveIDsetor);

                    var recomendacoes = await _obterRecomendacoesNaoAtendidas.ObterRecomendacoesNaoAtendidasAsync(idSetor);

                    if (recomendacoes.Count > 1)
                    {
                        TituloRecomendacao = $"Existem {recomendacoes.Count} Novas Recomendações não atendidas";
                    }
                    else if (recomendacoes.Count == 1)
                    {
                        TituloRecomendacao = "Existe uma nova Recomendaçaõ não atendida";
                    }
                    else
                    {
                        TituloRecomendacao = "Não há Recomendações no momento";
                    }

                    RecomendacoesNaoAtendidas?.Clear();

                    for (int i = recomendacoes.Count - 1; i >= 0; i--)
                    {
                        RecomendacoesNaoAtendidas?.Add(recomendacoes[i]);
                    }

                }
            }
            catch (Exception)
            {
                // Erro fechar a pagina ou reiniciar a aplicação
            }
        }
        #endregion
        #region COMANDOS
        public ICommand Btn_AvisoSemInternet => new Command(AvisoSemInternet);
        #endregion
    }
}