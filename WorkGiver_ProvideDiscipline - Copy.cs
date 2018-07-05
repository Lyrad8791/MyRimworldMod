//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using RimWorld;
//using Verse;

//namespace MyRimworldMod
//{
//    // RimWorld.JobGiver_Berserk
//    using RimWorld;
//    using Verse;
//    using Verse.AI;

//    public class WorkGiver_ProvideDiscipline : WorkGiver_Scanner
//    {

//        public IEnumerable<Thing> Pawns(Pawn pawn)
//        {
//            return pawn.Map.mapPawns.SpawnedPawnsInFaction(pawn.Faction).Cast<Thing>().AsEnumerable();
//        }

//        public override IEnumerable<Thing> PotentialWorkThingsGlobal(Pawn pawn)
//        {
//            return Pawns(pawn);
//        }
        

//        public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
//        {
//            //Check thing is pawn.
//            Pawn victim = t as Pawn;
//            if (pawn.gender == Gender.Female)
//            {
//                Log.Message("Im Female");
//                return false;
//            }
//            if (victim == null)
//                return false;
//            if (victim.Downed || !victim.RaceProps.Humanlike || !pawn.RaceProps.Humanlike || !ReservationUtility.CanReserve(pawn, victim))
//            {
//                Log.Message("Victim not a good target");
//                return false;
//            }
//            Log.Message("Found Job");
//            return true; 
//        }

//        public override bool ShouldSkip(Pawn pawn)
//        {
//            //DebugLogMessage(pawn);
//            return false;
//        }

//        public override Job JobOnThing(Pawn pawn, Thing t,bool forced = false)
//        {
//            Pawn victim = t as Pawn;
//            if (victim == null)
//            {
//                Log.Error(pawn.LabelCap + " report: mate is not pawn.");
//                return null;
//            }
//            Log.Message("Job On thing");
//            //Job jobNew = new Job(DefDatabase<JobDef>.GetNamed("Driver_Discipline"), victim);
//            return null;      
//        }        
//    }
//}
