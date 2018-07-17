using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;
using HugsLib;


namespace Control
{

    public class CultManager : ModBase
    {
        public static TickManager tm;
        public static Dictionary<int, PawnOpinionCache> OpinionCacheLookup = new Dictionary<int, PawnOpinionCache>();
        static List<Pawn> pawns = new List<Pawn>();
        public static Pawn Leader;
        public static HediffDef CommittedHediffdef
        {
            get
            {
                if (committedHediffdef == null)
                {
                    committedHediffdef = DefDatabase<HediffDef>.GetNamed("Committed");
                }
                return committedHediffdef;                
            }
        }
        static HediffDef committedHediffdef;
        
        public const int OpinionNormalizer = 100;
        
        public override string ModIdentifier
        {
            get { return "CultManager"; }
        }

        public static int GetOpinionOfLeader(Pawn pawnWithLeaderOpinion)
        {
            return OpinionCacheLookup[Leader.GetHashCode()].GetOpinionOfMe(pawnWithLeaderOpinion);
        }

        public override void Tick(int currentTick)
        {
            //if (pawns.Count == 0)
            //{
            //    tm = Find.TickManager;
            //    var cultLeaderTraitDef = DefDatabase<TraitDef>.GetNamed("CultLeader");
            //    pawns = Find.VisibleMap.mapPawns.FreeColonists.ToList();
            //    foreach (var pawn in pawns)
            //    {
            //        if (pawn.story.traits.HasTrait(cultLeaderTraitDef))
            //        {
            //            var hediff = pawn.health.hediffSet.GetFirstHediffOfDef(CommittedHediffdef);
            //            if (hediff == null)
            //            {
            //                hediff = HediffMaker.MakeHediff(CommittedHediffdef, pawn);
            //                pawn.health.AddHediff(hediff);
            //                hediff.Severity = 1;
                           
            //            }
            //            (hediff as Hediff_Committed).isLeader = true;
            //            Leader = pawn;
            //            var cache = new PawnOpinionCache(true, pawns,pawn);
            //            OpinionCacheLookup.Add(pawn.GetHashCode(), cache);
            //        }
            //        else
            //        {
            //            var cache = new PawnOpinionCache(false, pawns, pawn);
            //            OpinionCacheLookup.Add(pawn.GetHashCode(), cache);

            //        }
            //    }
            //    base.Tick(currentTick);
            //}

        }
    }
}