using RimWorld;
using System.Linq;
using Verse;

namespace Control
{
    public class ThoughtWorker_MaleAttraction : ThoughtWorker
    {
        protected override ThoughtState CurrentSocialStateInternal(Pawn p, Pawn otherPawn)
        {
            if (p.gender == Gender.Male || otherPawn.gender == Gender.Female)
            {
                return ThoughtState.ActiveAtStage(-99999);
            }
            int mySkills = p.skills.skills.Sum(x => x.Level);
            int manSkills = otherPawn.skills.skills.Sum(x => x.Level);
            int delta = manSkills = mySkills;
            if (delta < 0)
            {
                return ThoughtState.ActiveAtStage(0);
            }
            else if(delta < 8)
            {
                return ThoughtState.ActiveAtStage(1);
            }
            else if (delta < 15)
            {
                return ThoughtState.ActiveAtStage(2);
            }
            else if (delta < 23)
            {
                return ThoughtState.ActiveAtStage(3);

            }
            else if (delta < 30)
            {
                return ThoughtState.ActiveAtStage(4);
            }
            else if (delta >= 30)
            {
                return ThoughtState.ActiveAtStage(4);
            }
            return ThoughtState.ActiveAtStage(-99999);

        }
    }

}