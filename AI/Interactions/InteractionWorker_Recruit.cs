using System.Collections.Generic;
using RimWorld;
using Verse;

namespace Control
{
    public class InteractionWorker_Recruit : InteractionWorker
    {
        public override void Interacted(Pawn initiator, Pawn recipient, List<RulePackDef> extraSentencePacks)
        {
            var recruitDef = DefDatabase<ThoughtDef>.GetNamed("CTRL_Recruit_ToLeader");
            Thought_Memory newThought = (Thought_Memory)ThoughtMaker.MakeThought(recruitDef);
            recipient.needs.mood.thoughts.memories.TryGainMemory(newThought, CultManager.Leader);
        }
        public override float RandomSelectionWeight(Pawn initiator, Pawn recipient)
        {
            if (recipient == CultManager.Leader)
            {
                return 0;
            }
            float opinionVal = CultManager.GetOpinionOfLeader(initiator) / CultManager.OpinionNormalizer;
            return 10f;
            // Get my opinion of cultleader
            // 
        }
    }



    
}