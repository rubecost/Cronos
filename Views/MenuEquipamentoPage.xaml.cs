using CommunityToolkit.Mvvm.Messaging;
using Cronos.Services;
using Cronos.ViewModels;

namespace Cronos.Views;

public partial class MenuEquipamentoPage : ContentPage
{
    private readonly MenuEquipamentoViewModel viewModel;
	public MenuEquipamentoPage()
	{
		InitializeComponent();

        viewModel = new MenuEquipamentoViewModel();

        BindingContext = viewModel;

        WeakReferenceMessenger.Default.Register<MensageriaService>(this, (r, m) =>
        {
            if (m.Value == "IniciarLoading")
            {
               // viewModel.ShowPageLoading(true);
            }
            else if (m.Value == "PararLoading")
            {
              //  viewModel.ShowPageLoading(false);
            }
        });
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

      //  viewModel.ShowPageLoading(true);
    }
}