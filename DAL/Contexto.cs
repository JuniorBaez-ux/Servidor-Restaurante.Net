using Microsoft.EntityFrameworkCore;
using Servidor.Models;

namespace Servidor.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Reservaciones>? Reservaciones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=Data\Reservaciones.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Reservaciones>()
               .HasData(new Reservaciones() { id = 1, Nombre = "Juancito Francisco", Cedula = "402-1274726-1", Fecha = System.DateTime.Now, Mesa = "Mesa 1"});

        }

    }

}
