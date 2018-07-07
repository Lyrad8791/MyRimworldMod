using RimWorld;
using System;
using UnityEngine;
using Verse;
using Verse.AI;

namespace MyRimworldMod
{
    public class JobGiver_HaveSex : ThinkNode_JobGiver
    {
        float maxDist = 30;
        public override float GetPriority(Pawn pawn)
        {
            Need_Libido libido = pawn.needs.TryGetNeed<Need_Libido>();
            Log.Message("Trigger " +  libido.Def.ratioSexTrigger + " Libido " + libido.CurLevelPercentage);
            if (libido == null || pawn.gender != Gender.Male)
            {
                return 0f;
            }
            if (libido.CurLevelPercentage >libido.Def.ratioSexTrigger)
            {
                return 9.5f;
            }
            return 0f;
        }

        protected override Job TryGiveJob(Pawn pawn)
        {
            Log.Message("Giving Job");
            if (pawn.gender != Gender.Male || GetPriority(pawn) < 1f)
            {
                return null;
            }
            var nearbyPawns = PawnFinder.GetNearbyPawns(pawn);

            foreach (var nearbyPawn in nearbyPawns)
            {
                Log.Message(nearbyPawn.Name.ToStringShort + " is nearby " + pawn.Name.ToStringShort);
            }
            Predicate<Pawn> validator = delegate (Pawn pawn3)
            {

                return !pawn3.Downed && pawn3.CanCasuallyInteractNow(false) && !pawn3.IsForbidden(pawn) && pawn3.Faction == pawn.Faction;
            };
            var properpawns = nearbyPawns.FindAll(validator);
            foreach (var nearbyPawn in properpawns)
            {
                Log.Message(nearbyPawn.Name.ToStringShort + " is propperly nearby " + pawn.Name.ToStringShort);
            }


            Pawn female = properpawns.MinBy(x => x.needs.TryGetNeed<Need_Libido>().SexualStatus);
            if (female == null)
            {
                return null;
            }
            return new Job(DefDatabase<JobDef>.GetNamed("Fuck"), female);
        }
    }
    }
