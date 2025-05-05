using System.ComponentModel.DataAnnotations.Schema;

namespace Compliance_Platform.Model
{
    public class CompPlatformVerificationHistory
    {
        public int Id { get; set; }

        [ForeignKey("InstanceId")]
        public int InstanceId { get; set; }  // FK do instancji kwestionariusza

        [ForeignKey("AuditorId")]
        public string AuditorId { get; set; }  // FK do audytora

        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        public string Comment { get; set; }
        public DateTime VerificationDate { get; set; }

        public virtual CompPlatformInstance Instance { get; set; }
        public virtual CompPlatformUser Auditor { get; set; }

    }
}
