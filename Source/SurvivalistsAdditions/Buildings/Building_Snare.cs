using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace SurvivalistsAdditions;

public class Building_Snare : Building_TrapDamager
{
    private readonly List<Pawn> touchingPawns = new List<Pawn>();
    private Pawn affectedPawn;
    private bool rearmAfterCleared;
    public float stunDuration = 20;

    public bool KnowsOfSnare(Pawn p)
    {
        if (p.Faction == null && p.RaceProps.Animal)
        {
            return false;
        }

        return KnowsOfTrap(p);
    }

    private bool IsValidAnimal(Pawn p)
    {
        if (!p.RaceProps.Animal)
        {
            return false;
        }

        if (p.BodySize > 1f)
        {
            return false;
        }

        return p.Faction != Faction.OfPlayer && p.HostFaction != Faction.OfPlayer ||
               p.RaceProps.trainability.intelligenceOrder < TrainabilityDefOf.Intermediate.intelligenceOrder;
    }

    private void CheckSpring(Pawn p)
    {
        if (Rand.Chance(SpringChance(p)))
        {
            Spring(p);
        }
    }

    private void CheckAutoRebuild(Map map)
    {
        if (map != null)
        {
            GenConstruct.PlaceBlueprintForBuild(def, Position, map, Rotation, Faction.OfPlayer, Stuff);
        }
    }

    private void ApplyDamage(Pawn p)
    {
        if (p.BodySize > 1f)
        {
            return;
        }

        if (p.BodySize is >= 0.6f and <= 1f)
        {
            p.health.AddHediff(SrvDefOf.SRV_SnaredLarge);
        }
        else
        {
            if (!p.health.hediffSet.HasHediff(SrvDefOf.SRV_SnaredSmall))
            {
                NotifyPlayer(p);
            }

            p.health.AddHediff(SrvDefOf.SRV_SnaredSmall);
        }
    }


    private void RemoveHediff()
    {
        if (affectedPawn == null)
        {
            return;
        }

        var largeDiff = affectedPawn.health.hediffSet.hediffs.Find(x => x.def == SrvDefOf.SRV_SnaredLarge);
        if (largeDiff != null)
        {
            affectedPawn.health.RemoveHediff(largeDiff);
        }

        var smallDiff = affectedPawn.health.hediffSet.hediffs.Find(x => x.def == SrvDefOf.SRV_SnaredSmall);
        if (smallDiff != null)
        {
            affectedPawn.health.RemoveHediff(smallDiff);
        }

        // Try to enter either a berserk or panicking state as a result of being snared
        // Larger animals are more likely to go berserk, smaller animals are more likely to flee
        if (Rand.Chance(affectedPawn.BodySize))
        {
            affectedPawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Berserk, null, true);
        }
        else if (!Rand.Chance(affectedPawn.BodySize))
        {
            affectedPawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.PanicFlee, null, true);
        }
    }


    private void NotifyPlayer(Pawn p)
    {
        // If the notification type isn't None, try to notify the player
        if (SrvSettings.Snare_NotificationType == NotificationType.None)
        {
            return;
        }

        // Determine if this is a positive notification or not
        var isPositive = !(p.Faction == Faction.OfPlayer || p.HostFaction == Faction.OfPlayer);
        // Check if this notification is allowed
        if ((!isPositive || !SrvSettings.Snare_AllowPositiveNotification) &&
            (isPositive || !SrvSettings.Snare_AllowNegativeNotification))
        {
            return;
        }

        // If the notification type is SilentText
        if (SrvSettings.Snare_NotificationType == NotificationType.SilentText)
        {
            Messages.Message("SRV_LetterSnareTriggered".Translate(p.LabelShort, p),
                new TargetInfo(Position, Map),
                MessageTypeDefOf.SilentInput
            );
        }
        // If the notification type is TextWithSound
        else if (SrvSettings.Snare_NotificationType == NotificationType.TextWithSound)
        {
            Messages.Message("SRV_LetterSnareTriggered".Translate(p.LabelShort, p),
                new TargetInfo(Position, Map),
                isPositive ? MessageTypeDefOf.PositiveEvent : MessageTypeDefOf.NegativeEvent
            );
        }
        // If the notification type is Letter
        else
        {
            Find.LetterStack.ReceiveLetter("SRV_LetterSnareTriggeredLabel".Translate(p.LabelShort, p),
                "SRV_LetterSnareTriggered".Translate(p.LabelShort, p),
                isPositive ? LetterDefOf.PositiveEvent : LetterDefOf.NegativeEvent,
                new TargetInfo(Position, Map)
            );
        }
    }

    #region properties

    public int Difficulty => 1;

    public float FailChance => SrvSettings.Snare_FailChance * Difficulty / 100f;

    public float BreakChance => SrvSettings.Snare_BreakChance * Difficulty / 100f;

    #endregion

    #region Overrides

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref rearmAfterCleared, "rearmAfterCleared", true);
    }


    public override void Tick()
    {
        if (!Spawned)
        {
            return;
        }

        //check to see if we're sprung
        var thingList = Position.GetThingList(Map);
        // ReSharper disable once ForCanBeConvertedToForeach
        for (var i = 0; i < thingList.Count; i++)
        {
            if (thingList[i] is not Pawn pawn || touchingPawns.Contains(pawn))
            {
                continue;
            }

            touchingPawns.Add(pawn);
            CheckSpring(pawn);
        }
    }

    protected override float SpringChance(Pawn p)
    {
        if (!IsValidAnimal(p))
        {
            return 0;
        }

        var num = KnowsOfSnare(p) ? 0.008f : this.GetStatValue(StatDefOf.TrapSpringChance);

        if (p.Faction == Faction.OfPlayer || p.HostFaction == Faction.OfPlayer)
        {
            num *= 0.5f;
        }

        return Mathf.Clamp01(num);
    }

    public override ushort PathWalkCostFor(Pawn p)
    {
        return KnowsOfSnare(p) ? (ushort)30 : (ushort)0;
    }

    public override ushort PathFindCostFor(Pawn p)
    {
        return !KnowsOfSnare(p) ? (ushort)0 : (ushort)800;
    }

    public override bool IsDangerousFor(Pawn p)
    {
        return KnowsOfSnare(p);
    }

    protected override void SpringSub(Pawn p)
    {
        if (p != null && Rand.Value > FailChance)
        {
            affectedPawn = p;
            ApplyDamage(p);
        }

        foreach (var pawn in touchingPawns)
        {
            if (pawn.Dead || !pawn.health.hediffSet.HasHediff(SrvDefOf.SRV_SnaredLarge) &&
                !pawn.health.hediffSet.HasHediff(SrvDefOf.SRV_SnaredSmall))
            {
                continue;
            }

            Map.designationManager.RemoveAllDesignationsOn(this);
            rearmAfterCleared = true;
            break;
        }

        CheckAutoRebuild(Map);

        base.SpringSub(p);
    }

    #endregion
}