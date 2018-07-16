using RimWorld;
using Verse;

namespace Control
{
    public class Statpart_FriendOpinion : StatPart
    {
        public override string ExplanationPart(StatRequest req)
        {
            return "Returns Influence from friends";
        }

        public override void TransformValue(StatRequest req, ref float val)
        {
            var pawn = req.Thing as Pawn;
            var opawns = Find.VisibleMap.mapPawns.FreeColonists;
            var cache = CultManager.OpinionCacheLookup[pawn.GetHashCode()];
            float totalOpinionFactored = 0;
            int numOpawns=0;
            foreach (var opawn in opawns)
            {
                if (!(opawn.GetHashCode()==pawn.GetHashCode()) && opawn.GetHashCode()!=CultManager.Leader.GetHashCode())
                {
                    totalOpinionFactored += cache.GetOpinionOfOther(opawn) * Hediff_Committed.GetSeverityForPawn(opawn);
                    numOpawns++;
                }
            }
            totalOpinionFactored /= numOpawns/4f;
            if (totalOpinionFactored < 0)
            {

            }
            else if (totalOpinionFactored < 10)
            {
                val = .1f;
            }            
            else if (totalOpinionFactored < 20)
            {
                val = .3f;
            }
            else if (totalOpinionFactored < 40)
            {
                val = .5f;
            }
            else if (totalOpinionFactored >= 50)
            {
                val = .7f;
            }
        }
    }

}