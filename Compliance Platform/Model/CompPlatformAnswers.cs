using System.ComponentModel.DataAnnotations.Schema;

namespace Compliance_Platform.Model
{
    public class CompPlatformAnswers
    {
        public int Id { get; set; }
        public int QuestionTemplateId { get; set; }  // FK do pytań
        public string Treść { get; set; }
        public decimal WagaRyzyka { get; set; }  // Waga dla ryzyka odpowiedzi
        public int OrderIndex { get; set; }

        [ForeignKey("QuestionTemplateId")]
        public virtual CompPlatformQuestions Questions { get; set; }
    }
}
