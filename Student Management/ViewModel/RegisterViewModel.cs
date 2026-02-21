using System.ComponentModel.DataAnnotations;

namespace Student_Management.ViewModel
{
    public class RegisterViewModel
    {
       public string UserName { get; set; }
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }



    }
}
