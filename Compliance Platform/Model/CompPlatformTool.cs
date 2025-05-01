using System.ComponentModel.DataAnnotations.Schema;

namespace Compliance_Platform.Model
{
    public class CompPlatformTool
{
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public DateTime DataPowstania { get; set; }
        public string Status { get; set; }

        [ForeignKey("Owner")]
        public required string OwnerId { get; set; }

        public virtual CompPlatformUser Wlasciciel { get; set; }
    }
}
