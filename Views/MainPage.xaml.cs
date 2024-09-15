using Cronos.Services;
using Cronos.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Collections.ObjectModel;

namespace Cronos.Views;

public partial class MainPage : ContentPage
{
	private readonly MainPageViewModel viewModel;

	public MainPage()
	{
		InitializeComponent();

		viewModel = new MainPageViewModel();

		BindingContext = viewModel;

        /*WeakReferenceMessenger.Default.Register<MensageriaService>(this, (r, m) =>
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