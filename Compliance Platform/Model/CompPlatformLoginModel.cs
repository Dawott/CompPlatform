using System.ComponentModel.DataAnnotations;

namespace Compliance_Platform.Model
{
    public class CompPlatformLoginModel
    {
        public class LoginModel
        {
            [Required(ErrorMessage = "Email jest wymagany")]
            [EmailAddress(ErrorMessage = "Niewłaściwy format")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "Hasło jest wymagane")]
            public string Password { get; set; } = string.Empty;

            public bool RememberMe { get; set; }
        }
    }
}
