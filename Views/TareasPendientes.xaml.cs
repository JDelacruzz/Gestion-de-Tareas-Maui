using Gestion_de_Tareas.ViewModels;

namespace Gestion_de_Tareas.Views;

public partial class TareasPendientes : ContentPage
{
    private readonly TareasViewModel _vm;

    public TareasPendientes(TareasViewModel viewModel)
    {
        InitializeComponent();
        _vm = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Filtra solo las pendientes cada vez que entra
        ListaTareasPendientes.ItemsSource = _vm.Tareas
            .Where(t => !t.Completada)
            .ToList();
    }
}