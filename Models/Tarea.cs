using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Gestion_de_Tareas.Models
{
    public class Tarea : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now.AddDays(1);
        public string Prioridad { get; set; } = "Media";

        private bool _completada = false;
        public bool Completada
        {
            get => _completada;
            set
            {
                if (_completada == value) return;
                _completada = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Estado));
                OnPropertyChanged(nameof(ColorEstado));
            }
        }

        public string Estado => _completada ? "Completada" : "Pendiente";
        public string ColorEstado => _completada ? "#4CAF50" : "#9C27B0";

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string n = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
    }
}