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
            var moodPercent = pawn.needs.mood.CurLevelPercentage;
            float moodfactor = 1f;
            if (moodPercent<0.4)
            {
                moodfactor = 0.2f;
            }
            else if (moodPercent < 0.5)
            {
                moodfactor = 0.4f;
            }
            else if (moodPercent < 0.6)
            {
                moodfactor = 0.65f;
            }
            else if (moodPercent < 0.7)
            {
                moodfactor = 0.75f;
            }
            else if (moodPercent < 0.8)
            {
                moodfactor = 0.85f;
            }
            float otherfactor =  moodfactor * researchSkillFactor * 2f;
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