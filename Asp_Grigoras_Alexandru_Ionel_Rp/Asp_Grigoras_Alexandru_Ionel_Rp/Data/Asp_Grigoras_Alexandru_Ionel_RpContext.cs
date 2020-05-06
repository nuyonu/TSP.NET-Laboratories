using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Asp_Grigoras_Alexandru_Ionel_Rp.Models;

namespace Asp_Grigoras_Alexandru_Ionel_Rp.Data
{
    public class Asp_Grigoras_Alexandru_Ionel_RpContext : DbContext
    {
        public Asp_Grigoras_Alexandru_Ionel_RpContext (DbContextOptions<Asp_Grigoras_Alexandru_Ionel_RpContext> options)
            : base(options)
        {
        }

        public DbSet<Asp_Grigoras_Alexandru_Ionel_Rp.Models.Movie> Movie { get; set; }
    }
}
