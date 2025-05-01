using System.ComponentModel.DataAnnotations.Schema;

namespace Compliance_Platform.Model
{
    public class CompPlatformQuestions
{
        public int Id { get; set; }
        [ForeignKey("QuestionnaireId")]
        public int QuestionnaireId { get; set; }  // FK do CompPlatformQuestionnaires
        public string Tresc { get; set; }
        public string TypPytania { get; set; }  // "Multichoice", "Tak/Nie" itp.
        public int OrderIndex { get; set; }
        public string Kategoria { get; set; } 
        public bool Wymagane { get; set; } // Do przemyślenia - w założeniu true by default
        public decimal WagaRyzyka { get; set; } // Podać a priori kalkulację ryzyka

        public virtual CompPlatformQuestionnaires Questionnaires { get; set; }
        public virtual ICollection<CompPlatformAnswers> Answers { get; set; }
    }
}
