using Laborator5.Entities;
using System.Data.Entity;

namespace Laborator5
{
    public class BusinessContext : DbContext
    {
        public DbSet<Business> Businesses { get; set; }

    }
}
