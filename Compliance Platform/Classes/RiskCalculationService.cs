using Compliance_Platform.Model;

namespace Compliance_Platform.Classes
{
    public class RiskCalculationService
    {
        public(decimal Score, string Level) CalculateRisk(List<CompPlatformInstanceAnswer> answers)
        {
            // Kalkulacja na bazie odpowiedzi i ich wag
            decimal totalRisk = answers.Sum(a => a.WagaRyzykaPojedyncza);

            // Ostateczny poziom ryzyka
            string riskLevel = DetermineRiskLevel(totalRisk);

            return (totalRisk, riskLevel);
        }

        private string DetermineRiskLevel(decimal totalRisk)
        {
            // Threshold
            if (totalRisk < 50) return "System niskiego ryzyka";
            if (totalRisk < 150) return "System średniego ryzyka";
            if (totalRisk < 250) return "System wysokiego ryzyka";
            return "System zakazany";
        }
    }
}
