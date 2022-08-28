// All data related files should come under the Data folder
// using Microsoft.EntityFrameworkCore;

using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    // StoreContext should derive from the DbContext base class
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        {
            
        }

        //Name of the table is Products
        public DbSet<Product> Products { get; set; }
    }
}