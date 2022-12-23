using AppWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AppWeb.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
