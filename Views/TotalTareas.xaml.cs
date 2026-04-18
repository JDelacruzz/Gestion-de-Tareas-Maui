using Gestion_de_Tareas.ViewModels;

namespace Gestion_de_Tareas.Views;

public partial class TotalTareas : ContentPage
{
    private readonly TareasViewModel _vm;

    public TotalTareas(TareasViewModel viewModel)
    {
        InitializeComponent();
        _vm = viewModel;
        // Todas las tareas sin filtro
        ListaTotalTareas.ItemsSource = _vm.Tareas;
    }

    // Refresca cada vez que se entra a la página
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ListaTotalTareas.ItemsSource = null;
        ListaTotalTareas.ItemsSource = _vm.Tareas;
    }
}