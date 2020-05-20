using Microsoft.EntityFrameworkCore;
using BlazorWithEFCoreCosmos.Entity;

namespace BlazorWithEFCoreCosmos.Infrastructure.Cosmos
{
    public class CosmosDbContext : DbContext
    {
        public CosmosDbContext(DbContextOptions<CosmosDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region DefaultContainer
            // modelBuilder.HasDefaultContainer("Store");
            #endregion

            #region Container
            modelBuilder.Entity<ToDo>().ToContainer("ToDo");
            #endregion

            #region NoDiscriminator
            // modelBuilder.Entity<Tax>()
            //     .HasNoDiscriminator();
            #endregion

            #region Converter
            // var converter = new GuidToStringConverter();
            // modelBuilder.Entity<Tax>()
            //     .Property("Id")
            //     .HasConversion(converter);
            #endregion

            #region PartitionKey
            // modelBuilder.Entity<Tax>()
            //      .HasPartitionKey(t => t.Id);
            #endregion

            #region PropertyNames
            // modelBuilder.Entity<Tax>().OwnsOne(
            //     o => o.ShippingAddress,
            //     sa =>
            //     {
            //         sa.ToJsonProperty("Address");
            //         sa.Property(p => p.Street).ToJsonProperty("ShipsToStreet");
            //         sa.Property(p => p.City).ToJsonProperty("ShipsToCity");
            //     });
            #endregion

            #region Owns


            #endregion
        }


        #region DbSet
        public DbSet<ToDo> ToDo { get; set; }
        #endregion
    }
}



