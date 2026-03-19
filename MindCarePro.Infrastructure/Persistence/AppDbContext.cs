using Microsoft.EntityFrameworkCore;
using MindCarePro.Domain.Entities.Appointments;
using MindCarePro.Domain.Entities.Assistant;
using MindCarePro.Domain.Entities.Patients;
using MindCarePro.Domain.Entities.Psychologists;
using MindCarePro.Domain.Entities.Sessions;
using MindCarePro.Domain.Entities.Users;

namespace MindCarePro.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Psychologist> Psychologists=> Set<Psychologist>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Appointment> Appointments => Set<Appointment>();

    public DbSet<Session> Sessions => Set<Session>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(entity.GetTableName()?.ToLower());
        }
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.User)
            .WithMany() 
            .HasForeignKey(a => a.UserId)
            .IsRequired(); 

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany() 
            .HasForeignKey(a => a.PatientId)
            .IsRequired();

        modelBuilder.Entity<Session>()
            .HasOne(s => s.User)
            .WithMany()
            .HasForeignKey(s => s.UserId)
            .IsRequired();

        modelBuilder.Entity<Session>()
            .HasOne(s => s.Patient)
            .WithMany()
            .HasForeignKey(s => s.PatientId)
            .IsRequired();
        
        modelBuilder.Entity<Patient>()
            .HasOne(p => p.User)
            .WithMany(u => u.Patients)
            .HasForeignKey(p => p.UserId)
            .IsRequired(false) // indica que pode ser null
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<User>()
            .ToTable("users") 
            .HasDiscriminator<string>("UserType")
            .HasValue<User>("User")
            .HasValue<Psychologist>("Psychologist")
            .HasValue<Assistant>("Assistant");
        
        base.OnModelCreating(modelBuilder);
        
    }
}
