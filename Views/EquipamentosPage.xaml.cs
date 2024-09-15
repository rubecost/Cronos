using CommunityToolkit.Mvvm.Messaging;
using Cronos.Services;
using Cronos.ViewModels;

namespace Cronos.Views;

public partial class EquipamentosPage : ContentPage
{
    private readonly EquipamentosViewModel viewModel;
	public EquipamentosPage()
	{
		InitializeComponent();

        viewModel = new EquipamentosViewModel();

        BindingContext = viewModel;

       /* WeakReferenceMessenger.Default.Register<MensageriaService>(this, (r, m) =>
        {
            if(m.Value == "IniciarLoading")
            {
                viewModel.ShowPageLoading(true);
            }
            else if(m.Value == "PararLoading")
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
    private void OnPointerEntered(object sender, PointerEventArgs e)
    {
        if (sender is Frame frm)
        {
            frm.BackgroundColor = Color.FromArgb("8A8A8A");
        }
    }
    private void OnPointerExited(object sender, PointerEventArgs e)
    {
        if (sender is Frame frm)
        {      
            frm.BackgroundColor = Colors.Transparent;
        }
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