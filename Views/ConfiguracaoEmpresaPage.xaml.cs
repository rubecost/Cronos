using CommunityToolkit.Mvvm.Messaging;
using Cronos.Services;
using Cronos.ViewModels;

namespace Cronos.Views;

public partial class ConfiguracaoEmpresaPage : ContentPage
{
    private readonly ConfiguracaoEmpresaViewModel viewModel;
	public ConfiguracaoEmpresaPage()
	{
		InitializeComponent();

        viewModel = new ConfiguracaoEmpresaViewModel();

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