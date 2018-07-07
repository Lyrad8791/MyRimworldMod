using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;

namespace MyRimworldMod
{

    public class Need_Libido : Need
    {
        public float SexualStatus;
        public void UpdateSatus()
        {
            SexualStatus = 0;
            SexualStatus += pawn.health.capacities.GetLevel(DefDatabase<PawnCapacityDef>.GetNamed("Authority"));
            SexualStatus += 1 - CurLevelPercentage;
            SexualStatus /= 2f;
            
        }

        public Libido_def Def
        {
            get
            {
                return this.def as Libido_def;
            }
        }


        public Need_Libido(Pawn pawn) : base(pawn)
        {
            this.threshPercents = new List<float>();
            this.threshPercents.Add(0.15f);
            this.threshPercents.Add(0.3f);
            this.threshPercents.Add(0.7f);
            this.threshPercents.Add(0.85f);
        }

        public override void SetInitialLevel()
        {
            this.CurLevelPercentage = 0.5f;
        }
        public override void NeedInterval()
        {
            if (pawn.gender == Gender.Female)
            {
                this.CurLevelPercentage += 1.0E-04f;
            }
            if (pawn.gender == Gender.Male)
            {
                this.CurLevelPercentage += 3.0E-03f;
            }
            UpdateSatus();


        }
    }
}