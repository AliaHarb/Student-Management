using System.ComponentModel.DataAnnotations;

namespace Student_Management.ViewModel
{
    public class LoginViewModelcs
    {
        [Required (ErrorMessage ="*") ]
        public String Name { get; set; }
        [DataType (DataType.Password)]
        public String Password { get; set; }
        [Display(Name="Remember Me")]
        public bool RememberMe { get; set; }

    }
}
