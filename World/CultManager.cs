using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;
using HugsLib;
using HugsLib.Utils;
using UnityEngine;

namespace Control
{

    public class CultManager : ModBase
    {
        TickManager tm;
        public static Dictionary<int, PawnOpinionCache> OpinionCacheLookup = new Dictionary<int, PawnOpinionCache>();
        static List<Pawn> pawns = new List<Pawn>();
        public static Pawn Leader;

        public const int OpinionNormalizer = 100;
        
        public override string ModIdentifier
        {
            get { return "StorageTest"; }
        }

        public override void MapGenerated(Map map)
        {
            base.MapGenerated(map);
            tm = Find.TickManager;
        }

        public static int GetOpinionOfLeader(Pawn pawnWithLeaderOpinion)
        {
            return OpinionCacheLookup[Leader.GetHashCode()].LookupOpinionOfMe(pawnWithLeaderOpinion);
        }

        public override void Tick(int currentTick)
        {
            if (pawns.Count == 0)
            {
                var hediffdef = DefDatabase<HediffDef>.GetNamed("Committed");
                var cultLeaderTraitDef = DefDatabase<TraitDef>.GetNamed("CultLeader");
                pawns = Find.VisibleMap.mapPawns.FreeColonists.ToList();
                foreach (var pawn in pawns)
                {
                    if (pawn.story.traits.HasTrait(cultLeaderTraitDef))
                    {
                        if (pawn.health.hediffSet.HasHediff(hediffdef))
                        {
                            var hediff = HediffMaker.MakeHediff(hediffdef, pawn);
                            pawn.health.AddHediff(hediff);
                            hediff.Severity = 1;
                        }
                        Leader = pawn;
                        var cache = new PawnOpinionCache(true, pawns,pawn);
                        OpinionCacheLookup.Add(pawn.GetHashCode(), cache);
                    }
                    else
                    {
                        var cache = new PawnOpinionCache(false, pawns, pawn);
                        OpinionCacheLookup.Add(pawn.GetHashCode(), cache);

                    }
                }
                base.Tick(currentTick);
            }

        }
    }
}