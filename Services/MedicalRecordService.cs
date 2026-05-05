using AdoptABuddy.Models;
using modelMVC.Models;
using modelMVC.Repositories;
using System.Threading.Tasks;

namespace modelMVC.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IRepository<MedicalRecord> _repository;

        public MedicalRecordService(IRepository<MedicalRecord> repository)
        {
            _repository = repository;
        }

        public async Task CreateRecordAsync(MedicalRecord record)
        {
            await _repository.AddAsync(record);
            await _repository.SaveAsync();
        }
    }
}