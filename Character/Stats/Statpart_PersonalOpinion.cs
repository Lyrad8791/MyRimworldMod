using RimWorld;
using Verse;

namespace Control
{
    public class Statpart_PersonalOpinion : StatPart
    {
        
        public override string ExplanationPart(StatRequest req)
        {
            return "Returns Peronal opinion";
        }

        public override void TransformValue(StatRequest req, ref float val)
        {
            float opinion = CultManager.GetOpinionOfLeader(req.Thing as Pawn);
            
            float mult = 0;
            if (opinion < 0)
            {
                mult = 0f;
            }
            else if (opinion < 20)
            {
                mult = 0.2f;
            }
            else if (opinion < 40)
            {
                mult = 0.4f;
            }
            else if (opinion < 60)
            {
                mult = 0.6f;
            }
            else if (opinion < 60)
            {
                mult = 0.8f;
            }
            else if (opinion < 80)
            {
                mult = 1f;
            }
            else if (opinion >= 80)
            {
                mult = 1.2f;
            }
            val=mult;
        }
    }

}