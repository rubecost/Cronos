using CommunityToolkit.Mvvm.Messaging;
using Cronos.Services;
using Cronos.ViewModels;

namespace Cronos.Views;

public partial class InserirInformacoesEquipamentoPage : ContentPage
{
    private readonly InserirInformacaoEqViewModel viewModel;
	public InserirInformacoesEquipamentoPage()
	{
		InitializeComponent();

        viewModel = new InserirInformacaoEqViewModel();

        BindingContext = viewModel;

       /* WeakReferenceMessenger.Default.Register<MensageriaService>(this, (r, m) =>
        {
            if (m.Value == "IniciarLoading")
            {
                viewModel.ShowPageLoading(true);
            }
            else if (m.Value == "PararLoading")
            {
                viewModel.ShowPageLoading(false);
            }
        });*/
    }
    /*protected override void OnAppearing()
    {
        base.OnAppearing();

        viewModel.ShowPageLoading(true);
    }*/
}