using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Control
{
    public abstract class DisciplinaryAction 
    {
        protected Pawn master;
        protected Pawn receiver;

        public DisciplinaryAction(Pawn master, Pawn receiver)
        {
            this.master = master;
            this.receiver = receiver;
        }

        abstract public int Duration { get; }
        abstract protected List<SkillRequirement> SkillRequirements { get; }

        abstract protected List<HediffDef> hediffsToApplyAtEndReceiver { get; }
        abstract protected List<HediffDef> hediffsToApplyAtEndMaster { get; }

        abstract protected List<ThoughtDef> tougthsToApplyAtEndReceiver { get; }
        abstract protected List<ThoughtDef> toughtsToApplyAtEndMaster { get; }

        abstract protected List<NeedModification> needGainsToApplyAtEndMaster { get; }
        abstract protected List<NeedModification> needGainsToApplyAtEndReceiver { get; }
        
        public bool PawnSatisfiesSkillRequirements()
        {
            return SkillRequirements == null || SkillRequirements.Any((SkillRequirement req) => !req.PawnSatisfies(master));
        }
        public virtual void ApplyEndEffects()
        {

            hediffsToApplyAtEndReceiver.ForEach(x => HediffMaker.MakeHediff(x, receiver));
            hediffsToApplyAtEndMaster.ForEach(x => HediffMaker.MakeHediff(x, master));
            tougthsToApplyAtEndReceiver.ForEach(x => receiver.needs.mood.thoughts.memories.TryGainMemory(x));
            toughtsToApplyAtEndMaster.ForEach(x => master.needs.mood.thoughts.memories.TryGainMemory(x));
            needGainsToApplyAtEndReceiver.ForEach(x => x.Apply(receiver));
            needGainsToApplyAtEndMaster.ForEach(x => x.Apply(master));

        }
        


        



    }
}