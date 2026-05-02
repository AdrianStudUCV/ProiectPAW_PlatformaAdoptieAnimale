using Microsoft.EntityFrameworkCore;
using modelMVC.Models;

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
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<AdoptionApplication> AdoptionApplications { get; set; }
    }
}