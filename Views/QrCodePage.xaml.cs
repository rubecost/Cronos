using CommunityToolkit.Mvvm.Messaging;
using Cronos.Services;
using Cronos.ViewModels;

namespace Cronos.Views;

public partial class QrCodePage : ContentPage
{
    private readonly QrCodeViewModel viewModel;
	public QrCodePage()
	{
		InitializeComponent();

        viewModel = new QrCodeViewModel();

        BindingContext = viewModel;

        WeakReferenceMessenger.Default.Register<MensageriaService>(this, (r, m) =>
        {
            if (m.Value == "IniciarLoading")
            {
                //viewModel.ShowPageLoading(true);
            }
            else if (m.Value == "PararLoading")
            {
               // viewModel.ShowPageLoading(false);
            }
        });
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

      //  viewModel.ShowPageLoading(true);
    }
}