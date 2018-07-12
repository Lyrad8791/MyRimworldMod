using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace Control
{
    public class InfluenceChangeColumn : PawnColumnWorker
    {
        public override void DoCell(Rect rect, Pawn pawn, PawnTable table)
        {
            var leader = CultManager.Leader;
            float delta = StatusUtil.GetStatus(pawn) - StatusUtil.GetStatus(leader);

            Text.Anchor = TextAnchor.MiddleCenter;
            Widgets.Label(rect, delta.ToString());
            Text.Anchor = TextAnchor.UpperLeft;
        }
    }
    public class InfluenceLevelColumn : PawnColumnWorker
    {
        public override void DoCell(Rect rect, Pawn pawn, PawnTable table)
        {
            //var hediffDef = DefDatabase<HediffDef>.GetNamed("Committed");
            //var hediff = pawn.health.hediffSet.GetFirstHediffOfDef(hediffDef);
            //int id = hediff.CurStageIndex;
            //float ratio = 1;
            //if (id < hediffDef.stages.Count-1)
            //{
            //    float min = hediff.CurStage.minSeverity;
            //    float max = hediffDef.stages[id + 1].minSeverity;
            //    float curSeverity = hediff.Severity;
            //    ratio = Mathf.InverseLerp(min, max, curSeverity);
            //}

            //Widgets.FillableBar(rect,ratio);
            //Text.Anchor = TextAnchor.MiddleCenter;
            //Widgets.Label(rect, hediff.CurStage.label);
            //Text.Anchor = TextAnchor.UpperLeft;

        }
    }
} 