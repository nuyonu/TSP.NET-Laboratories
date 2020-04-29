using Microsoft.EntityFrameworkCore;
using PostCommentDatabase.Models;

namespace PostCommentDatabase
{
    public class PostCommentContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-DDRTRS2\NUYONUSQL;Initial Catalog=PostCommentGRPC;Integrated Security=True");
        }
    }
}
