using System.Text;
using RimWorld;
using UnityEngine;
using Verse;

namespace SurvivalistsAdditions;

public class HardyPlant : Plant
{
    private HardyPlantStats ext;


    private string hardyCachedLabelMouseover;

    private HardyPlantStats Ext
    {
        get
        {
            if (ext != null)
            {
                return ext;
            }

            if (!def.HasModExtension<HardyPlantStats>())
            {
                Log.Error(
                    $"Survivalists Additions:: No mod extension found for hardy plant - {def.LabelCap}. Assigning default values.");
                def.modExtensions.Add(new HardyPlantStats());
            }

            ext = def.GetModExtension<HardyPlantStats>();

            return ext;
        }
    }

    public override float GrowthRate =>
        GrowthRateFactor_Fertility * HardyGrowthRateFactor_Temperature * GrowthRateFactor_Light;

    public float HardyGrowthRateFactor_Temperature
    {
        get
        {
            if (!GenTemperature.TryGetTemperatureForCell(Position, Map, out var num))
            {
                return 1f;
            }

            if (num < Ext.minOptimalGrowthTemperature)
            {
                return Mathf.InverseLerp(Ext.minGrowthTemperature, Ext.minOptimalGrowthTemperature, num);
            }

            return num > Ext.maxOptimalGrowthTemperature
                ? Mathf.InverseLerp(Ext.maxGrowthTemperature, Ext.maxOptimalGrowthTemperature, num)
                : 1f;
        }
    }

    protected override float LeaflessTemperatureThresh
    {
        get
        {
            var num = 16f;
            return (this.HashOffset() * 0.01f % num) - num + -2f;
        }
    }

    public override string LabelMouseover
    {
        get
        {
            if (hardyCachedLabelMouseover != null)
            {
                return hardyCachedLabelMouseover;
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.Append(def.LabelCap);
            stringBuilder.Append(" (" + "PercentGrowth".Translate(GrowthPercentString));
            if (Dying)
            {
                stringBuilder.Append(", " + "DyingLower".Translate());
            }

            stringBuilder.Append(")");
            hardyCachedLabelMouseover = stringBuilder.ToString();

            return hardyCachedLabelMouseover;
        }
    }


    public bool GrowthSeasonNow(IntVec3 c, Map map)
    {
        var roomOrAdjacent = c.GetRoomOrAdjacent(map, RegionType.Set_All);
        if (roomOrAdjacent == null)
        {
            return false;
        }

        if (roomOrAdjacent.UsesOutdoorTemperature)
        {
            return map.weatherManager.growthSeasonMemory.GrowthSeasonOutdoorsNow;
        }

        var temperature = c.GetTemperature(map);
        return temperature > Ext.minGrowthTemperature && temperature < Ext.maxGrowthTemperature;
    }


    public override void TickLong()
    {
        CheckMakeLeafless();
        if (Destroyed)
        {
            return;
        }

        if (GrowthSeasonNow(Position, Map))
        {
            if (!HasEnoughLightToGrow)
            {
                unlitTicks += 2000;
            }
            else
            {
                unlitTicks = 0;
            }

            var num = growthInt;
            var isMature = LifeStage == PlantLifeStage.Mature;
            growthInt += GrowthPerTick * 2000f;
            if (growthInt > 1f)
            {
                growthInt = 1f;
            }

            if ((!isMature && LifeStage == PlantLifeStage.Mature || (int)(num * 10f) != (int)(growthInt * 10f)) &&
                CurrentlyCultivated())
            {
                Map.mapDrawer.MapMeshDirty(Position, MapMeshFlagDefOf.Things);
            }

            if (def.plant.LimitedLifespan)
            {
                ageInt += 2000;
                if (Dying)
                {
                    var map = Map;
                    var isCrop = IsCrop;
                    var amount = Mathf.CeilToInt(10f);
                    TakeDamage(new DamageInfo(DamageDefOf.Rotting, amount, -1f));
                    if (Destroyed)
                    {
                        if (isCrop && def.plant.Harvestable &&
                            MessagesRepeatAvoider.MessageShowAllowed($"MessagePlantDiedOfRot-{def.defName}", 240f))
                        {
                            Messages.Message("MessagePlantDiedOfRot".Translate(Label).CapitalizeFirst(),
                                new TargetInfo(Position, map), MessageTypeDefOf.NegativeEvent);
                        }

                        return;
                    }
                }
            }
            //Spawn new plants
            //if (def.plant.reproduces && growthInt >= 0.6f && Rand.MTBEventOccurs(def.plant.reproduceMtbDays, 60000f, 2000f))
            //{
            //    if (!PlantUtility.SnowAllowsPlanting(Position, Map))
            //    {
            //        return;
            //    }
            //    GenSpawn.Spawn(plant, Position, Map);
            //}
        }

        hardyCachedLabelMouseover = null;
    }


    public override string GetInspectString()
    {
        var stringBuilder = new StringBuilder();
        if (LifeStage == PlantLifeStage.Growing)
        {
            stringBuilder.AppendLine("PercentGrowth".Translate(GrowthPercentString));
            stringBuilder.AppendLine("GrowthRate".Translate() + ": " + GrowthRate.ToStringPercent());
            if (Resting)
            {
                stringBuilder.AppendLine("PlantResting".Translate());
            }

            if (!HasEnoughLightToGrow)
            {
                stringBuilder.AppendLine("PlantNeedsLightLevel".Translate() + ": " +
                                         def.plant.growMinGlow.ToStringPercent());
            }

            var growthRateFactor_Temperature = HardyGrowthRateFactor_Temperature;
            if (!(growthRateFactor_Temperature < 0.99f))
            {
                return stringBuilder.ToString().TrimEndNewlines();
            }

            if (growthRateFactor_Temperature < 0.01f)
            {
                stringBuilder.AppendLine("OutOfIdealTemperatureRangeNotGrowing".Translate());
            }
            else
            {
                stringBuilder.AppendLine(
                    "OutOfIdealTemperatureRange".Translate(Mathf.RoundToInt(growthRateFactor_Temperature * 100f)
                        .ToString()));
            }
        }
        else if (LifeStage == PlantLifeStage.Mature)
        {
            stringBuilder.AppendLine(def.plant.Harvestable ? "ReadyToHarvest".Translate() : "Mature".Translate());
        }

        return stringBuilder.ToString().TrimEndNewlines();
    }
}