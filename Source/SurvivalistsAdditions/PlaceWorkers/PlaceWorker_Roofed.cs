using System.Linq;
using Verse;

namespace SurvivalistsAdditions;

public class PlaceWorker_Roofed : PlaceWorker
{
    public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map,
        Thing thingToIgnore = null, Thing thing = null)
    {
        if (GenAdj.CellsOccupiedBy(loc, rot, checkingDef.Size).Any(cell => !map.roofGrid.Roofed(cell)))
        {
            return new AcceptanceReport("SRV_NeedsRoof".Translate(checkingDef.LabelCap));
        }

        return true;
    }
}