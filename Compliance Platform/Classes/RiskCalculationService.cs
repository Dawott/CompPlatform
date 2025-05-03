using Compliance_Platform.Model;

namespace Compliance_Platform.Classes
{
    public class RiskCalculationService
    {
        // Defaultowe wagi dla kategorii pytań
        private readonly Dictionary<string, decimal> _categoryWeights = new Dictionary<string, decimal>
        {
            { "PrawaPodstawowe", 1.5m },        // Współczynnik najwyższy z oczywistych względow
            { "Bezpieczenstwo", 1.4m },         // Zagadnienia bezpieczeństwa
            { "Przejrzystosc", 1.2m },          // Wymogi przejrzystości
            { "NadzorLudzki", 1.3m },           // Uwzględnienie nadzoru ludzkiego
            { "DataGovernance", 1.1m },         // Jakość danych i tzw. governance
            { "OdpornoscTechniczna", 1.0m },    // Oporność technologiczna i "data precision" (w to wliczymy DORE?)
            { "Inne", 1.0m }                    // Inne bez kategorii (ale lepiej uniknąć)
        };

        private readonly Dictionary<string, decimal> _systemTypeMultipliers = new Dictionary<string, decimal>
        {
            { "PrzeznaczenieOgolne", 0.8m },        // Systemy AI do przeznaczenia ogólnego
            { "DiagnostykaMedyczna", 1.5m },        // Systemy do przeznaczeń medycznych
            { "Biometria", 2.0m },                  // Systemy biometrii zdalnej
            { "Profilowanie", 2.5m },               // Systemy profilujące poza udziałem służb porządkowych
            { "KrytycznaInfrastruktura", 1.7m },    // Systemy infrastruktury krytycznej
            { "EgzekwowaniePrawa", 1.6m },          // Systemy służb porządkowych
            { "OcenaEdukacji", 1.2m },              // Systemy oceny w edukacji
            { "OcenaZatrudnienia", 1.3m },          // Wszystkie systemy uczestniczące w ocenie zatrudnienia i decyzjach z nimi związanych
            { "Inne", 1.0m }                        // Inne systemy
        };

        private readonly List<(decimal Threshold, string Level, string Description)> _riskLevels = new List<(decimal, string, string)>
        {
            (0, "Ryzyko niskie", "Ogólne systemy niskiego ryzyka wg AI Act"),
            (100, "Ryzyko ograniczone", "Systemy wymagające podwyższonego nadzoru, ale nie podpadające pod systemy wysokiego ryzyka"),
            (200, "Wysokie ryzyko", "Systemy wykazujące wysokie ryzyko według AI Act i wymagające dodatkowych regulacji wewnątrz organizacji oraz systemu raportowania"),
            (350, "System Zakazany", "System, którego poziom ryzyka pod żadnym pozorem nie zezwala na użycie wewnątrz organizacji lub w ramach świadczenia usług na terenie UE")
        };

        // Tutaj dzieje się magia tzn. obliczanie ryzyka
        public (decimal Score, string Level, string Description, Dictionary<string, decimal> CategoryScores) CalculateRisk(List<CompPlatformInstanceAnswer> answers, CompPlatformTool tool)
        {
            // Wprowadzenie kalkulacji
            var categoryScores = _categoryWeights.Keys.ToDictionary(k => k, k => 0m);

            // Grupowanie po kategoriach
            foreach (var answer in answers)
            {
                // Pobranie kategorii (Inne domyślnie)
                var category = answer.CompPlatformQuestions?.Kategoria ?? "Inne";

                // Pobranie wag (Inne domyślnie)
                decimal categoryWeight = _categoryWeights.ContainsKey(category) ? _categoryWeights[category] : _categoryWeights["Inne"];

                decimal weightedScore = answer.WagaRyzykaPojedyncza * categoryWeight;

                
                if (!categoryScores.ContainsKey(category))
                {
                    categoryScores[category] = 0;
                }
                categoryScores[category] += weightedScore;
            }

            // Oblicz pełne ryzyko
            decimal totalRiskScore = categoryScores.Values.Sum();

            // Zastosuj system mnożenia
            string systemType = tool.Kategoria ?? "Inne";
            decimal systemMultiplier = _systemTypeMultipliers.ContainsKey(systemType)
                ? _systemTypeMultipliers[systemType]
                : _systemTypeMultipliers["Inne"];

            totalRiskScore *= systemMultiplier;

            // Przekaż poziom ryzyka
            var riskLevel = DetermineRiskLevel(totalRiskScore);

            return (totalRiskScore, riskLevel.Level, riskLevel.Description, categoryScores);
        }

        private (string Level, string Description) DetermineRiskLevel(decimal totalRisk)
        {
            // Dopasuj ryzyko do poziomu na bazie thresholdów
            for (int i = _riskLevels.Count - 1; i >= 0; i--)
            {
                if (totalRisk >= _riskLevels[i].Threshold)
                {
                    return (_riskLevels[i].Level, _riskLevels[i].Description);
                }
            }

            // Fallback
            return (_riskLevels[0].Level, _riskLevels[0].Description);
        }

        public Dictionary<string, decimal> CalculateImpactAreas(List<CompPlatformInstanceAnswer> answers)
        {
            var impactAreas = new Dictionary<string, decimal>
            {
                { "Prywatnosc", 0 },
                { "Dyskryminacja", 0 },
                { "Biometria", 0 },
                { "WynikPrzejrzystosci", 0 },
                { "WynikNadzoruLudzkiego", 0 },
                { "WynikOdpornosciCyfrowej", 0 }
            };

            // TBD

            return impactAreas;
        }

    }
}
