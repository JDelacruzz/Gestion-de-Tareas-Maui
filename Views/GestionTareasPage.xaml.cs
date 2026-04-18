using Gestion_de_Tareas.Handlers;
using Gestion_de_Tareas.ViewModels;

namespace Gestion_de_Tareas.Views
{
    public partial class GestionTareasPage : ContentPage
    {
        public GestionTareasPage(TareasViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
                
            var handler = new TareaBusquedaHandler(viewModel);
            handler.SearchBoxVisibility = SearchBoxVisibility.Expanded;
            Shell.SetSearchHandler(this, handler);
        }
    }
}