using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Student_Management.Models;
using Student_Management.ViewModel;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Student_Management.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser>userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> SaveRegister(RegisterViewModel userViewModel )
        {
            if (ModelState.IsValid)
            {
                //UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(); use inject rather than this
                //Mapping 
                ApplicationUser appUser =new ApplicationUser();
                appUser.Email = userViewModel.Email;
                appUser.UserName=userViewModel.UserName;
                appUser.PasswordHash = userViewModel.Password;
                //Save Database
               IdentityResult result= await userManager.CreateAsync(appUser,userViewModel.Password);
                if (result.Succeeded)
                {
                    //assign to role
                   await userManager.AddToRoleAsync(appUser, "Admin"); //add  IdentityResult result to check

                    //cookie
                    //SignInManager<ApplicationUser> sing = SignInManager<ApplicationUser>();use inject rather than this
                    await signInManager.SignInAsync(appUser, false); //return result also
                    return RedirectToAction("Index", "Department");

                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View("Register",userViewModel);
        }

        public async Task<IActionResult> Logout()
        {
           await signInManager.SignOutAsync();
            return View("Login");
        }
        public IActionResult Login()
        {
            return View("Login");
        }
        [HttpPost]
        public async Task<IActionResult> SaveLogin(LoginViewModelcs loginViewModel)
        {
            if (ModelState.IsValid)
            {
                //check found
                ApplicationUser appUser = await userManager.FindByNameAsync(loginViewModel.Name);
                if(appUser != null)
                {
             bool found= await userManager.CheckPasswordAsync(appUser, loginViewModel.Password);
                    if (found)
                    {//cookie
                     //  await signInManager.SignInAsync(appUser,loginViewModel.RememberMe); //Just set id ,name
                     //to set extra info
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim("UserEmail", appUser.Email));
                        await signInManager.SignInWithClaimsAsync(appUser, loginViewModel.RememberMe, claims);
                        return RedirectToAction("Index", "Department");
                    }
                }
                ModelState.AddModelError("", "UserName or password is false");
            }

            return View("Login",loginViewModel);
        }
    }
}
