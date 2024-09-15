namespace Cronos;

public partial class SplashPage : ContentPage
{
	public SplashPage()
	{
		InitializeComponent();
	}

    private async void SKLottieView_AnimationCompleted(object sender, EventArgs e)
    {
		ImgLogo.IsVisible = true;

		await ImgNome.FadeTo(1,2000,Easing.Linear);
    }
}