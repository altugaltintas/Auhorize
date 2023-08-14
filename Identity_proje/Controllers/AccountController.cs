using Identity_proje.Models;
using Identity_proje.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Identity_proje.Controllers
{
    [Authorize]   // sayfaya erişmek için giriş yapılamsı gerekiyor authorize bu işe yarıyo r
    public class AccountController :Controller
    {
        

        private readonly UserManager<AppUser> _userManager;  // User manager içerisindeki metotdlar önemli tüm işlemleri orası yapıyor  yaratma silme ve birçok metod mevcut
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser>userManager,SignInManager<AppUser>signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [AllowAnonymous]   //Register actionunua kendini tanıtmamış kişilerde login olmamış kişilerde gelebilir demek oluyor anonime izin ver gibi 
        //kimiliği belirlenmiş 
       public IActionResult Register()
        {
            return View();

        }

        [HttpPost,AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDTO dTO)

        {
            if (ModelState.IsValid)  //validasyon tammammı?
              
            {
                AppUser appUser = new AppUser() { Email = dTO.Mail, UserName = dTO.UserName };
                IdentityResult result= await _userManager.CreateAsync(appUser,dTO.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");

                }

            }
        return View(dTO);
        }


        [AllowAnonymous]
        public IActionResult Login(string retunUrl)
        {
            return View( new LoginDTO() {ReturnUrl=retunUrl });
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task <IActionResult> Login(LoginDTO dTO)
        {
            if (ModelState.IsValid)
            {     //async çalışan bir metod var ise   üst metodunda async çalışması gerekir   public async Task <IActionResult> yazıması  gibi
                
                
                AppUser appUser=await _userManager.FindByNameAsync(dTO.UserName);
                if (appUser != null)
                {


                    SignInResult result = await _signInManager.PasswordSignInAsync(dTO.UserName, dTO.Password, false, false); //PasswordSignInAsync SignInManager kütüphanesi ile kontrol ediyoruz
                    if (result.Succeeded)
                    {
                        return Redirect(dTO.ReturnUrl ?? "/"); // dTO.ReturnUrl    gibi bir değer var ise oraya götür yok ise Anasayfaya demiş olduk ?? "/"  ifadesi ile
                    }
                }
                

            }
            return View(dTO);
        }


        public async Task<IActionResult> LogOut()
        { 
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");

        }

    }
}
