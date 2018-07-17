using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace Control
{
    public class MainTabWindow_Sex : MainTabWindow_PawnTable
    {
        
        public MainTabWindow_Sex()
        {
           
        }

        protected override PawnTableDef PawnTableDef
        {
            get
            {
                return DefDatabase<PawnTableDef>.GetNamed("CTRL_Sex");
            }
        }
    }

}