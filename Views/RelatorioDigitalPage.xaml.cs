using CommunityToolkit.Mvvm.Messaging;
using Cronos.Services;
using Cronos.ViewModels;

namespace Cronos.Views;

public partial class RelatorioDigitalPage : ContentPage
{
    private readonly RelatorioDigitalViewModel viewModel;
	public RelatorioDigitalPage()
	{
		InitializeComponent();

        viewModel = new RelatorioDigitalViewModel();

        BindingContext = viewModel;

        WeakReferenceMessenger.Default.Register<MensageriaService>(this, (r, m) =>
        {
            if (m.Value == "IniciarLoading")
            {
             //   viewModel.ShowPageLoading(true);
            }
            else if (m.Value == "PararLoading")
            {
             //   viewModel.ShowPageLoading(false);
            }
        });
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

     //   viewModel.ShowPageLoading(true);
    }
}