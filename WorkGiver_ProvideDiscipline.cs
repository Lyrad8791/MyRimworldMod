using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace MyRimworldMod
{
    // RimWorld.JobGiver_Berserk
    using RimWorld;
    using Verse;
    using Verse.AI;

    public class WorkGiver_ProvideDiscipline : WorkGiver
    {

        bool isProperTarget(Pawn victim)
        {
           
            if (victim == null)
                return false;
            if (victim.Downed || !victim.RaceProps.Humanlike)
            {
                Log.Message("Victim not a good target");
                return false;
            }
            Log.Message("Found Target");
            return true;
        }


        public override Job NonScanJob(Pawn pawn)
        {
            var targets = pawn.Map.mapPawns.SpawnedPawnsInFaction(pawn.Faction);
            for (int i = 0; i < targets.Count; i++)
            {
                if (isProperTarget(targets[i]))
                {
// var job = new Job(JobDefOf.AttackMelee, targets[i]);
                    var job = new Job(DefDatabase<JobDef>.GetNamed("Driver_Discipline"), targets[i]);
                    Log.Message("Built job");
                    return job;

                }
            }
            return null;
        }

   
    }
}
