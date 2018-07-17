using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Control
{
    public class Need_Discipline : Need
    {
        StatDef disciplineDecay;

        public Need_Discipline(Pawn pawn) : base(pawn)
        {
            this.threshPercents = new List<float>();
            this.threshPercents.Add(0.15f);
            this.threshPercents.Add(0.3f);
            this.threshPercents.Add(0.7f);
            this.threshPercents.Add(0.85f);
            disciplineDecay = DefDatabase<StatDef>.GetNamed("DisciplineDecay");
        }

        public static float GetVal(Pawn pawn)
        {
            return pawn.needs.TryGetNeed<Need_Discipline>().CurLevelPercentage;
        }

        public override void SetInitialLevel()
        {

        }
        public override void NeedInterval()
        {
            CurLevelPercentage -= pawn.GetStatValue(disciplineDecay);
        }
    }
}