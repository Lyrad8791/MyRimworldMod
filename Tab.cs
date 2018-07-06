using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace MyRimworldMod
{
    public class ITab_Pawn_Religion : ITab
    {
        public override bool IsVisible
        {
            get
            {
                return true;
            }
        }

        private Pawn myPawn
        {
            get
            {
                if (base.SelPawn != null)
                {
                    return base.SelPawn;
                }
                Corpse corpse = base.SelThing as Corpse;
                if (corpse != null)
                {
                    return corpse.InnerPawn;
                }
                return null;
            }
        }


        public ITab_Pawn_Religion()
        {
            this.size = new Vector2(540f, 510f);
            this.labelKey = "TabReligion";
        }

        protected override void FillTab()
        {
            Rect outRect = new Rect(0f, 20f, this.size.x, this.size.y - 20f);
            CardUtility.DrawPawnCard(outRect, myPawn,  base.SelThing);

        }
    }
}