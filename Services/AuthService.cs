using Microsoft.AspNetCore.Identity;
using modelMVC.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace modelMVC.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            // 1. Logica de stocare si salvare a imaginii de profil pe disc
            if (model.ProfilePicture != null && model.ProfilePicture.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profiles");

                // Ne asiguram ca folderul exista pe disc
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Generam un nume unic ca sa nu se suprascrie pozele cu acelasi nume
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfilePicture.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ProfilePicture.CopyToAsync(fileStream);
                }

                // Salvam calea relativa in baza de date
                user.ProfilePictureUrl = "/images/profiles/" + uniqueFileName;
            }
            else
            {
                // O poza implicita daca utilizatorul nu incarca nimic
                user.ProfilePictureUrl = "/images/profiles/default-avatar.png";
            }

            // 2. Crearea efectiva a utilizatorului in baza de date
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // 3. Verificam si cream rolul daca acesta nu exista inca in baza de date
                if (!await _roleManager.RoleExistsAsync(model.Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(model.Role));
                }

                // 4. Atribuim rolul selectat utilizatorului
                await _userManager.AddToRoleAsync(user, model.Role);

                // Autentificam automat utilizatorul dupa inregistrare
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            // Apelam mecanismul de Login din Identity
            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}