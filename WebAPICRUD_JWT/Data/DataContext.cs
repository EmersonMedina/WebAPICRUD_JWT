using Microsoft.EntityFrameworkCore;
using WebAPICRUD_JWT.Models;

namespace WebAPICRUD_JWT.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
