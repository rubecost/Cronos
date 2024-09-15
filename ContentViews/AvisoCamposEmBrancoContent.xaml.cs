
namespace Cronos.ContentViews;

public partial class AvisoCamposEmBrancoContent : ContentView
{
    int preferencia = 0;
	public AvisoCamposEmBrancoContent()
	{
		InitializeComponent();

        bool existe = Preferences.ContainsKey("KeyAvisoCampo");

        if (existe == false)
        {
            Preferences.Set("KeyAvisoCampo", 0);
        }
        else
        {
            preferencia = Preferences.Get("KeyAvisoCampo", 0);
        }

        if(preferencia == 1)
        {
            GrdAviso.IsVisible = false;
        }
        else
        {
            GrdAviso.IsVisible = true;
        }
    }

    private void BtnOK(object sender, EventArgs e)
    {
        GrdAviso.IsVisible = false;
    }

    private void BtnNaoMostrarMais(object sender, TappedEventArgs e)
    {
        Preferences.Set("KeyAvisoCampo", 1);
        GrdAviso.IsVisible = false;
    }
}