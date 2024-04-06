using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;
        private IPasswordHasher<AppUser> passwordHasher;
        public AdminController(UserManager<AppUser> userManager, 
            IPasswordHasher passwordHasher)
        {
            this.userManager = userManager;
            this.passwordHasher = passwordHasher;
        }
        public IActionResult Index()
        {
            return View(this.userManager.Users);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser()
                {
                    UserName = model.Name, Email = model.Email,
                };

                IdentityResult result = await userManager.CreateAsync(appUser, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            AppUser? user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    // s
                }
            }

            return View("Index", userManager.Users);
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, string email, string password)
        {
            AppUser? user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                user.Email = email;
                user.PasswordHash = passwordHasher.HashPassword(user, password);

                IdentityResult result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    // sad
                }
            }

            return View();
        }
    }
}
