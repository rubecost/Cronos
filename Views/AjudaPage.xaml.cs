using CommunityToolkit.Mvvm.Messaging;
using Cronos.Services;

namespace Cronos.Views;

public partial class AjudaPage : ContentPage
{
	public AjudaPage()
	{
		InitializeComponent();

        //WeakReferenceMessenger.Default.Register<MensageriaService>(this, (r, m) =>
        //{
        //    if (m.Value == "IniciarLoading")
        //    {
        //        viewModel.ShowPageLoading(true);
        //    }
        //    else if (m.Value == "PararLoading")
        //    {
        //        viewModel.ShowPageLoading(false);
        //    }
        //});
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

      //  viewModel.ShowPageLoading(true);
    }
}