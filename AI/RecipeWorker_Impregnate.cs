﻿using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace Control
{
    public class RecipeWorker_Restrain : RecipeWorker
    {
        public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients, Bill bill)
        {

            pawn.health.AddHediff(this.recipe.addsHediff, part, null);
        }
        
        public override IEnumerable<BodyPartRecord> GetPartsToApplyOn(Pawn pawn, RecipeDef recipe)
        {
            if (pawn.gender == Gender.Female)
            {
                for (int i = 0; i < recipe.appliedOnFixedBodyParts.Count; i++)
                {
                    BodyPartDef part = recipe.appliedOnFixedBodyParts[i];
                    List<BodyPartRecord> bpList = pawn.RaceProps.body.AllParts;
                    for (int j = 0; j < bpList.Count; j++)
                    {
                        BodyPartRecord record = bpList[j];
                        if (record.def == part)
                        {
                            if (pawn.health.hediffSet.GetNotMissingParts(BodyPartHeight.Undefined, BodyPartDepth.Undefined).Contains(record))
                            {
                                if (!pawn.health.hediffSet.PartOrAnyAncestorHasDirectlyAddedParts(record))
                                {
                                    if (!pawn.health.hediffSet.hediffs.Any((Hediff x) => x.Part == record && x.def == recipe.addsHediff))
                                    {
                                        yield return record;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //yield return null;
        }

        public override bool IsViolationOnPawn(Pawn pawn, BodyPartRecord part, Faction billDoerFaction)
        {
            if (pawn.gender == Gender.Male)
            {
                return true;
            }
            return base.IsViolationOnPawn(pawn, part, billDoerFaction);
        }
       
        
    }


}
