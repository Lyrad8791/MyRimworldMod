//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using UnityEngine;
//using Verse;
//using RimWorld;
//using HugsLib;
//using HugsLib.Utils;
//using UnityEngine;

//namespace Control
//{

//    public class CultManager : ModBase
//    {
//        TickManager tm;
//        bool showInitialGui = false;
//        List<Pawn> pawns = new List<Pawn>();
//        public static Dictionary<int, int> opinionCache = new Dictionary<int, int>();

//        public override string ModIdentifier
//        {
//            get { return "StorageTest"; }
//        }

//        public override void MapGenerated(Map map)
//        {

//            base.MapGenerated(map);
//            tm = Find.TickManager;
//            showInitialGui = true;

//        }
//        public override void Tick(int currentTick)
//        {
//            if (showInitialGui && pawns.Count == 0)
//            {
//                var hediffdef = DefDatabase<HediffDef>.GetNamed("Control");
//                pawns = Find.VisibleMap.mapPawns.FreeColonists.ToList();
//                foreach (var pawn in pawns)
//                {
//                    var hediff = HediffMaker.MakeHediff(hediffdef, pawn);
//                    pawn.health.AddHediff(hediff);
//                    var ctrl_Hediff = hediff as Hediff_Control;
//                    if (ctrl_Hediff == null)
//                    {
//                        Log.Message("Cant cast");
//                    }
//                    hediff_Controls.Add(ctrl_Hediff);
//                }
//            }
//            base.Tick(currentTick);
//        }

//        public override void OnGUI()
//        {

//            if (showInitialGui && pawns.Count > 0)
//            {
//                if (!tm.Paused)
//                {
//                    tm.TogglePaused();
//                }
//                DoTabGUI();
//            }
//        }

//        public void DoTabGUI()
//        {
//            float width = UI.screenWidth;
//            float height = UI.screenHeight;

//            Rect rect = new Rect(width / 4f, height / 4f, width / 2, height / 2);

//            Find.WindowStack.ImmediateWindow(235086, rect, WindowLayer.GameUI, delegate
//            {

//                if (Widgets.CloseButtonFor(rect.AtZero()))
//                {
//                    showInitialGui = false;
//                    tm.TogglePaused();
//                }
//                //GUI.BeginGroup(rect);
//                //Rect btn = new Rect(0, 0, 200, 100);
//                //Widgets.ButtonText(btn, "Test");
//                //GUI.EndGroup();
//                try
//                {
//                    float pad = 0f;
//                    width = (rect.width - pad) / 3f;
//                    height = 50;
//                    Rect[] rects = new Rect[3];

//                    for (int i = 0; i < pawns.Count; i++)
//                    {
//                        float ymin = (i + 1) * height + 10;
//                        for (int x = 0; x < rects.Length; x++)
//                        {
//                            float xmin = width * x;
//                            rects[x] = new Rect(xmin, ymin, width, height);
//                            Log.Message(rects[x].ToString());
//                        }
//                        Widgets.Label(rects[0],pawns[i].Name.ToStringShort);
//                        hediff_Controls[i].Severity = Widgets.HorizontalSlider(rects[1], hediff_Controls[i].Severity, 0f, 1f);
//                        Widgets.CheckboxLabeled(rects[2], "Is Leader", ref hediff_Controls[i].isLeader);
                        
//                    }
//                }
//                catch (Exception e)
//                {
//                    Log.Message(e.Message);
//                }




//            }, true, false, 1f);
//        }

//    }
//}