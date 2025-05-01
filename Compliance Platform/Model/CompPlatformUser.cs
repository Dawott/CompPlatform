using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Compliance_Platform.Model
{
    public class CompPlatformUser : IdentityUser
{
        [Required(ErrorMessage = "Imię jest wymagane!")]
        public required string Imie { get; set; }
        [Required(ErrorMessage = "Nazwisko jest wymagane!")]
        public required string Nazwisko { get; set; }
        public string? Departament { get; set; }
        [Required(ErrorMessage = "Podaj rolę użytkownika!")]
        public required string Rola { get; set; }

        public virtual ICollection<CompPlatformTool>? Tool { get; set; }
        public virtual ICollection<CompPlatformInstance>? ToolInstance { get; set; }
    }
}
