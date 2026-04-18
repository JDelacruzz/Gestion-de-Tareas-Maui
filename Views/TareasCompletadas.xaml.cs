using Gestion_de_Tareas.ViewModels;

namespace Gestion_de_Tareas.Views;

public partial class TareasCompletadas : ContentPage
{
    private readonly TareasViewModel _vm;

    public TareasCompletadas(TareasViewModel viewModel)
    {
        InitializeComponent();
        _vm = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Filtra solo las completadas cada vez que entra
        ListaTareasCompletadas.ItemsSource = _vm.Tareas
            .Where(t => t.Completada)
            .ToList();
    }
}