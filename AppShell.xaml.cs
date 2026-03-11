using Gestion_de_Tareas.Views;

namespace Gestion_de_Tareas
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("TotalTareas", typeof(TotalTareas));
            Routing.RegisterRoute("TareasPendientes", typeof(TareasPendientes));
            Routing.RegisterRoute("TareasCompletadas", typeof(TareasCompletadas));
        }
    }
}
