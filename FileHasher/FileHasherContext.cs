using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;

namespace FileHasher
{
    public class FileHasherContext : DbContext
    {
        public FileHasherContext() : base(new SQLiteConnection()
        {
            ConnectionString = new SQLiteConnectionStringBuilder()
            {
                DataSource = @"C:\Users\AuthBase\source\repos\AuthBaseSystemIOMonitor\FileHasher\FileHasher.db",
                ForeignKeys = true
            }.ConnectionString
        }, true)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<File> File { get; set; }
    }
}
