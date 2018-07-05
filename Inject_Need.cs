﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace MyRimworldMod
{

    public class Inject_Need : Need
    {
        

        public Inject_Need(Pawn pawn)
            : base(pawn)
        {
            threshPercents = new List<float>();
            threshPercents.Add(0.3f);
             
        }

        public override void NeedInterval()
        {
            
        }

        public override void SetInitialLevel()
        {
            if (pawn.gender == Gender.Female && !pawn.NonHumanlikeOrWildMan())
            {
                HediffDef def = DefDatabase<HediffDef>.GetNamed("Feminism");
                var hediff =HediffMaker.MakeHediff(def, pawn, null);
                hediff.Severity = 0.4f;
                pawn.health.AddHediff(hediff, null, null);
                pawn.RaceProps.

            }



        }

    }

}
