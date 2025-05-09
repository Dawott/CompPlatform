using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Compliance_Platform.Model
{
    public class CompPlatformQuestionnaires
{
    public int Id { get; set; }
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        [StringLength(100, ErrorMessage = "Nazwa nie może być dłuższa niż 100 znaków")]
        public string Nazwa { get; set; }
        [StringLength(500, ErrorMessage = "Opis nie może być dłuższy niż 500 znaków")]
        public string Opis { get; set; }
    public bool Aktywny { get; set; }
    public DateTime DataUtworzenia { get; set; }

    [Required]
    public string CreatedById { get; set; }

    public virtual CompPlatformUser CreatedBy { get; set; }
    public virtual ICollection<CompPlatformQuestions> Questions { get; set; }
}
}
