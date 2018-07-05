using System.Collections.Generic;
using Verse;

namespace MyRimworldMod
{
    public class PawnCapacityWorker_Rationalism : PawnCapacityWorker
    {
        public override float CalculateCapacityLevel(HediffSet diffSet, List<PawnCapacityUtility.CapacityImpactor> impactors = null)
        {
            return PawnCapacityUtility.CalculateTagEfficiency(diffSet, "RationalismSource", 3.40282347E+38f, impactors);
        }
    }

}
