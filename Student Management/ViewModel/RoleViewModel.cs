using System.ComponentModel.DataAnnotations;

namespace Student_Management.ViewModel
{
    public class RoleViewModel
    {
        [Display(Name="Role Name")]
        public string RoleName { get; set; }

    }
}
