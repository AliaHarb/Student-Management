using System.ComponentModel.DataAnnotations;

namespace Student_Management.Models
{
    public class UniqueEmailAttribute: ValidationAttribute
    {
        public string msg { get; set; }//to use it by attribute 

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null;
            }

            string email = value.ToString();

            var context = new ApplicationContext();

            Student student = context.Students.
            FirstOrDefault(b => b.Email == email);

            Student sts = (Student)validationContext.ObjectInstance;
            //To Use the validationContext  

            if (student != null)
            {
                return new ValidationResult("Email already exists");
            }

            return ValidationResult.Success;
        }
    }
}
