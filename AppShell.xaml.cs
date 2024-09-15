using CommunityToolkit.Mvvm.Messaging;
using Cronos.Platforms.Windows;
using Cronos.Services;
using Cronos.ViewModels;
using Cronos.Views;

namespace Cronos
{
    public partial class AppShell : Shell
    {
        private readonly NavegacaoViewModel viewModel;

        public AppShell()
        {
            InitializeComponent();
            
            viewModel = new NavegacaoViewModel();
            BindingContext = viewModel;

            // Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("TransitionPage", typeof(TransitionPage));
            Routing.RegisterRoute("MainPage/EquipamentosPage", typeof(EquipamentosPage));         
            Routing.RegisterRoute("MainPage/EquipamentosPage/MenuEquipamentoPage", typeof(MenuEquipamentoPage));
            Routing.RegisterRoute("MainPage/EquipamentosPage/MenuEquipamentoPage/ImagensEquipamentoPage", typeof(ImagensEquipamentoPage));         
            Routing.RegisterRoute("MainPage/EquipamentosPage/MenuEquipamentoPage/InstrumentosSegurancaPage", typeof(InstrumentosSegurancaPage));
            Routing.RegisterRoute("MainPage/EquipamentosPage/MenuEquipamentoPage/ProgramacaoInspecoesPage", typeof(ProgramacaoInspecoesPage));          
            Routing.RegisterRoute("MainPage/EquipamentosPage/MenuEquipamentoPage/RelatorioDigitalPage", typeof(RelatorioDigitalPage));
            Routing.RegisterRoute("MainPage/EquipamentosPage/MenuEquipamentoPage/RecomendacaoPage", typeof(RecomendacaoPage));
            Routing.RegisterRoute("MainPage/EquipamentosPage/MenuEquipamentoPage/QrCodePage", typeof(QrCodePage));
            Routing.RegisterRoute("MainPage/InserirInformacoesEquipamentoPage", typeof(InserirInformacoesEquipamentoPage));
            Routing.RegisterRoute("MainPage/HistoricoPage", typeof(HistoricoPage));
            Routing.RegisterRoute("MainPage/CriarContaPage", typeof(CriarContaPage));
            Routing.RegisterRoute("MainPage/ConfiguracaoEmpresaPage", typeof(ConfiguracaoEmpresaPage));
            Routing.RegisterRoute("MainPage/ConfiguracaoPage", typeof(ConfiguracaoPage));       
            Routing.RegisterRoute("MainPage/AjudaPage", typeof(AjudaPage));
            Routing.RegisterRoute("MainPage/NotificacoesPage", typeof(NotificacoesPage));
        }
        private void BtnMenuEntered(object sender, PointerEventArgs e)
        {
            if (sender is Label txt)
            {
                    txt.TextColor = Colors.White;
                    txt.FontSize = 35;                           
            }
        }
        private void BtnMenuExited(object sender, PointerEventArgs e)
        {
            if (sender is Label txt)
            {            
                    txt.TextColor = Color.FromArgb("8e8e8e");
                    txt.FontSize = 30;               
            }
        }
    }
}
