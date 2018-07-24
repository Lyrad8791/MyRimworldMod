using RimWorld;
using Verse;
using Verse.AI;

namespace Control
{
    public class WorkGiver_Free : WorkGiver
    {
        JobDef def => DefDatabase<JobDef>.GetNamed("Free");
        public override Job NonScanJob(Pawn pawn)
        {
            if (Building_DominationDevice.pawnsToSave.Count == 0)
            {
                return null;
            }
            else
            {
                var pawnToFree = Building_DominationDevice.pawnsToSave[0];
                return new Job(def, pawnToFree);
            }
        }
    }

}
