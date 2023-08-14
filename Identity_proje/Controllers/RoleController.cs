using Identity_proje.Models;
using Identity_proje.Models.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Identity_proje.Controllers
{
    public class RoleController : Controller
    {
        [Authorize]
            // [Authorize(Roles = "CEO,Yönetici")]   kimlik tanıtmak yetmez kimliği doğru oolan ve rolu bunlardan biri olan byuradaki actionlara  ulaşabilir

        public IActionResult Create() => View();  // buradaki => öyleki ifadesi  {return} ile aynı işlevi görüyor

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<IdentityRole>roleManager,UserManager<AppUser> userManager)
        {
          _roleManager = roleManager;
          _userManager = userManager;
        }

        [HttpPost]

        public async Task<IActionResult> Create([Required(ErrorMessage ="İsim Boş Olamaz")][MinLength(3,ErrorMessage ="En az 3 karakter olmalı")]string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole() { Name = name });
                if (result.Succeeded)
                {
                    return RedirectToAction("List"); //Rollerin listesi 
                }

            }
            return View();
        }
       
        public IActionResult List () => View(_roleManager.Roles);

        public async Task<IActionResult> AssignUser(string id)
        {
            IdentityRole identityRole = await _roleManager.FindByIdAsync(id);

            List<AppUser> hasRole = new List<AppUser>();
            List<AppUser> hasNotRole = new List<AppUser>();

            foreach (var item in _userManager.Users)
            {
                var list = (await _userManager.IsInRoleAsync(item, identityRole.Name)) ? hasRole : hasNotRole;
                list.Add(item);
            }

            AssignVM vm = new AssignVM() { RoleName = identityRole.Name, HasNotRole = hasNotRole, HasRole = hasRole };
            return View(vm);

        }


        [HttpPost]

        public async Task<IActionResult> AssignUser(AssignVM vM)

        {
            IdentityResult result;

            foreach (var item in vM.AddIds ?? new string[] {})
            {
                AppUser appUser = await _userManager.FindByIdAsync(item);
                result = await _userManager.AddToRoleAsync(appUser, vM.RoleName);

            }
            foreach (var item in vM.DeleteIds ?? new string[] { })
            {
                AppUser appUser = await _userManager.FindByIdAsync(item);
                result = await _userManager.RemoveFromRoleAsync(appUser, vM.RoleName);

            }

            return RedirectToAction("List");
        }
    }
}
