using Microsoft.EntityFrameworkCore;

namespace AdoptABuddy.Models
{
    public class AdoptBuddyContext : DbContext
    {
        public AdoptBuddyContext(DbContextOptions<AdoptBuddyContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<AdoptionApplication> AdoptionApplications { get; set; }
    }
}