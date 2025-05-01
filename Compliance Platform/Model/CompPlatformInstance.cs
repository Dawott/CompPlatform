using System.ComponentModel.DataAnnotations.Schema;

namespace Compliance_Platform.Model
{
    public class CompPlatformInstance
    {
        // Poszczególne wypełnione formularze
        public int Id { get; set; }
        [ForeignKey("ToolId")]
        public int ToolId { get; set; }  // FK do Tool
        [ForeignKey("QuestionnaireId")]
        public int QuestionnaireId { get; set; }  // FK do Questionnaires
        public string Status { get; set; }  
        public DateTime DataUtworzenia { get; set; }
        public DateTime? DataZlozenia { get; set; } // Różnica ze względu na możliwość tworzenia draftów 
        public DateTime? DataSprawdzenia { get; set; }
        public string? AudytorId { get; set; }  // FK do AspNetUsers (audytor)
        public decimal KalkulacjaRyzyka { get; set; } //Wg algorytmu
        public string PoziomRyzyka { get; set; }

        public virtual CompPlatformTool CompPlatformTool { get; set; }
        public virtual CompPlatformQuestionnaires CompPlatformQuestionnaires { get; set; }
        public virtual CompPlatformUser Audytor { get; set; }
        public virtual ICollection<CompPlatformInstanceAnswer> Answers { get; set; }
    }
}
