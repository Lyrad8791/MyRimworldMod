using RimWorld;
using System;
using Verse;
using Verse.AI;

namespace Control
{

    public class JobGiver_HaveSex : ThinkNode_JobGiver
    {
        float minLibido = 0.5f;

        protected override Job TryGiveJob(Pawn pawn)
        {
            return null;
            //var nearbyPawns = PawnFinder.GetNearbyPawns(pawn);
            //if (!(nearbyPawns.Count > 0))
            //{
            //    return null;
            //}
            //Predicate<Pawn> validator = delegate (Pawn pawn3)
            //{
            //    return !pawn3.Downed && pawn3.CanCasuallyInteractNow(false) && !pawn3.IsForbidden(pawn) && pawn3.Faction == pawn.Faction && pawn != pawn3;
            //};
            //var properpawns = nearbyPawns.FindAll(validator);
            //Log.Message("Proper : " + properpawns.Count);
            //if (!(properpawns.Count > 0))
            //{
            //    return null;
            //}

            //Pawn female = properpawns.MinBy(x => Need_Discipline.GetVal(x));
            //if (female == null || Need_Discipline.GetVal(female) > minDiscipline)
            //{
            //    return null;
            //}
            //return new Job(DefDatabase<JobDef>.GetNamed("Discipline"), female);
        }

    }
}