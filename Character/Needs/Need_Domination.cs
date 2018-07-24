using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;

namespace Control
{

    public class Need_Domination : Need
    {

        public Need_Domination(Pawn pawn) : base(pawn)
        {
            this.threshPercents = new List<float>();
            this.threshPercents.Add(0.15f);
            this.threshPercents.Add(0.3f);
            this.threshPercents.Add(0.7f);
            this.threshPercents.Add(0.85f);

        }

        public override void SetInitialLevel()
        {
            this.CurLevelPercentage = 1f;
            if (pawn.gender == Gender.Female)
            {
                CurLevelPercentage = 0.2f;
            }
        }

        public override void NeedInterval()
        {
            if (pawn.gender == Gender.Female)
            {
                CurLevelPercentage -= 1E-4f;
            }
        }
    }
}