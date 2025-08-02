using Verse;

namespace SurvivalistsAdditions;

public sealed class ThingCountExposable : IExposable
{
    private int count;

    private ThingDef thingDef;


    public ThingCountExposable()
    {
    }


    public ThingCountExposable(ThingDef thingDef, int count)
    {
        this.thingDef = thingDef;
        this.count = count;
    }


    public void ExposeData()
    {
        Scribe_Defs.Look(ref thingDef, "thingDef");
        Scribe_Values.Look(ref count, "count");
        if (Scribe.mode != LoadSaveMode.ResolvingCrossRefs || thingDef != null)
        {
            return;
        }

        Log.Warning("Survivalist's Additions:: Failed to load ThingCount. Setting default.");
        thingDef = DefDatabase<ThingDef>.GetNamedSilentFail("Alpaca_Meat");
        count = count <= 0 ? 10 : count;
    }


    public override string ToString()
    {
        return $"({count}x {(thingDef == null ? "null" : thingDef.defName)})";
    }


    public override int GetHashCode()
    {
        return (thingDef.shortHash + count) << 16;
    }
}