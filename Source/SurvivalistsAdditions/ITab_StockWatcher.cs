using RimWorld;
using UnityEngine;
using Verse;

namespace SurvivalistsAdditions;

public class ITab_StockWatcher : ITab
{
    public ITab_StockWatcher()
    {
        size = new Vector2(300f, 100f);
        labelKey = Static.Label_ITabCharPit;
    }

    public int MinimumToAdd { get; private set; } = 1;


    protected override void FillTab()
    {
        Text.Font = GameFont.Small;
        var fullRect = new Rect(10f, 10f, size.x - 10, size.y - 10).GetInnerRect();
        var leftRect = fullRect.LeftHalf().Rounded();
        var rightRect = fullRect.RightHalf().Rounded();
        GUI.BeginGroup(fullRect);

        Widgets.Label(leftRect, "Pause at: ");
        MinimumToAdd = (int)Widgets.HorizontalSlider_NewTemp(rightRect, MinimumToAdd, 1, 50);

        GenUI.AbsorbClicksInRect(fullRect);
        GUI.EndGroup();
    }
}