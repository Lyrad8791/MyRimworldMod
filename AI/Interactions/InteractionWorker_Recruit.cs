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

            var hediff = HediffMaker.MakeHediff(CultManager.CommittedHediffdef, recipient);
            recipient.health.AddHediff(hediff);
            hediff.Severity = 0.05f;
        }

        public override float RandomSelectionWeight(Pawn initiator, Pawn recipient)
        {
            
            if (!initiator.health.hediffSet.HasHediff(CultManager.CommittedHediffdef) ||
                recipient.health.hediffSet.HasHediff(CultManager.CommittedHediffdef))
            {
                
                return 0;
            }
            if (Hediff_Committed.GetHediffForPawn(initiator).CurStageIndex <= 1)
            {
                return 0;
            }
            return 10000000f;
            // Get my opinion of cultleader
            // 
        }
    }        
}