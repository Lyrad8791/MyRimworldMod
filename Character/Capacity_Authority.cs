using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;

namespace MyRimworldMod
{
    

    public class Capacity_Authority : PawnCapacityWorker
    {
        public override float CalculateCapacityLevel(HediffSet diffSet, List<PawnCapacityUtility.CapacityImpactor> impactors = null)
        {
           
            return base.CalculateCapacityAndRecord(diffSet, PawnCapacityDefOf.Consciousness, impactors);
        }
    }
}