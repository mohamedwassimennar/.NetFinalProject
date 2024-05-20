using AM.ApplicationCore.Domain;
using AM.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.Infrastructure
{
    public class AMContext:DbContext
    {   //DBSets
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Traveller> Travellers { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        //OnConfiguring
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer
                (@"Data Source=(localdb)\mssqllocaldb;
                   Initial Catalog=AirportManagementDB;
                   Integrated Security=true;
                   MultipleActiveResultSets=true");
            //Activer LazyLoading
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new PlaneConfiguration());
            modelBuilder.Entity<Plane>().HasKey(p => p.PlaneId);
            modelBuilder.Entity<Plane>().ToTable("MyPlanes");
            modelBuilder.Entity<Plane>().Property(p => p.Capacity)
                .HasColumnName("PlaneCapacity");
            modelBuilder.ApplyConfiguration(new FlightConfiguration());
            //Configurer un type détenu
            modelBuilder.Entity<Passenger>().OwnsOne(p => p.FullName);
            //Configurer l'héritage Table Per Hierarchy (TPH)
            //modelBuilder.Entity<Passenger>().HasDiscriminator<int>("IsTraveller")
            //                                .HasValue<Passenger>(0)
            //                                .HasValue<Staff>(2)
            //                                .HasValue<Traveller>(1);
            //Configurer l'héritage Table Per Type (TPT)
            modelBuilder.Entity<Staff>().ToTable("Staffs");
            modelBuilder.Entity<Traveller>().ToTable("Travellers");
            //Configurer la clé primaire de la porteuse de données
            modelBuilder.Entity<Ticket>()
                .HasKey(t => new {t.FlightFK,t.PassengerFK});
            base.OnModelCreating(modelBuilder);
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.Properties<DateTime>().HaveColumnType("DateTime");
        }

    }
}
