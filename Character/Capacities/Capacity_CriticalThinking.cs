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
            var pawn = diffSet.pawn;
            var researchSkillFactor = pawn.skills.GetSkill(SkillDefOf.Intellectual).Level / 12f;
            float otherfactor = pawn.needs.mood.CurLevelPercentage * researchSkillFactor;
            return CalculateCapacityAndRecord(diffSet, PawnCapacityDefOf.Consciousness, impactors) * otherfactor;
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