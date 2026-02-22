using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Student_Management.ViewModel;

namespace Student_Management.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager) 
        {
            this.roleManager = roleManager;
        }
        public IActionResult AddRole()
        {
            return View("Add Role");
        }
        [HttpPost]
        public async Task<IActionResult> SaveRoleAsync(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            { 
                //Save Db Role
                IdentityRole role = new IdentityRole();
                role.Name = roleViewModel.RoleName;
               IdentityResult result= await roleManager.CreateAsync(role);
                if (result.Succeeded) 
                {
                    ViewBag.Success=true;
                    return View("Add Roles");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View("AddRole",roleViewModel);

        }

    }
}
