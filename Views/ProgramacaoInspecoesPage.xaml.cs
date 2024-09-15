using CommunityToolkit.Mvvm.Messaging;
using Cronos.Services;
using Cronos.ViewModels;

namespace Cronos.Views;

public partial class ProgramacaoInspecoesPage : ContentPage
{
    private readonly ProgramacaoInspecoesViewModel viewModel;
	public ProgramacaoInspecoesPage()
	{
		InitializeComponent();

        viewModel = new ProgramacaoInspecoesViewModel();

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
    private void BtnMaisMenosDadosEntered(object sender, PointerEventArgs e)
    {
        if (sender is Label txt)
        {
            txt.TextColor = Colors.White;
            txt.FontAttributes = FontAttributes.Bold;
        }
    }
    private void BtnMaisMenosDadosExited(object sender, PointerEventArgs e)
    {
        if (sender is Label txt)
        {
            txt.TextColor = Color.FromArgb("8a8a8a");
            txt.FontAttributes = FontAttributes.None;
        }
    }
}