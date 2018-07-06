using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;
namespace MyRimworldMod
{
    [StaticConstructorOnStartup]
    public static class CardUtility2
    {

        private const float IconSize = 20f;
        private static List<ThingDef> tmpMedicineBestToWorst = new List<ThingDef>();

        public static void DrawPawnCard(Rect outRect, Pawn pawn, Thing thingForActionBills)
        {
            outRect = outRect.Rounded();
            GUILayout.BeginArea(outRect);

            GUILayout.EndArea();


 //           Widgets.CustomButtonText(ref outRect, "Yo", Color.white, Color.white, Color.white);
// Rect rect = new Rect(outRect.x, outRect.y, outRect.width * 0.375f, outRect.height).Rounded();
//            Draw(rect, pawn,thingForActionBills);
        }

        public static void Draw(Rect rect, Pawn pawn, Thing thingForBills)
        {

            GUI.color = Color.white;
            Widgets.DrawMenuSection(rect);
            rect = rect.ContractedBy(9f);
            GUI.BeginGroup(rect);
            float curY = 0f;
            Text.Font = GameFont.Medium;
            GUI.color = Color.white;
            Text.Anchor = TextAnchor.UpperCenter;
         
            PlayerKnowledgeDatabase.KnowledgeDemonstrated(ConceptDefOf.MedicalOperations, KnowledgeAmount.FrameDisplayed);
            
            Text.Font = GameFont.Small;
            GUI.color = Color.white;
            Text.Anchor = TextAnchor.UpperLeft;
            GUI.EndGroup();
        }          
        
    }
}
