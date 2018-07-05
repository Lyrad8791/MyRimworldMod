//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using RimWorld;
//using Verse;

//namespace MyRimworldMod
//{
//    using RimWorld;
//    using Verse;
//    using Verse.AI;

//    public class JobDriver_Discipline : JobDriver
//    {
//        //Constants
//        private const TargetIndex VictimInd = TargetIndex.A;

//        public Pawn Victim
//        {
//            get
//            {
//                 return (Pawn)job.GetTarget(TargetIndex.A).Thing;
//            }
//        }

//        protected override IEnumerable<Toil> MakeNewToils()
//        {
//            Log.Message("In Toil, will discipline " + Victim.Name);
            
//            var originAutorithy = pawn.needs.TryGetNeed<Authority_Need>();
//            var victimAutorithy = Victim.needs.TryGetNeed<Authority_Need>();
//            if (originAutorithy == null || victimAutorithy == null)
//            {
//                Log.Error("Authority null");
//            }
//            float delta = originAutorithy.CurLevelPercentage - victimAutorithy.CurLevelPercentage;
//            int cnt = 60;

//            if (delta < 0.1f)
//            {
//                this.EndJobWith(JobCondition.Incompletable);
//            }
//            yield return Toils_Reserve.Reserve(TargetIndex.A, 1);
//            yield return Toils_Goto.GotoCell(TargetIndex.A, PathEndMode.OnCell);
//            yield return new Toil
//            {
//                initAction = delegate
//                {

//                },
//                tickAction = delegate
//                {
//                    var targetWillPower = Victim.needs.TryGetNeed<Willpower_Need>();
//                    if (targetWillPower.waitCounter == 0)
//                    {
//                        PawnUtility.ForceWait(Victim, 60);
//                        targetWillPower.waitCounter = 60;
//                    }

                    
//                    //targetWillPower.CurLevelPercentage += delta * 0.4f;

//                    if (targetWillPower.CurLevelPercentage > 0.9f)
//                    {
//                        Log.Message("I am done");
//                        this.EndJobWith(JobCondition.Succeeded);
//                    }
                                    


//                },
//                defaultCompleteMode = ToilCompleteMode.Delay,
//                defaultDuration = 1800
//            };
//        }

//        public override bool TryMakePreToilReservations()
//        {
//            Log.Message("Make reservations");
//            return pawn.Reserve(Victim, job, 1, -1, null);
//        }

    

//    }
//}
