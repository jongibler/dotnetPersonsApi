using Microsoft.EntityFrameworkCore;

namespace PersonsApi.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }

    }
}
