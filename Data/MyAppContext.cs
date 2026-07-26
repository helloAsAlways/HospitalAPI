using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data;

public class MyAppContext: DbContext
{
    public MyAppContext(DbContextOptions<MyAppContext> options): base(options) {}
    
    public DbSet<Doctor> Doctor { get; set; }
    public DbSet<Schedule> Schedule { get; set; }
    public DbSet<Appointment> Appointment { get; set; }
    public DbSet<Patient> Patient { get; set; }
    public DbSet<Diagnosis> Diagnosis { get; set; }
    public DbSet<Nurse> Nurse { get; set; }
    public DbSet<TreatmentPlan>  TreatmentPlan { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Map C# class names to your existing snake_case Postgres tables
        modelBuilder.Entity<Patient>().ToTable("patients");
        modelBuilder.Entity<Doctor>().ToTable("doctors");
        modelBuilder.Entity<Nurse>().ToTable("nurses");
        modelBuilder.Entity<Appointment>().ToTable("appointments");
        modelBuilder.Entity<Schedule>().ToTable("schedules");
        modelBuilder.Entity<Diagnosis>().ToTable("diagnoses");
        modelBuilder.Entity<TreatmentPlan>().ToTable("treatment_plans");
 
        // Appointment -> Doctor: keep the appointment if the doctor is removed
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Doctor)
            .WithMany(d => d.Appointments)
            .OnDelete(DeleteBehavior.SetNull);
 
        // Appointment -> Patient: delete appointments if the patient is removed
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .OnDelete(DeleteBehavior.Cascade);
 
        // Diagnosis -> TreatmentPlan: cascade delete
        modelBuilder.Entity<TreatmentPlan>()
            .HasOne(t => t.Diagnosis)
            .WithMany(d => d.TreatementPlans)
            .OnDelete(DeleteBehavior.Cascade);
    }
}