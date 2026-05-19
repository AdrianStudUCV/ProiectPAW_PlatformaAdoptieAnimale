using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using modelMVC.Models;
using modelMVC.Services;
using System.Threading.Tasks;

namespace modelMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(IAuthService authService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _authService = authService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: /Auth/Register
        [HttpGet]
        public IActionResult Register()
        {
            // Daca utilizatorul e deja logat, il trimitem la prima pagina
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }

        // POST: /Auth/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.RegisterAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                // Daca Identity returneaza erori (ex: parola prea simpla), le adaugam in formular
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        // GET: /Auth/Login
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        // POST: /Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.LoginAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email sau parolă incorectă.");
            }
            return View(model);
        }

        // POST: /Auth/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
        // GET: /Auth/Profile
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var model = new ProfileViewModel
            {
                // Daca FullName e gol in DB, punem provizoriu partea dinainte de @ din email
                FullName = string.IsNullOrEmpty(user.FullName) ? user.UserName.Split('@')[0] : user.FullName,
                CurrentProfilePictureUrl = user.ProfilePictureUrl
            };

            return View(model);
        }

        // POST: /Auth/Profile
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            // Siguranta: eliminam orice verificare reziduala pentru UserName
            ModelState.Remove("UserName");

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null) return NotFound();

                var result = await _authService.UpdateProfileAsync(user, model);

                if (result.Succeeded)
                {
                    // Forțăm împrospătarea sesiunii ca bara de sus sa preia instant schimbarile
                    await _signInManager.RefreshSignInAsync(user);

                    TempData["SuccessMessage"] = "Profilul tău a fost actualizat cu succes!";
                    return RedirectToAction("Profile");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Daca codul esueaza, reincarcam poza veche ca sa nu apara imagine lipsa pe ecran
            var currentUser = await _userManager.GetUserAsync(User);
            model.CurrentProfilePictureUrl = currentUser?.ProfilePictureUrl;
            return View(model);
        }
    }
}