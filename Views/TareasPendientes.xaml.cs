using Gestion_de_Tareas.Models;
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
        // Solo pendientes, refresca cada vez que entrás
        ListaTareasPendientes.ItemsSource = _vm.Tareas
            .Where(t => !t.Completada)
            .ToList();
    }

    private async void OnTareaTapped(object sender, TappedEventArgs e)
    {
        if (sender is Border border && border.BindingContext is Tarea tarea)
        {
            var detalle = new DetalleTareaPage(_vm, tarea);
            await Navigation.PushAsync(detalle);
        }
    }
}