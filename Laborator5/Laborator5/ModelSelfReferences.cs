using Laborator5.Entities;
using System.Data.Entity;

namespace Laborator5
{
    public class ModelSelfReferences : DbContext
    {
        public virtual DbSet<SelfReference> SelfReferences { get; set; }
        public ModelSelfReferences() : base("name=ModelSelfReferences")
        { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                    modelBuilder.Entity<SelfReference>()
                    .HasMany(m => m.References)
                    .WithOptional(m => m.ParentSelfReference);
        }
    }
}