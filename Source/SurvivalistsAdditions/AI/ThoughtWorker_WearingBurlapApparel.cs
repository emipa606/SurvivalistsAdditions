using RimWorld;
using Verse;

namespace SurvivalistsAdditions;

public class ThoughtWorker_WearingBurlapApparel : ThoughtWorker
{
    protected override ThoughtState CurrentStateInternal(Pawn p)
    {
        string text = null;
        var num = 0;
        var wornApparel = p.apparel.WornApparel;
        foreach (var apparel in wornApparel)
        {
            if (apparel.Stuff != SrvDefOf.SRV_Burlap ||
                !apparel.def.apparel.layers.Contains(ApparelLayerDefOf.OnSkin) &&
                !apparel.def.apparel.layers.Contains(ApparelLayerDefOf.Overhead))
            {
                continue;
            }

            if (text == null)
            {
                text = apparel.def.label;
            }

            num++;
        }

        if (num == 0)
        {
            return ThoughtState.Inactive;
        }

        return num >= 4 ? ThoughtState.ActiveAtStage(3, text) : ThoughtState.ActiveAtStage(num - 1, text);
    }
}