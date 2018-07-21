using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;

namespace Control
{

    public class Need_Sex : Need
    {
        bool added = false;
        public Need_Sex(Pawn pawn) : base(pawn)
        {
            this.threshPercents = new List<float>();
            this.threshPercents.Add(0.15f);
            this.threshPercents.Add(0.3f);
            this.threshPercents.Add(0.7f);
            this.threshPercents.Add(0.85f);

        }

        public override void SetInitialLevel()
        {
            this.CurLevelPercentage = 0.7f;
        }

        public override void NeedInterval()
        {
           

            this.CurLevelPercentage -= 1.0E-04f;
            if (!added)
            {
                Hediff_SexualSatisfaction.AddInitialEntitlement(pawn);
                added = true;
            }
        }
    }
}