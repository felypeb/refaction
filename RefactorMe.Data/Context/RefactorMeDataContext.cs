using RefactorMe.Model.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace RefactorMe.Data.Context
{
    public partial class RefactorMeDataContext : DbContext
    {
        public RefactorMeDataContext(string connectionStringName)
            : base(connectionStringName)
        {
        }

        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductOption> ProductOption { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<RefactorMeDataContext>(null);

            ConfigureConventions(modelBuilder);

            ConfigureCustomProperties(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void ConfigureConventions(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        private static void ConfigureCustomProperties(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties().Where(p => p.Name == p.ReflectedType.Name + "Id").Configure(p => p.IsKey());

            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(100));
        }
    }
}
