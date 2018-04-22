namespace Unnamed.Data
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using Unnamed.Data.Models;

    public class UnnamedDbContext : DbContext, IUnnamedDbContext
    {
        public UnnamedDbContext()
            : base("DefaultConnection")
        {
        }

        public static UnnamedDbContext Create()
        {
            return new UnnamedDbContext();
        }

        public new DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<Author> Authors { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
