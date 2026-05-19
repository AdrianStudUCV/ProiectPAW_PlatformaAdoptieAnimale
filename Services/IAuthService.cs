using modelMVC.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace modelMVC.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterViewModel model);
        Task<SignInResult> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
        Task<IdentityResult> UpdateProfileAsync(ApplicationUser user, ProfileViewModel model);
    }
}