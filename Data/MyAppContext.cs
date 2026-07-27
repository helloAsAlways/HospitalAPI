using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data;

public class MyAppContext: DbContext
{
    public MyAppContext(DbContextOptions<MyAppContext> options): base(options) {}
    
    public DbSet<Doctor> Doctor { get; set; }
    public DbSet<Appointment> Appointment { get; set; }
    public DbSet<Patient> Patient { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<MedicalRecord> MedicalRecords { get; set; }
    
    public DbSet<Nurse> Nurses { get; set; }
    public DbSet<Diagnosis> Diagnosis { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<TreatmentPlan> TreatmentPlans { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Map C# class names to your existing snake_case Postgres tables
        modelBuilder.Entity<Patient>().ToTable("patients");
        modelBuilder.Entity<Doctor>().ToTable("doctors");
        modelBuilder.Entity<Appointment>().ToTable("appointments");
        modelBuilder.Entity<Person>().ToTable("persons");
        modelBuilder.Entity<MedicalRecord>().ToTable("medical_records"); 
            
        modelBuilder.Entity<Nurse>().ToTable("nurses");
        modelBuilder.Entity<Diagnosis>().ToTable("diagnoses");
        modelBuilder.Entity<Schedule>().ToTable("schedules");
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
        
        modelBuilder.Entity<Appointment>()
            .Property(a => a.Status)
            .HasConversion(
                v => v.ToString().ToLower(),
                v => (AppointmentStatus)Enum.Parse(typeof(AppointmentStatus), v, true));
 
        // Diagnosis -> TreatmentPlan: cascade delete
        modelBuilder.Entity<TreatmentPlan>()
            .HasOne(t => t.Diagnosis)
            .WithMany(d => d.TreatmentPlans)
            .OnDelete(DeleteBehavior.Cascade);
        
        
        modelBuilder.Entity<Patient>()
            .HasOne(p => p.Person)
            .WithOne(pe => pe.Patient)
            .HasForeignKey<Patient>(p => p.PersonId);

        modelBuilder.Entity<Doctor>()
            .HasOne(d => d.Person)
            .WithOne(pe => pe.Doctor)
            .HasForeignKey<Doctor>(d => d.PersonId);
        
        modelBuilder.Entity<Nurse>()
            .HasOne(n => n.Person)
            .WithOne(pe => pe.Nurse)
            .HasForeignKey<Nurse>(n => n.PersonId);
        
        modelBuilder.Entity<Schedule>()
            .HasOne(s => s.Doctor)
            .WithMany(d => d.Schedules)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Diagnosis>()
            .HasOne(d => d.Appointment)
            .WithOne(a => a.Diagnosis)
            .HasForeignKey<Diagnosis>(d => d.AppointmentId);
        

    }
}
