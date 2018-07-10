using RimWorld;
using System.Text;
using Verse;

namespace Control
{
    
    public class Hediff_Female : Hediff
    { }

    public class Hediff_Entitled : HediffWithComps
    {
        public static void AddInitialEntitlement(Pawn pawn)
        {
            if (pawn != null)
            {
                if (pawn.gender == Gender.Female && !pawn.NonHumanlikeOrWildMan())
                {
                    var entitledDef = DefDatabase<HediffDef>.GetNamed("Entitled");
                    if (!pawn.health.hediffSet.hediffs.Exists(x => x.def == entitledDef))
                    {
                        var entitledHedif = HediffMaker.MakeHediff(entitledDef, pawn);
                        entitledHedif.Severity = Rand.Range(0.3f, 1f);
                        pawn.health.AddHediff(entitledHedif, null, null);

                    }
                    var femaleDef = DefDatabase<HediffDef>.GetNamed("Female");
                    if (!pawn.health.hediffSet.hediffs.Exists(x => x.def == femaleDef))
                    {
                        var genitals = pawn.RaceProps.body.AllParts.Find((x) => x.def.defName == "Genitals");
                        var femaleHediff = HediffMaker.MakeHediff(femaleDef, pawn, genitals);
                        pawn.health.AddHediff(femaleHediff, genitals);
                        Log.Message("Hediff added");
                    }
                }
            }
        }
    }
    public class HediffComp_CapacitySeverityPerDay : HediffComp_SeverityPerDay
    {


        protected override float SeverityChangePerDay()
        {
            var prop = props as HediffCompProperties_SeverityPerDay;
            PawnCapacityDef capacityDef = prop.capacityDef;

            float capacity = Pawn.health.capacities.GetLevel(capacityDef) * prop.weight;
            return capacity;
        }
    }

    public class HediffCompProperties_SeverityPerDay : HediffCompProperties
    {
        public PawnCapacityDef capacityDef;
        public float weight;

        public HediffCompProperties_SeverityPerDay()
        {
            this.compClass = typeof(HediffComp_CapacitySeverityPerDay);
        }
    }
}