using RimWorld;
using System;
using Verse;
using Verse.AI;
using DubsBadHygiene;
using System.Linq;

namespace Control
{

    public class JobGiver_HaveSex : ThinkNode_JobGiver
    {
        float sexTrigger { get { return NeedUtil.SexTrigger; } }
        float maxBladder { get{ return NeedUtil.BladderTrigger; } }
        protected override Job TryGiveJob(Pawn pawn)
        {
            var failconditions =
                pawn.gender != Gender.Male
                || !pawn.IsColonist;
            
            if (failconditions)
            {
                Log.Message("Failed : " + pawn.Name.ToStringShort);
                return null;
            }

            var nearbyPawns = PawnFinder.GetNearbyPawns(pawn);
            var females = nearbyPawns.Where(x => x.gender == Gender.Female && x.CanCasuallyInteractNow());
            if (females.Count() == 0)
            {
                return null;
            }
            var female = females.MinBy(x => NeedUtil.GetSexNeed(x).CurLevelPercentage);
            if (female == null)
            {
                Log.Message("Cant find other pawn");
                return null;
            }
            failconditions = (NeedUtil.GetSexNeed(pawn).CurLevelPercentage > sexTrigger
                && pawn.needs.TryGetNeed<Need_Bladder>().CurLevelPercentage > maxBladder
                && NeedUtil.GetSexNeed(female).CurLevelPercentage > sexTrigger);
            if (failconditions)
            {
                return null;
            }
            
            Log.Message("Found other pawn");
            return new Job(DefDatabase<JobDef>.GetNamed("HaveSex"), female);
        }
    }

}
