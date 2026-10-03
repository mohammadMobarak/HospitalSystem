using HospitalSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace HospitalSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.; initial catalog = HospitalDb ; Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure 1-to-Many Relationship
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Initial Doctors Data
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { DoctorId = 1, Name = "Dr. Sarah Johnson", Specialization = "Cardiology", ImageUrl = "/images/doctors/doctor1.jpg" },
                new Doctor { DoctorId = 2, Name = "Dr. Michael Chen", Specialization = "Neurology", ImageUrl = "/images/doctors/doctor2.jpg" },
                new Doctor { DoctorId = 3, Name = "Dr. Emily Davis", Specialization = "Pediatrics", ImageUrl = "/images/doctors/doctor3.jpg" },
                new Doctor { DoctorId = 4, Name = "Dr. James Wilson", Specialization = "Cardiology", ImageUrl = "/images/doctors/doctor4.jpg" },
                new Doctor { DoctorId = 5, Name = "Dr. Sophia Martinez", Specialization = "Dermatology", ImageUrl = "/images/doctors/doctor5.jpg" },
                new Doctor { DoctorId = 6, Name = "Dr. Robert Taylor", Specialization = "Orthopedics", ImageUrl = "/images/doctors/doctor6.jpg" }
            );
        }
    }
}
