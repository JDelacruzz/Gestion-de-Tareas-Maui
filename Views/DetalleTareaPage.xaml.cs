using Gestion_de_Tareas.Models;
using Gestion_de_Tareas.ViewModels;

namespace Gestion_de_Tareas.Views;

public partial class DetalleTareaPage : ContentPage
{
    private readonly TareasViewModel _vm;
    private readonly Tarea _tarea;

    public DetalleTareaPage(TareasViewModel viewModel, Tarea tarea)
    {
        InitializeComponent();
        _vm = viewModel;
        _tarea = tarea;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        EntryNombre.Text = _tarea.Nombre;
        EditorDescripcion.Text = _tarea.Descripcion;
        PickerPrioridad.SelectedItem = _tarea.Prioridad;
        DatePickerFecha.Date = (DateTime)_tarea.Fecha; // ✅
    }

    private async void BtnGuardar_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EntryNombre.Text))
        {
            await DisplayAlert("Error", "El nombre no puede estar vacío.", "OK");
            return;
        }

        _tarea.Nombre = EntryNombre.Text.Trim();
        _tarea.Descripcion = EditorDescripcion.Text?.Trim() ?? string.Empty;
        _tarea.Prioridad = PickerPrioridad.SelectedItem?.ToString() ?? "Media";
        _tarea.Fecha = (DateTime)DatePickerFecha.Date; // ✅

        _vm.ActualizarTareaCommand.Execute(_tarea);

        await DisplayAlert("✅ Listo", "Tarea actualizada.", "OK");
        await Navigation.PopAsync();
    }
}