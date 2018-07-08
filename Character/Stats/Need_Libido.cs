﻿using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;

namespace MyRimworldMod
{

    public class Statpart_Gender : StatPart
    {
        public override string ExplanationPart(StatRequest req)
        {
            return "Returns 1 for male, 0 for female";
        }

        public override void TransformValue(StatRequest req, ref float val)
        {
            Pawn pawn = req.Thing as Pawn;
            val = 0;
            if (pawn.gender == Gender.Male)
            {
                val = 1;
            }
        }
    }
}