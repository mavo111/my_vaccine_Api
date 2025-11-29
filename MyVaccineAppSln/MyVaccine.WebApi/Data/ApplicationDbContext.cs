using Microsoft.EntityFrameworkCore;
using MyVaccine.WebApi.Models;

namespace MyVaccine.WebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Dose> Doses { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientVaccine> PatientVaccines { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Evitar CASCADE múltiple
            modelBuilder.Entity<PatientVaccine>()
                .HasOne(pv => pv.Patient)
                .WithMany()
                .HasForeignKey(pv => pv.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PatientVaccine>()
                .HasOne(pv => pv.Vaccine)
                .WithMany()
                .HasForeignKey(pv => pv.VaccineId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PatientVaccine>()
                .HasOne(pv => pv.Dose)
                .WithMany()
                .HasForeignKey(pv => pv.DoseId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
