using Microsoft.Extensions.Logging;

namespace Gestion_de_Tareas
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                    fonts.AddFont("EBGaramond-Regular.ttf", "EBGaramondRegular");
                    fonts.AddFont("EBGaramond-Bold.ttf", "EBGaramondBold");
                    fonts.AddFont("EBGaramond-Italic.ttf", "EBGaramondItalic");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
