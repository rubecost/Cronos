using CommunityToolkit.Mvvm.Messaging;
using Cronos.Services;
using Cronos.ViewModels;

namespace Cronos.Views;

public partial class ImagensEquipamentoPage : ContentPage
{
    private readonly ImagensEquipamentoViewModel viewModel;
	public ImagensEquipamentoPage()
	{
		InitializeComponent();

        viewModel = new ImagensEquipamentoViewModel();

        BindingContext = viewModel;

    }
    private void ImagemEntered(object sender, PointerEventArgs e)
    {
        if (sender is BoxView box)
        {
            box.Color = Color.FromArgb(viewModel.CorTema);
            box.Opacity = 0.5;
        }
    }
    private void ImagemExited(object sender, PointerEventArgs e)
    {
        if (sender is BoxView box)
        {
            box.Opacity = 0;
        }
    }
}