using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Control
{

  

    public class ResetSexNeedColumn : PawnColumnWorker
    {
         
        public override void DoCell(Rect rect, Pawn pawn, PawnTable table)
        {
            if (Widgets.ButtonText(rect,"Reset Libido"))
            {
                NeedUtil.GetSexNeed(pawn).CurLevelPercentage = 0f;
            }          
        }
    }
    public class ResetNeedBladder : PawnColumnWorker
    {
        public override void DoCell(Rect rect, Pawn pawn, PawnTable table)
        {
            if (Widgets.ButtonText(rect, "Reset Bladder"))
            {
                NeedUtil.GetBladder(pawn).CurLevelPercentage = 0.28f;
            }
        }
    }



} 