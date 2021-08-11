using Microsoft.EntityFrameworkCore;

namespace web_api.Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public virtual DbSet<DatabaseClassExample> Buildings { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //ORIGINAL LINES FROM SCAFFOLDING
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer("Server=ISREPORTSDEV;Database=ISREPT01;Trusted_Connection=True;");

                //ADDED THESE LINES BUT DID NOT WANT appsettings.json, WANTED appsettings.Development.json OR appsettigs.Production.json
                //THIS IS HANDLED IN Startup.cs SO COMMENTED OUT HERE.
                //IConfigurationRoot configuration = new ConfigurationBuilder()
                //            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                //            .AddJsonFile("appsettings.json")
                //            .Build();

                //var connection = configuration.GetConnectionString("ISREPT01");
                //optionsBuilder.UseSqlServer(connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}