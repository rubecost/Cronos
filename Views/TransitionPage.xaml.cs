using CommunityToolkit.Mvvm.Messaging;
using Cronos.Services;
using Cronos.ViewModels;

namespace Cronos.Views;

public partial class TransitionPage : ContentPage
{
    public TransitionPage()
    {
        InitializeComponent();

        // Registra o messenger para iniciar a animação de carregamento.
        WeakReferenceMessenger.Default.Register<MensageriaService>(this, async (r, m) =>
        {
            if (m.Value == "IniciarLoading")
            {
                await ShowLoadingEfeito();
            }
        });
    }
    private async Task ShowLoadingEfeito()
    {
        // Certifica-se de que a animação do GIF está tocando.
        LoadingIcon.IsAnimationPlaying = true;

        await Task.CompletedTask;
    }
}
