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


    public class StorageTest : ModBase
    {
        TickManager tm;
        bool showInitialGui = false;
        List<Pawn> pawns = new List<Pawn>();
        public override string ModIdentifier
        {
            get { return "StorageTest"; }
        }

        public override void MapGenerated(Map map)
        {
            tm = Find.TickManager;
            pawns = Find.VisibleMap.mapPawns.FreeColonists.ToList();
            showInitialGui = true;
            base.MapGenerated(map);
      
        }
   

        public override void OnGUI()
        {

            if (showInitialGui && pawns.Count > 0)
            {
                if (!tm.Paused)
                {
                    tm.TogglePaused();
                }
                DoTabGUI();
            }

        }
        public void DoTabGUI()
        {
            float width = UI.screenWidth;
            float height = UI.screenHeight;

            Rect rect = new Rect(width / 4f, height / 4f, width / 2, height / 2);
           
            Find.WindowStack.ImmediateWindow(235086, rect, WindowLayer.GameUI, delegate
            {
           
                if (Widgets.CloseButtonFor(rect.AtZero()))
                {
                    showInitialGui = false;
                    tm.TogglePaused();
                }
                GUI.BeginGroup(rect);
                Rect btn = new Rect(0, 0, 200, 100);
                Widgets.ButtonText(btn, "Test");
                GUI.EndGroup();

                GUILayout.BeginArea(rect);
                GUILayout.BeginVertical();
                for (int i = 0; i < pawns.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(pawns[i].Name.ToStringShort);
                    GUILayout.HorizontalSlider(0.5f, 0, 1f);
                    GUILayout.Toggle(false, "Is Leader");
                    GUILayout.EndHorizontal();

                }
                GUILayout.EndVertical();
                GUILayout.EndArea();

            }, true, false, 1f);
            


        }

    }
}