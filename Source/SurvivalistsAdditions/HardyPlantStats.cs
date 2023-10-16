using Verse;

namespace SurvivalistsAdditions;

public class HardyPlantStats : DefModExtension
{
    public readonly float maxGrowthTemperature = 45f;
    public readonly float maxOptimalGrowthTemperature = 35f;
    public readonly float minGrowthTemperature = -10f;
    public readonly float minOptimalGrowthTemperature = 0f;
    public float maxLeaflessTemperature = -12f;

    public float minLeaflessTemperature = -18f;
}