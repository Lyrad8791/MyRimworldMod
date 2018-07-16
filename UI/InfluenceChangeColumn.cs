using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace Control
{
    public class InfluenceFromLeaderColumn : PawnColumnWorker
    {
        public override void DoCell(Rect rect, Pawn pawn, PawnTable table)
        {
            var val = pawn.GetStatValue(DefDatabase<StatDef>.GetNamed("InfluenceGainFromLeader"));

            Text.Anchor = TextAnchor.MiddleCenter;
            Widgets.Label(rect, val.ToString());
            Text.Anchor = TextAnchor.UpperLeft;
        }
    }
    public class InfluenceChangeFromFriendsColumn : PawnColumnWorker
    {
        public override void DoCell(Rect rect, Pawn pawn, PawnTable table)
        {
            var val = pawn.GetStatValue(DefDatabase<StatDef>.GetNamed("InfluenceGainFromFriends"));
            
            Text.Anchor = TextAnchor.MiddleCenter;
            Widgets.Label(rect, val.ToString());
            Text.Anchor = TextAnchor.UpperLeft;
        }
    }
    public class InfluenceChangeColumn : PawnColumnWorker
    {
        public override void DoCell(Rect rect, Pawn pawn, PawnTable table)
        {
            var hediff = Hediff_Committed.GetHediffForPawn(pawn);
            if (hediff!=null)
            {
                float delta = hediff.delta;
                Text.Anchor = TextAnchor.MiddleCenter;
                Widgets.Label(rect, delta.ToString());
                Text.Anchor = TextAnchor.UpperLeft;
            }           
        }
    }
    public class InfluenceLevelColumn : PawnColumnWorker
    {
        public override void DoCell(Rect rect, Pawn pawn, PawnTable table)
        {
            var hediffDef = CultManager.CommittedHediffdef;
            var hediff = Hediff_Committed.GetHediffForPawn(pawn);
            if (hediff!=null)
            {
                int id = hediff.CurStageIndex;
                float ratio = 1;
                if (id < hediffDef.stages.Count - 1)
                {
                    float min = hediff.CurStage.minSeverity;
                    float max = hediffDef.stages[id + 1].minSeverity;
                    float curSeverity = hediff.Severity;
                    ratio = Mathf.InverseLerp(min, max, curSeverity);
                }

                Widgets.FillableBar(rect, ratio);
                Text.Anchor = TextAnchor.MiddleCenter;
                Widgets.Label(rect, hediff.CurStage.label);
                Text.Anchor = TextAnchor.UpperLeft;


            }
        }
    }
} 