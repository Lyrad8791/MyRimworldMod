using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace Control
{
    public class Hediff_SexualSatisfaction : HediffWithComps
    {

        public static void AddInitialEntitlement(Pawn pawn)
        {

            if (pawn.gender == Gender.Female && !pawn.NonHumanlikeOrWildMan())
            {
                var satisfactionDef = DefDatabase<HediffDef>.GetNamed("SexualSatisfaction");
                if (!pawn.health.hediffSet.hediffs.Exists(x => x.def == satisfactionDef))
                {
                    var entitledHedif = HediffMaker.MakeHediff(satisfactionDef, pawn);
                    pawn.health.AddHediff(entitledHedif, null, null);

                }
            }

        }

        public override void Tick()
        {
            var sexNeed = NeedUtil.GetSexNeed(pawn).CurLevelPercentage;
            if (sexNeed < 0.3f)
            {
                Severity = 1f;
            }
            else if (sexNeed < 0.7f)
            {
                Severity = 0.5f;
            }
            else
            {
                Severity = 0;
            }

        }


    }


}
