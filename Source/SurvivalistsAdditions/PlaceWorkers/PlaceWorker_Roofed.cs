using Verse;

namespace SurvivalistsAdditions;

public class PlaceWorker_Roofed : PlaceWorker
{
    public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map,
        Thing thingToIgnore = null, Thing thing = null)
    {
        foreach (var current in GenAdj.CellsOccupiedBy(loc, rot, checkingDef.Size))
        {
            if (!map.roofGrid.Roofed(current))
            {
                return new AcceptanceReport("SRV_NeedsRoof".Translate(checkingDef.LabelCap));
            }
        }

        return true;
    }
}