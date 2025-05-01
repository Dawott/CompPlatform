using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Compliance_Platform.Model
{
    public class CompPlatformInstanceAnswer
    {
        //Odpowiedzi dla instancji
        public int Id { get; set; }
        [ForeignKey("InstanceId")]
        public int InstanceId { get; set; }  // FK do instancji
        [ForeignKey("CompPlatformQuestions")]
        public int QuestionTemplateId { get; set; }  // FK do wzorców
        public string OdpowiedzTekst { get; set; }  // Wolny tekst
        [ForeignKey("OpcjaOdpowiedziId")]
        public int? OpcjaOdpowiedziId { get; set; }  // Dla zdefiniowanych punktów
        public decimal WagaRyzykaPojedyncza { get; set; }  // Doliczane do ostatecznego wyniku
        public DateTime DataOdpowiedzi { get; set; }
        [ForeignKey("WnioskujacyId")]
        public string WnioskujacyId { get; set; }

        public virtual CompPlatformInstance CompPlatformInstance { get; set; }
        
        public virtual CompPlatformQuestions CompPlatformQuestions { get; set; }
        
        public virtual CompPlatformAnswers OpcjaOdpowiedzi { get; set; }
        public virtual CompPlatformUser Wnioskujacy { get; set; }
    }
}
