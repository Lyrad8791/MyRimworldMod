using RimWorld;
using System;
using UnityEngine;
using Verse;
using Verse.AI;

namespace MyRimworldMod
{
    public class WorkGiver_Discipline : WorkGiver
    {
        float minDiscipline = 0.3f;
        float maxDist = 30;
        public override Job NonScanJob(Pawn pawn)
        {
            Log.Message("Giving Job");
            var nearbyPawns = PawnFinder.GetNearbyPawns(pawn);
            if (!(nearbyPawns.Count > 0))
            {
                return null;
            }
            Predicate<Pawn> validator = delegate (Pawn pawn3)
            {

                return !pawn3.Downed && pawn3.CanCasuallyInteractNow(false) && !pawn3.IsForbidden(pawn) && pawn3.Faction == pawn.Faction;
            };
            var properpawns = nearbyPawns.FindAll(validator);
            if (!(properpawns.Count > 0))
            {
                return null;
            }
            Pawn female = properpawns.MinBy(x => Need_Discipline.GetVal(x));
            if (female == null || Need_Discipline.GetVal(female) < minDiscipline)
            {
                return null;
            }
            return new Job(DefDatabase<JobDef>.GetNamed("Discipline"), female);
        }
    }
    }
