using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;

namespace Control
{
    

    public class Capacity_CriticalThinking : PawnCapacityWorker
    {
        public override float CalculateCapacityLevel(HediffSet diffSet, List<PawnCapacityUtility.CapacityImpactor> impactors = null)
        {
            return base.CalculateCapacityAndRecord(diffSet, PawnCapacityDefOf.Consciousness, impactors);
        }
        public static float GetAuthority(Pawn pawn)
        {
            return pawn.health.capacities.GetLevel(DefDatabase<PawnCapacityDef>.GetNamed("Authority"));
        }
    }

    //public class ThoughtWorker_HadSex : ThoughtWorker
    //{
    //    protected override ThoughtState CurrentStateInternal(Pawn p)
    //    {
    //        return true;
    //    }
    //}
}