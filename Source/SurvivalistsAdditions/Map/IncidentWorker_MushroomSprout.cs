using RimWorld;
using Verse;

namespace SurvivalistsAdditions;

public class IncidentWorker_MushroomSprout : IncidentWorker
{
    private const int MinRoomCells = 25;
    private const int SpawnRadius = 6;
    private static readonly IntRange CountRange = new IntRange(10, 20);

    protected override bool CanFireNowSub(IncidentParms parms)
    {
        if (!base.CanFireNowSub(parms))
        {
            return false;
        }

        var map = (Map)parms.target;
        return TryFindRootCell(map, out _);
    }


    protected override bool TryExecuteWorker(IncidentParms parms)
    {
        var map = (Map)parms.target;
        if (!TryFindRootCell(map, out var root))
        {
            return false;
        }

        Thing thing = null;
        var randomInRange = CountRange.RandomInRange;
        for (var i = 0; i < randomInRange; i++)
        {
            if (!CellFinder.TryRandomClosewalkCellNear(root, map, SpawnRadius, out var intVec, x => CanSpawnAt(x, map)))
            {
                break;
            }

            var plant = intVec.GetPlant(map);
            plant?.Destroy();

            var thing2 = GenSpawn.Spawn(SrvDefOf.SRV_Mushroom, intVec, map);
            if (thing == null)
            {
                thing = thing2;
            }
        }

        if (thing == null)
        {
            return false;
        }

        SendStandardLetter(parms, null);
        return true;
    }


    private bool TryFindRootCell(Map map, out IntVec3 cell)
    {
        return CellFinderLoose.TryFindRandomNotEdgeCellWith(10,
            x => CanSpawnAt(x, map) && x.GetRoom(map).CellCount >= MinRoomCells, map, out cell);
    }


    private bool CanSpawnAt(IntVec3 c, Map map)
    {
        if (!c.Standable(map) || c.Fogged(map) || c.GetEdifice(map) != null || c.GetRoom(map).OpenRoofCount > 0 ||
            map.terrainGrid.TerrainAt(c).layerable || !map.terrainGrid.TerrainAt(c).defName.Contains("Rough"))
        {
            return false;
        }

        var plant = c.GetPlant(map);
        if (plant != null && plant.def.plant.growDays > 10f)
        {
            return false;
        }

        var thingList = c.GetThingList(map);
        foreach (var thing in thingList)
        {
            if (thing.def == SrvDefOf.SRV_Mushroom)
            {
                return false;
            }
        }

        return true;
    }
}