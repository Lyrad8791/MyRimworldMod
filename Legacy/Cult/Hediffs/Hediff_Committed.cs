using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace Control
{
    public class Hediff_Committed : HediffWithComps
    {
        public bool isLeader = false;
        static Dictionary<int, Hediff_Committed> lookup = new Dictionary<int, Hediff_Committed>();
        public float delta;
        const float dividerConst = 1e-5f;
        int cnt = 0;
        bool added = false;
        StatDef InfluenceGainFromLeader;
        StatDef InfluenceGainFromFriends;
        PawnCapacityDef CritThink;
  
        public static float GetSeverityForPawn(Pawn pawn)
        {
            int hash = pawn.GetHashCode();
            if (lookup.ContainsKey(hash))
            {
                return lookup[hash].Severity;
            }
            return 0;
        }
        public static Hediff_Committed GetHediffForPawn(Pawn pawn)
        {
            int hash = pawn.GetHashCode();
            if (lookup.ContainsKey(hash))
            {
                return lookup[hash];
            }
            return null;
        }
        public override void Tick()
        {
            if (!added)
            {
                Log.Message("Added");
                lookup.Add(pawn.GetHashCode(), this);
                InfluenceGainFromLeader = DefDatabase<StatDef>.GetNamed("InfluenceGainFromLeader");
                InfluenceGainFromFriends = DefDatabase<StatDef>.GetNamed("InfluenceGainFromFriends");
                CritThink = DefDatabase<PawnCapacityDef>.GetNamed("CriticalThinking");
                added = true;
            }
            cnt++;
            if (!isLeader)
            {
                
                delta = pawn.GetStatValue(InfluenceGainFromLeader) + pawn.GetStatValue(InfluenceGainFromFriends) - CurStageIndex * 0.2f * pawn.health.capacities.GetLevel(CritThink);
                Severity += delta / GenDate.TicksPerDay;
            }
        }


        //public void InfluenceOthers()
        //{
        //    var influenceTargets = InfluenceCalculator.GetInfluenceTargets();
        //    int totalPriority = influenceTargets.Sum(x => x.InfluencePriority);
        //    float myInfluenceScore = InfluenceCalculator.GetInfluenceOffense(pawn);
        //    foreach (var influenceTarget in influenceTargets)
        //    {
        //        influenceTarget.GetInfluenced(ControlLevel * myInfluenceScore * influenceTarget.InfluencePriority/totalPriority);
        //    }
        //} 
    }


}
