using Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class KeyvalContext : DbContext
    {
        public KeyvalContext(DbContextOptions<KeyvalContext> options) : base(options)
        {

        }

        public DbSet<Keyval>? keyvals { get; set; } 
    }    

}
