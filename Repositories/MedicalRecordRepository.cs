using AdoptABuddy.Models;
using modelMVC.Models;

namespace modelMVC.Repositories
{
    public class MedicalRecordRepository : Repository<MedicalRecord>
    {
        public MedicalRecordRepository(AdoptBuddyContext context) : base(context)
        {
        }
    }
}