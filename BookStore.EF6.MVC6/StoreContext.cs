using BookStore.EF6.Configurations;
using BookStore.EF6.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.EF6
{
    [DbConfigurationType(typeof(CodeConfig))]
    class StoreContext : DbContext
    {

        static StoreContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<StoreContext>());
        }

        public StoreContext() 
            : base("Server=.;Database=BookStore;Integrated Security=SSPI;")
        {
        }

        public DbSet<EBook> Books { get; set; }
        public DbSet<EAuthor> Authors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new EBookConfiguration());
            modelBuilder.Configurations.Add(new EAuthorConfiguration());
        }
    }

    class CodeConfig : DbConfiguration
    {
        public CodeConfig()
        {
            SetDefaultConnectionFactory(new System.Data.Entity.Infrastructure.SqlConnectionFactory());
            SetProviderServices("System.Data.SqlClient",
                System.Data.Entity.SqlServer.SqlProviderServices.Instance);
        }
    }
}
