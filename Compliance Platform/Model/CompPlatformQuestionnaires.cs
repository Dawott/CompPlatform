using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Compliance_Platform.Model
{
    public class CompPlatformQuestionnaires
{
    public int Id { get; set; }
    public string Nazwa { get; set; }
    public string Opis { get; set; }
    public bool Aktywny { get; set; }
    public DateTime DataUtworzenia { get; set; }

    [ForeignKey("CreatedById")]
    public string? CreatedById { get; set; }

    public virtual CompPlatformUser CreatedBy { get; set; }
    public virtual ICollection<CompPlatformQuestions> Questions { get; set; }
}
}
