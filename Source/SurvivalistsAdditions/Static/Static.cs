using UnityEngine;
using Verse;

namespace SurvivalistsAdditions;

[StaticConstructorOnStartup]
public static class Static
{
    public const int GenericWaitDuration = 200;

    public static readonly string ModName = "SRV_ModName".Translate();
    public static string TemperatureTrans = "BadTemperature".Translate().ToLower();
    public static string NoIngredient = "SRV_NoIngredient".Translate();
    public static string SmokerLocked = "SRV_SmokerLocked".Translate();
    public static string Food = "SRV_Food".Translate();
    public static string DisableSnare = "SRV_MenuOption_DisableSnare".Translate();
    public static string TemperatureRangeLower = "SRV_TemperatureRangeLower".Translate();

    public static string Label_ITabSmoker = "SRV_Label_ITabSmoker".Translate();
    public static readonly string Label_ITabCharPit = "SRV_Label_ITabCharPit".Translate();
    public static readonly string Label_AllowPositiveLetter = "SRV_Label_Snare_AllowPositive".Translate();
    public static readonly string Label_AllowNegativeLetter = "SRV_Label_Snare_AllowNegative".Translate();
    public static readonly string Label_NotificationType = "SRV_Label_Snare_NotificationType".Translate();

    public static readonly string ToolTip_VinegarBarrel_MaxCapacity =
        "SRV_ToolTip_VinegarBarrel_MaxCapacity".Translate();

    public static readonly string ToolTip_VinegarBarrel_FermentDays =
        "SRV_ToolTip_VinegarBarrel_FermentDays".Translate();

    public static readonly string ToolTip_CheeseBarrel_MaxCapacity = "SRV_ToolTip_CheeseBarrel_MaxCapacity".Translate();
    public static readonly string ToolTip_CheeseBarrel_AgingDays = "SRV_ToolTip_CheeseBarrel_AgingDays".Translate();
    public static readonly string ToolTip_Smoker_MaxCapacity = "SRV_ToolTip_Smoker_MaxCapacity".Translate();
    public static readonly string ToolTip_Smoker_SmokeHours = "SRV_ToolTip_Smoker_SmokeHours".Translate();
    public static readonly string ToolTip_Smoker_TendHours = "SRV_ToolTip_Smoker_TendHours".Translate();
    public static readonly string ToolTip_CharcoalPit_MaxCapacity = "SRV_ToolTip_CharcoalPit_MaxCapacity".Translate();
    public static readonly string ToolTip_CharcoalPit_BurnHours = "SRV_ToolTip_CharcoalPit_BurnHours".Translate();

    public static readonly string ToolTip_CharcoalPit_CharcoalPerWoodLog =
        "SRV_ToolTip_CharcoalPit_CharcoalPerWoodLog".Translate();

    public static readonly string ToolTip_Snare_FailChance = "SRV_ToolTip_Snare_FailChance".Translate();
    public static readonly string ToolTip_Snare_BreakChance = "SRV_ToolTip_Snare_BreakChance".Translate();
    public static readonly string ToolTip_Snare_AllowPositiveLetter = "SRV_ToolTip_Snare_AllowPositive".Translate();
    public static readonly string ToolTip_Snare_AllowNegativeLetter = "SRV_ToolTip_Snare_AllowNegative".Translate();
    public static readonly string ToolTip_Snare_NotificationType = "SRV_ToolTip_Snare_NotificationType".Translate();
    public static readonly string ToolTip_GenStep_PlantDensity = "SRV_ToolTip_GenStep_PlantDensity".Translate();

    public static Graphic Graphic_CharcoalPitFilled = GraphicDatabase.Get<Graphic_Single>(
        "Cupro/Object/Utility/CharcoalPit/FullPit", ShaderDatabase.DefaultShader, new Vector2(3, 3), Color.white);

    public static readonly Material BarUnfilledMat_Generic =
        SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.2f, 0.22f, 0.22f));

    public static readonly Vector2 BarSize_Generic = new(0.55f, 0.1f);
    public static readonly Color BarZeroProgressColor_Generic = new(0.4f, 0.27f, 0.22f);
    public static readonly Color BarZeroProgressColor_CharcoalPit = new(0.522f, 0.38f, 0.263f);
    public static readonly Color BarZeroProgressColor_Smoker = new(0.9f, 0.4f, 0.2f);
    public static readonly Color BarFullColor_Generic = new(0.9f, 0.85f, 0.2f);
    public static readonly Color BarFullColor_Smoker = new(0.376f, 0.25f, 0.125f);


    public static string ToHumanString(this NotificationType nt)
    {
        if (nt == NotificationType.None)
        {
            return "SRV_NotificationType_None".Translate();
        }

        if (nt == NotificationType.SilentText)
        {
            return "SRV_NotificationType_SilentText".Translate();
        }

        if (nt == NotificationType.TextWithSound)
        {
            return "SRV_NotificationType_TextWithSound".Translate();
        }

        if (nt == NotificationType.Letter)
        {
            return "SRV_NotificationType_Letter".Translate();
        }

        return "ERROR";
    }
}