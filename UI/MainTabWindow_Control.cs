using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace Control
{
    public class MainTabWindow_Control : MainTabWindow_PawnTable
    {
        
        public MainTabWindow_Control()
        {
           
        }

        protected override PawnTableDef PawnTableDef
        {
            get
            {
                return DefDatabase<PawnTableDef>.GetNamed("CTRL_Control");
            }
        }


    }
}