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
                Email = model.Email,
                FullName = model.FullName
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

        public async Task<IdentityResult> UpdateProfileAsync(ApplicationUser user, ProfileViewModel model)
        {
            // Actualizare Nume Complet
            user.FullName = model.FullName;
            // 1. Actualizare Poza de Profil
            if (model.NewProfilePicture != null && model.NewProfilePicture.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profiles");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.NewProfilePicture.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.NewProfilePicture.CopyToAsync(fileStream);
                }
                user.ProfilePictureUrl = "/images/profiles/" + uniqueFileName;
            }

           

            // 3. Schimbare Parola (daca a completat campurile)
            if (!string.IsNullOrEmpty(model.CurrentPassword) && !string.IsNullOrEmpty(model.NewPassword))
            {
                var passResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (!passResult.Succeeded)
                {
                    return passResult; // Returnam eroarea (ex: parola curenta e gresita)
                }
            }

            // Salvam utilizatorul in baza de date
            return await _userManager.UpdateAsync(user);
        }
        public async Task<IdentityResult> PromoteToAdminAsync(string email)
        {
            // Cautam utilizatorul in baza de date dupa email
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Utilizatorul cu acest email nu a fost găsit." });
            }

            // Verificam daca rolul de Admin exista, daca nu, il cream
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Îi adăugăm rolul de Admin
            return await _userManager.AddToRoleAsync(user, "Admin");
        }
    }
}