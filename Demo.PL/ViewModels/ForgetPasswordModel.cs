using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class ForgetPasswordModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
