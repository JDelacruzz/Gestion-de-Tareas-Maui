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
        DatePickerFecha.Date = (DateTime)_tarea.Fecha;

        ActualizarBadgeEstado();
    }

    // 🆕 Toca el badge → cambia el estado visualmente
    private void OnEstadoTapped(object sender, TappedEventArgs e)
    {
        _tarea.Completada = !_tarea.Completada;
        ActualizarBadgeEstado();
    }

    // 🆕 Actualiza color, ícono y texto del badge
    private void ActualizarBadgeEstado()
    {
        if (_tarea.Completada)
        {
            BadgeEstado.BackgroundColor = Color.FromArgb("#4CAF50"); // verde
            LblEstado.Text = "Completada";
            IconoEstado.Text = "✅";
        }
        else
        {
            BadgeEstado.BackgroundColor = Color.FromArgb("#9C27B0"); // morado
            LblEstado.Text = "Pendiente";
            IconoEstado.Text = "⏳";
        }
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
        _tarea.Fecha = (DateTime)DatePickerFecha.Date;
        // _tarea.Completada ya fue actualizado por OnEstadoTapped

        _vm.ActualizarTareaCommand.Execute(_tarea);

        await DisplayAlert("✅ Listo", "Tarea actualizada.", "OK");
        await Navigation.PopAsync();
    }
}