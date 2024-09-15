using CommunityToolkit.Mvvm.Messaging;
using Cronos.Services;
using Cronos.ViewModels;

namespace Cronos.Views;

public partial class ConfiguracaoPage : ContentPage
{
    private readonly ConfiguracaoViewModel _viewModel;
	public ConfiguracaoPage()
	{
		InitializeComponent();

        _viewModel = new ConfiguracaoViewModel();
        BindingContext = _viewModel;

        /*WeakReferenceMessenger.Default.Register<MensageriaService>(this, (r, m) =>
        {
            if (m.Value == "IniciarLoading")
            {
                _viewModel.ShowPageLoading(true);
            }
            else if (m.Value == "PararLoading")
            {
                _viewModel.ShowPageLoading(false);
            }
        });*/
    }
    /*protected override void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.ShowPageLoading(true);
    }*/
    private void Pessoal_Tapped(object sender, TappedEventArgs e)
    {
        CoresTxt();
        VisibilidadeFrame();
        TxtPessoal.TextColor = Colors.White;    
        FrmPessoal.IsVisible = true;
        _viewModel.Mensagem.Text = "";
    }
    private void Empresa_Tapped(object sender, TappedEventArgs e)
    {
        CoresTxt();
        VisibilidadeFrame();
        TxtEmpresa.TextColor = Colors.White;
        FrmEmpresa.IsVisible = true;
        _viewModel.Mensagem.Text = "";
    }
    private void Notificacao_Tapped(object sender, TappedEventArgs e)
    {
        CoresTxt();
        VisibilidadeFrame();
        TxtNotificacao.TextColor = Colors.White;
        FrmNotificacao.IsVisible = true;
        _viewModel.Mensagem.Text = "";
    }
    private void IconAtualizar_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is Label label)
        {
            switch (label.ClassId)
            {
                case "IconPessoal":
                    GrdPopupAtPessoal.IsVisible = !GrdPopupAtPessoal.IsVisible;
                    break;
                case "IconEmpresa":
                    GrdPopupAtEmpresa.IsVisible = !GrdPopupAtEmpresa.IsVisible;
                    break;
                default:
                    break;
            }
        }
    }

    private void CoresTxt()
    {
        TxtPessoal.TextColor = Color.FromArgb("8A8A8A");
        TxtEmpresa.TextColor = Color.FromArgb("8A8A8A");
        TxtNotificacao.TextColor = Color.FromArgb("8A8A8A");
    }
    private void VisibilidadeFrame()
    {
        FrmPessoal.IsVisible = false;
        FrmEmpresa.IsVisible = false;
        FrmNotificacao.IsVisible = false;
    }

    private void EscolherImagem_PointerEntered(object sender, PointerEventArgs e)
    {
        if (sender is Label txt)
        {
            txt.TextColor = Colors.White;
        }
    }

    private void EscolherImagem_PointerExited(object sender, PointerEventArgs e)
    {
        if (sender is Label txt)
        {
            txt.TextColor = Color.FromArgb("8a8a8a");
        }
    }
}