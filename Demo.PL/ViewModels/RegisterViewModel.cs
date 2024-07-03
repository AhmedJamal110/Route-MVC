using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class RegisterViewModel
	{
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password" , ErrorMessage ="Password dose not match")]
        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }


    }
}
