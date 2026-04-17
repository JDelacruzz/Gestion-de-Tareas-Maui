// Views/GestionTareasPage.xaml.cs
using Gestion_de_Tareas.ViewModels;

namespace Gestion_de_Tareas.Views
{
    public partial class GestionTareasPage : ContentPage
    {
        private readonly TareasViewModel _vm;

        public GestionTareasPage(TareasViewModel viewModel)
        {
            InitializeComponent();
            _vm = viewModel;
            BindingContext = _vm; 
        }
    }
}