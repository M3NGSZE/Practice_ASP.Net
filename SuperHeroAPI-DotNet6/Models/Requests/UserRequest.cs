using System.ComponentModel.DataAnnotations;

namespace SuperHeroAPI_DotNet6.Models.Requests
{
    public class UserRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(100, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must include at least 1 special character, 1 uppercase letter, 1 lowercase letter, 1 number")]
        public string Password { get; set; } 
    }
}
