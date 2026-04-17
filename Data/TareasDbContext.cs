using Gestion_de_Tareas.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestion_de_Tareas.Data
{
    public class TareasDbContext : DbContext
    {
        // Representa la tabla "Tareas" en SQLite
        public DbSet<Tarea> Tareas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Guarda el archivo .db3 en el almacenamiento local del dispositivo
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "tareas.db3");
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Datos de ejemplo pa' que no arranque vacío (opcional)
            modelBuilder.Entity<Tarea>().HasData(
                new Tarea { Id = 1, Nombre = "Aprender MAUI", Descripcion = "Estudiar EF Core y Bindings", Prioridad = "Alta" },
                new Tarea { Id = 2, Nombre = "Hacer el proyecto", Descripcion = "Gestión de tareas con SQLite", Prioridad = "Media" }
            );
        }
    }
}