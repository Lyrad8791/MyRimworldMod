using System;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using Verse.AI;

namespace Control
{
    public class WorkGiver_Restrain : WorkGiver
    {
        JobDef def => DefDatabase<JobDef>.GetNamed("Restrain");
        public override Job NonScanJob(Pawn pawn)
        {
            if (Building_DominationDevice.targetsAway.Count == 0)
            {
                return null;
            }
            else
            {
                var building = Building_DominationDevice.targetsAway[0];
                return new Job(def, building.owners[0], building);
            }          
        }        
    }

}
