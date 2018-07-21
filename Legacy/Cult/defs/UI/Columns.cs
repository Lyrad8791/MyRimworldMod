using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Control
{

    public static class SexualCommandGiverUI
    {
        public static bool WindowOpen = false;
        public static List<Pawn> availablePawns;
        public static Pawn pawn;
        public static int hash;
        public static void UIWindow()
        {

            
            float width = UI.screenWidth;
            float height = UI.screenHeight;

            Rect rect = new Rect(width / 4f, height / 4f, width / 2, height / 2);
            if (Widgets.CloseButtonFor(rect.AtZero()))
            {
                WindowOpen = false;
            }
            var availableTasks = SexualTasksUtil.sexualBills;

            float pad = 0f;
            width = (rect.width - pad) / (availableTasks.Count + 1f);
            height = 50;

            Rect[] rects = new Rect[availableTasks.Count+1];

            for (int i = 0; i < availablePawns.Count; i++)
            {
                float ymin = (i + 1) * height + 10;
                for (int x = 0; x < rects.Length; x++)
                {
                    float xmin = width * x;
                    rects[x] = new Rect(xmin, ymin, width, height);
                }

                Widgets.Label(rects[0],availablePawns[i].Name.ToStringShort);
                for (int x = 0; x < availableTasks.Count; x++)
                {
                    if (Widgets.ButtonText(rects[x+1],availableTasks[x].Name))
                    {
                        var cmd = new SexualCommand(availablePawns[i], availableTasks[x], 0, 0);
                        SexualTasksUtil.AssignCommand(pawn, cmd);
                    }
                }
            }

            

        }

        public static void DrawUI()
        {
            float width = UI.screenWidth;
            float height = UI.screenHeight;

            Rect rect = new Rect(width / 4f, height / 4f, width / 2, height / 2);
            Find.WindowStack.ImmediateWindow(235086, rect, WindowLayer.GameUI, () => UIWindow());
        }

    }


    public class QueueLengthColumn : PawnColumnWorker
    {
        public override void DoCell(Rect rect, Pawn pawn, PawnTable table)
        {
            
            int queueLength = SexualTasksUtil.GetSexualCommands(pawn).Count;
            Text.Anchor = TextAnchor.MiddleCenter;
            Widgets.Label(rect, queueLength.ToString());
            Text.Anchor = TextAnchor.UpperLeft;
                       
        }
    }
    public class CommmandButtonColumn : PawnColumnWorker
    {
    

        public override void DoCell(Rect rect, Pawn pawn, PawnTable table)
        {
            if (pawn.gender == Gender.Male)
            {
                if (Widgets.ButtonText(rect, "Add Commands"))
                {
                    SexualCommandGiverUI.WindowOpen = true;
                    SexualCommandGiverUI.hash = pawn.GetHashCode();
                    SexualCommandGiverUI.pawn = pawn;
                    SexualCommandGiverUI.availablePawns = 
                        SexualTasksUtil.colonyPawns.Where(x => x.gender == Gender.Female && x.GetHashCode() != pawn.GetHashCode()).ToList();

                }
                if (SexualCommandGiverUI.WindowOpen && SexualCommandGiverUI.hash == pawn.GetHashCode())
                {
                    SexualCommandGiverUI.DrawUI();
                }
            }
        }
    }
} 