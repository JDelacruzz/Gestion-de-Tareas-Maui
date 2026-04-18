using Gestion_de_Tareas.ViewModels;

namespace Gestion_de_Tareas.Views
{
    public partial class GestionTareasPage : ContentPage
    {
        public GestionTareasPage(TareasViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}