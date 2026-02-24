using Microsoft.EntityFrameworkCore;
using MindCarePro.Domain.Entities.Appointments;
using MindCarePro.Domain.Entities.Assistant;
using MindCarePro.Domain.Entities.Patients;
using MindCarePro.Domain.Entities.Psychologists;
using MindCarePro.Domain.Entities.Users;

namespace MindCarePro.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Psychologist> Psychologists=> Set<Psychologist>();
    public DbSet<User> Users => Set<User>();
    
    public DbSet<Appointment> Appointments => Set<Appointment>();
    
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
        
        modelBuilder.Entity<Patient>()
            .HasOne(p => p.User)
            .WithOne() 
            .HasForeignKey<Patient>(p => p.UserId)
            .IsRequired(false) // indica que pode ser null
            .OnDelete(DeleteBehavior.Cascade);

     
        
        modelBuilder.Entity<User>()
            .ToTable("users") // <--- Verifique se no banco é "user" ou "users"
            .HasDiscriminator<string>("UserType")
            .HasValue<User>("User")
            .HasValue<Psychologist>("Psychologist")
            .HasValue<Assistant>("Assistant");
            

        base.OnModelCreating(modelBuilder);
        
    }
}
