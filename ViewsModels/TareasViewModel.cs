using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Gestion_de_Tareas.Data;
using Gestion_de_Tareas.Models;

namespace Gestion_de_Tareas.ViewModels
{
    public class TareasViewModel : INotifyPropertyChanged
    {
        private readonly TareasDbContext _context;

        // ── Lista observable → actualiza la UI automáticamente ──
        public ObservableCollection<Tarea> Tareas { get; set; } = new();

        // ── Contadores para el resumen ──
        public int TotalTareas => Tareas.Count;
        public int TareasPendientes => Tareas.Count(t => !t.Completada);
        public int TareasCompletadas => Tareas.Count(t => t.Completada);

        // ── Campos para nueva tarea ──
        private string _nuevoNombre = string.Empty;
        public string NuevoNombre
        {
            get => _nuevoNombre;
            set
            {
                _nuevoNombre = value;
                OnPropertyChanged();
                ((Command)AgregarTareaCommand).ChangeCanExecute();
            }
        }

        private string _nuevaDescripcion = string.Empty;
        public string NuevaDescripcion
        {
            get => _nuevaDescripcion;
            set { _nuevaDescripcion = value; OnPropertyChanged(); }
        }

        private string _nuevaPrioridad = "Media";
        public string NuevaPrioridad
        {
            get => _nuevaPrioridad;
            set { _nuevaPrioridad = value; OnPropertyChanged(); }
        }

        // ── Comandos ──
        public ICommand AgregarTareaCommand { get; }
        public ICommand EliminarTareaCommand { get; }
        public ICommand MarcarCompletadaCommand { get; }

        public TareasViewModel(TareasDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated(); // Crea la BD si no existe

            AgregarTareaCommand = new Command(AgregarTarea, PuedeAgregar);
            EliminarTareaCommand = new Command<Tarea>(EliminarTarea);
            MarcarCompletadaCommand = new Command<Tarea>(MarcarCompletada);

            Tareas.CollectionChanged += (s, e) => ActualizarContadores();

            CargarTareas();
        }

        // ── Cargar tareas desde SQLite ──
        private void CargarTareas()
        {
            Tareas.Clear();
            foreach (var tarea in _context.Tareas.ToList())
                Tareas.Add(tarea);

            ActualizarContadores();
        }

        // ── Agregar nueva tarea ──
        private void AgregarTarea()
        {
            var nueva = new Tarea
            {
                Nombre = NuevoNombre,
                Descripcion = NuevaDescripcion,
                Prioridad = NuevaPrioridad,
                Fecha = DateTime.Now.AddDays(1),
                Completada = false
            };

            _context.Tareas.Add(nueva);
            _context.SaveChanges();
            Tareas.Add(nueva);

            // Limpiar campos
            NuevoNombre = string.Empty;
            NuevaDescripcion = string.Empty;
            NuevaPrioridad = "Media";

            ActualizarContadores();
        }

        private bool PuedeAgregar() => !string.IsNullOrWhiteSpace(NuevoNombre);

        // ── Eliminar tarea ──
        private void EliminarTarea(Tarea tarea)
        {
            _context.Tareas.Remove(tarea);
            _context.SaveChanges();
            Tareas.Remove(tarea);

            ActualizarContadores();
        }

        // ── Marcar como completada / pendiente ──
        private void MarcarCompletada(Tarea tarea)
        {
            tarea.Completada = !tarea.Completada;
            _context.Tareas.Update(tarea);
            _context.SaveChanges();

            // Refrescar el item en la lista para que el Estado se actualice
            int index = Tareas.IndexOf(tarea);
            if (index >= 0)
            {
                Tareas.RemoveAt(index);
                Tareas.Insert(index, tarea);
            }

            ActualizarContadores();
        }

        // ── Actualizar los tres contadores del resumen ──
        private void ActualizarContadores()
        {
            OnPropertyChanged(nameof(TotalTareas));
            OnPropertyChanged(nameof(TareasPendientes));
            OnPropertyChanged(nameof(TareasCompletadas));
        }

        // ── INotifyPropertyChanged ──
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string nombre = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
    }
}