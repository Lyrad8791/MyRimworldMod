using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace Control
{
    public class ThoughtWorker_StatusDifference : ThoughtWorker
    {
        protected override ThoughtState CurrentSocialStateInternal(Pawn p, Pawn otherPawn)
        {
            float myStatus = StatusUtil.GetStatus(p);
            float otherStatus = StatusUtil.GetStatus(otherPawn);
            float delta = myStatus - otherStatus;
            if (delta < 0)
            {
                return ThoughtState.ActiveAtStage(0);
            }
            else
            {
                return ThoughtState.ActiveAtStage(1);
            }
            
        }
    }
    public class Thought_StatusDifference : Thought_SituationalSocial
    {
          
    }
    public static class StatusUtil
    {
        public static float GetStatus(Pawn pawn)
        {
            if (pawn.gender == Gender.Male)
            {
                return GetSocialInfluence(pawn) * 0.4f + GetSkillStatus(pawn) * 0.6f;
            }
            else
            {
                return GetSocialInfluence(pawn) * 0.65f + GetSkillStatus(pawn) * 0.35f;
            }
        }
        static float GetSocialInfluence(Pawn pawn)
        {
           return CultManager.OpinionCacheLookup[pawn.GetHashCode()].TotalOpinion * 0.1f;
        }
        static float GetSkillStatus(Pawn pawn)
        {
            return pawn.skills.skills.Sum(x => x.levelInt);
        }
        
    }


}