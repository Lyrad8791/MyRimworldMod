using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Control
{
    public class AnalSex : DisciplinaryAction
    {
        public AnalSex(Pawn master, Pawn receiver) : base(master, receiver)
        {
        }

        public override int Duration => 50;

        protected override List<SkillRequirement> SkillRequirements => new List<SkillRequirement>();

        protected override List<HediffDef> hediffsToApplyAtEndReceiver => new List<HediffDef>();

        protected override List<HediffDef> hediffsToApplyAtEndMaster => new List<HediffDef>();

        protected override List<ThoughtDef> tougthsToApplyAtEndReceiver => new List<ThoughtDef> { ThoughtDef.Named("HadSex_Thought") };

        protected override List<ThoughtDef> toughtsToApplyAtEndMaster => new List<ThoughtDef> { ThoughtDef.Named("HadSex_Thought") };

        protected override List<NeedModification> needGainsToApplyAtEndMaster => new List<NeedModification> {new NeedModification("Libido",x=> x.CurLevelPercentage = 0)};
        protected override List<NeedModification> needGainsToApplyAtEndReceiver => new List<NeedModification> { new NeedModification("Libido", x => x.CurLevelPercentage = 0), new NeedModification("Discipline", x => x.CurLevelPercentage = 1f) };


    }
}