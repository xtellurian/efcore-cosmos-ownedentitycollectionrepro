using Microsoft.EntityFrameworkCore;
using src.Model;

namespace src.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Site>(s =>
            {
                s.Property(_ => _.Id).ValueGeneratedOnAdd();
                s.OwnsMany(_ => _.Buildings, b =>
                {
                    b.Property(_ => _.Id).ValueGeneratedOnAdd();
                    b.OwnsMany(_ => _.Devices, d => {
                        d.Property(_ => _.Id).ValueGeneratedOnAdd();
                    });

                    b.OwnsMany(_ => _.Rooms, r => {
                        r.Property(_ => _.Id).ValueGeneratedOnAdd();
                    });
                });
            });
        }

        public DbSet<Site> Sites { get; set; }
    }
}