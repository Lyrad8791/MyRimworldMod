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

            Text.Anchor = TextAnchor.MiddleCenter;
            Widgets.Label(rect, (0.1f).ToString());
            Text.Anchor = TextAnchor.UpperLeft;
        }
    }
    public class InfluenceLevelColumn : PawnColumnWorker
    {
        public override void DoCell(Rect rect, Pawn pawn, PawnTable table)
        {
            Widgets.FillableBar(rect, 0.7f);
            Text.Anchor = TextAnchor.MiddleCenter;
            Widgets.Label(rect, " Level " + 0);
            Text.Anchor = TextAnchor.UpperLeft;

        }
    }
}