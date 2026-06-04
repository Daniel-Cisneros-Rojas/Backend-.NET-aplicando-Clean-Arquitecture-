using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Persistence
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //se definen las tablas de la base de datos, con eso se crea por defecto con los datos que tiene person entity definidos
        public DbSet<personEntity> Persons { get; set; }

        //esto es para darlo especifico, se le dice que la tabla se llamara Persons, sino se le dice nada se llamaria personEntity
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<personEntity>(entity =>
            {
                entity.ToTable("Persons");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();

                entity.Property(e => e.Code).IsRequired().HasMaxLength(20);
                entity.HasIndex(e=> e.Code).IsUnique();

                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);

                entity.Property(e=>e.LastName).IsRequired().HasMaxLength(50);

                entity.Property(e=>e.Email).IsRequired().HasMaxLength(100);

                entity.Property(e=>e.PhoneNumber).IsRequired().HasMaxLength(15);

                entity.Ignore(e => e.FullName);
                
                entity.Property<DateTime>("CreateAt").IsRequired().HasDefaultValueSql("GETUTCDATE()");

                entity.Property<DateTime>("UpdatedAt").IsRequired().HasDefaultValueSql("GETUTCDATE()");

            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken= default)
        {
            //extras
            UpdateTimeStamps();
            return base.SaveChangesAsync(cancellationToken);

        }

        private void UpdateTimeStamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e=> e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.Metadata.FindProperty("UpdatedAt")!=null){
                    entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
                } 
                
            }
        }
    }
}
