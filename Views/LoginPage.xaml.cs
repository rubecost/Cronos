using CommunityToolkit.Mvvm.Messaging;
using Cronos.Services;
using Cronos.ViewModels;

namespace Cronos.Views;

public partial class LoginPage : ContentPage
{
	private readonly LoginViewModel viewModel;
	public LoginPage()
	{
		InitializeComponent();

		viewModel = new LoginViewModel();

		BindingContext = viewModel;

		/*WeakReferenceMessenger.Default.Register<MensageriaService>(this, (r, m) =>
		{
			if(m.Value == "NoWifi")
			{
				viewModel.NoWifi = true;
			}
		});*/

    }
	private void VisibilidadeSenha(object sender, EventArgs e)
	{
		if (sender is Label label)
		{
			if (label.ClassId == "Aberto")
			{
				IconOlhoAberto.IsVisible = false;
				IconOlhoFechado.IsVisible = true;
				EtySenha.IsPassword = true;
			}
			else if (label.ClassId == "Fechado")
			{
				IconOlhoAberto.IsVisible = true;
				IconOlhoFechado.IsVisible = false;
				EtySenha.IsPassword = false;
			}
		}
	}
}
