using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using Cronos.Platforms.Windows;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace Cronos
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
               .UseMauiApp<App>()
                .UseSkiaSharp()
                .UseMauiCommunityToolkitCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Poppins-Light.ttf", "PoppinsLight");
                    fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                    fonts.AddFont("Poppins-Medium.ttf", "PoppinsMedium");
                    fonts.AddFont("Poppins-SemiBold.ttf", "PoppinsSemiBold");
                    fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");

                    fonts.AddFont("icons.ttf", "icons");
                });
            // Registrar o CursorBehavior como um serviço singleton
            builder.Services.AddSingleton<CursorBehavior>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
