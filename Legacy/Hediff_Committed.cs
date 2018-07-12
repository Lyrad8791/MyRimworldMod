//using System.Collections.Generic;
//using System.Linq;
//using RimWorld;
//using Verse;

//namespace Control
//{
//    public class Hediff_Committed : HediffWithComps
//    {
//        public bool isLeader = false;
//        public static Dictionary<int, float> severityLookup = new Dictionary<int, float>();
//        public static List<Pawn> affectedPawns = new List<Pawn>();
//        const float dividerConst = 1e-5f;
//        static PawnCapacityDef CritThink
//        {
//            get { return DefDatabase<PawnCapacityDef>.GetNamed("CriticalThinking");}
//        }
//        public override void Tick()
//        {


//        }
//        public override void PostTick()
//        {


//        }

//        //public void InfluenceOthers()
//        //{
//        //    var influenceTargets = InfluenceCalculator.GetInfluenceTargets();
//        //    int totalPriority = influenceTargets.Sum(x => x.InfluencePriority);
//        //    float myInfluenceScore = InfluenceCalculator.GetInfluenceOffense(pawn);
//        //    foreach (var influenceTarget in influenceTargets)
//        //    {
//        //        influenceTarget.GetInfluenced(ControlLevel * myInfluenceScore * influenceTarget.InfluencePriority/totalPriority);
//        //    }
//        //} 
//    }


//}
