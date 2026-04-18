using Gestion_de_Tareas.Data;
using Gestion_de_Tareas.Views;
using Gestion_de_Tareas.ViewModels;
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

                    fonts.AddFont("Lexend-Bold.ttf", "LexendBold");
                    fonts.AddFont("Lexend-Light.ttf", "LexendLight");
                    fonts.AddFont("Lexend-Regular.ttf", "LexendRegular");
                });

            builder.Services.AddDbContext<TareasDbContext>();
            builder.Services.AddTransient<TareasViewModel>();
            builder.Services.AddTransient<GestionTareasPage>();

            builder.Services.AddTransient<TotalTareas>();
            builder.Services.AddTransient<TareasPendientes>();
            builder.Services.AddTransient<TareasCompletadas>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
