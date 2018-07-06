using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;
namespace MyRimworldMod
{
    public static class SexualBillHolder
    {
        static void SaveSomething()
        {
            
        }
    }
         

    public class Bill_Sexual : Bill
    {
        private BodyPartRecord part;

        private int partIndex = -1;

        public ThingDef consumedInitialMedicineDef;

        public override bool CheckIngredientsIfSociallyProper
        {
            get
            {
                return false;
            }
        }

        public override bool CompletableEver
        {
            get
            {
                return !this.recipe.targetsBodyPart || this.recipe.Worker.GetPartsToApplyOn(this.GiverPawn, this.recipe).Contains(this.part);
            }
        }

        public BodyPartRecord Part
        {
            get
            {
                if (this.part == null && this.partIndex >= 0)
                {
                    this.part = this.GiverPawn.RaceProps.body.GetPartAtIndex(this.partIndex);
                }
                return this.part;
            }
            set
            {
                if (this.billStack == null)
                {
                    Log.Error("Can only set Bill_Sexual.Part after the bill has been added to a pawn's bill stack.");
                    return;
                }
                if (value != null)
                {
                    this.partIndex = this.GiverPawn.RaceProps.body.GetIndexOfPart(value);
                }
                else
                {
                    this.partIndex = -1;
                }
                this.part = value;
            }
        }

        private Pawn GiverPawn
        {
            get
            {
                var bg = this.billStack.billGiver as MyBillGiver;
                var pawn = bg.pawn;
                Corpse corpse = this.billStack.billGiver as Corpse;
                if (corpse != null)
                {
                    pawn = corpse.InnerPawn;
                }
                if (pawn == null)
                {
                    throw new InvalidOperationException("Sexual bill on non-pawn.");
                }
                return pawn;
            }
        }

        public override string Label
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(this.recipe.Worker.GetLabelWhenUsedOn(this.GiverPawn, this.part));
                if (this.Part != null && !this.recipe.hideBodyPartNames)
                {
                    stringBuilder.Append(" (" + this.Part.def.label + ")");
                }
                return stringBuilder.ToString();
            }
        }

        public Bill_Sexual()
        {
        }

        public Bill_Sexual(RecipeDef recipe) : base(recipe)
        {
        }

        public override bool ShouldDoNow()
        {
            return !this.suspended;
        }

        public override void Notify_IterationCompleted(Pawn billDoer, List<Thing> ingredients)
        {
            base.Notify_IterationCompleted(billDoer, ingredients);
            if (this.CompletableEver)
            {
                Pawn giverPawn = this.GiverPawn;
                this.recipe.Worker.ApplyOnPawn(giverPawn, this.Part, billDoer, ingredients, this);         
            }
            this.billStack.Delete(this);
        }

        public override void Notify_DoBillStarted(Pawn billDoer)
        {
            base.Notify_DoBillStarted(billDoer);
            this.consumedInitialMedicineDef = null;
            if (!this.GiverPawn.Dead)
            {
                List<ThingStackPartClass> placedThings = billDoer.CurJob.placedThings;
                for (int i = 0; i < placedThings.Count; i++)
                {
                    if (placedThings[i].thing is Medicine)
                    {
                        this.recipe.Worker.ConsumeIngredient(placedThings[i].thing.SplitOff(1), this.recipe, billDoer.MapHeld);
                        placedThings[i].Count--;
                        this.consumedInitialMedicineDef = placedThings[i].thing.def;
                        if (placedThings[i].thing.Destroyed || placedThings[i].Count <= 0)
                        {
                            placedThings.RemoveAt(i);
                        }
                        break;
                    }
                }
            }
        }

        
    }
}
