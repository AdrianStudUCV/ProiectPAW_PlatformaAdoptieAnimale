using AdoptABuddy.Models;
using modelMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace modelMVC.Services
{
    public interface IMedicalRecordService
    {
        Task CreateRecordAsync(MedicalRecord record);
        
    }
}