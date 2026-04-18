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

        public ObservableCollection<Tarea> Tareas { get; set; } = new();

        public int TotalTareas => Tareas.Count;
        public int TareasPendientes => Tareas.Count(t => !t.Completada);
        public int TareasCompletadas => Tareas.Count(t => t.Completada);

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

        public ICommand AgregarTareaCommand { get; }
        public ICommand EliminarTareaCommand { get; }
        public ICommand ToggleEstadoCommand { get; }

        public TareasViewModel(TareasDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();

            AgregarTareaCommand = new Command(AgregarTarea, PuedeAgregar);
            EliminarTareaCommand = new Command<Tarea>(EliminarTarea);
            ToggleEstadoCommand = new Command<Tarea>(ToggleEstado);

            Tareas.CollectionChanged += (s, e) => ActualizarContadores();
            CargarTareas();
        }

        private void CargarTareas()
        {
            Tareas.Clear();
            foreach (var t in _context.Tareas.ToList())
                Tareas.Add(t);
            ActualizarContadores();
        }

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

            NuevoNombre = string.Empty;
            NuevaDescripcion = string.Empty;
            NuevaPrioridad = "Media";
            ActualizarContadores();
        }

        private bool PuedeAgregar() => !string.IsNullOrWhiteSpace(NuevoNombre);

        private void EliminarTarea(Tarea tarea)
        {
            _context.Tareas.Remove(tarea);
            _context.SaveChanges();
            Tareas.Remove(tarea);
            ActualizarContadores();
        }

        // Toggle simple: no toca la colección, solo la propiedad
        private void ToggleEstado(Tarea tarea)
        {
            tarea.Completada = !tarea.Completada; // Tarea notifica sola la UI
            _context.Tareas.Update(tarea);
            _context.SaveChanges();
            ActualizarContadores();
        }

        private void ActualizarContadores()
        {
            OnPropertyChanged(nameof(TotalTareas));
            OnPropertyChanged(nameof(TareasPendientes));
            OnPropertyChanged(nameof(TareasCompletadas));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string n = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
    }
}