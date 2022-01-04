using System.Security.Claims;
using ExampleNET.Forms;
using ExampleNET.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserViewModel = ExampleNET.Forms.UserViewModel;

namespace ExampleNET.Controllers;

public class ProfileController : Controller
{
    private ApplicationDbContext db;
        public ProfileController(ApplicationDbContext context)
        {
            db = context;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModelView model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.MyUsers.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user != null)
                {
                    await Authenticate(model.Email); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        public async Task<IActionResult> Index(int ID)
        {
            var user = await db.MyUsers.FirstAsync(u => u.Id == ID);
            return View(new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                // ProfilePicture = user.ProfilePicture

            }) ;
        }
        //
        // public async Task<IActionResult> ChangeUserData(int ID, UserViewModel model)
        // {
        //     string uniqueFileName = UploadedFile(model);
        //     var user = await db.MyUsers.FirstAsync(u => u.Id == ID);
        //     user.ProfilePicture = uniqueFileName;
        //     await db.SaveChangesAsync();
        //
        //     return RedirectToAction("Index", "Profile", new { id = ID });
        // }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModelView model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.MyUsers.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    db.MyUsers.Add(new User { Email = model.Email, Password = model.Password});
        
                    await db.SaveChangesAsync();

                    await Authenticate(model.Email); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Такой пользователь уже существует");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        // private string UploadedFile(UserViewModel model)
        // {
        //     string uniqueFileName = null;
        //
        //     if (model.ProfileImage != null)
        //     {
        //         string uploadsFolder = Path.Combine(@"C:\Users\Home\Desktop\учеба\programming\asp.net\MyProject\MyProject\wwwroot", "images");
        //         uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
        //         string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //         using (var fileStream = new FileStream(filePath, FileMode.Create))
        //         {
        //             model.ProfileImage.CopyTo(fileStream);
        //         }
        //     }
        //     return uniqueFileName;
        // }
}