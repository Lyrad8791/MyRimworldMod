using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;

namespace Control
{
    public class JobDriver_Free : JobDriver
    {
        private const TargetIndex TakeeIndex = TargetIndex.A;
        private const TargetIndex RestrainIndex = TargetIndex.B;

        protected Pawn Takee
        {
            get
            {
                return (Pawn)this.job.GetTarget(TargetIndex.A).Thing;
            }
        }

        public override bool TryMakePreToilReservations()
        {
            return this.pawn.Reserve(this.Takee, this.job, 1, -1, null);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDestroyedOrNull(TargetIndex.A);
            this.FailOnNotDowned(TargetIndex.A);
            AddFinishAction(delegate
                {
                    Log.Message("Finished");
                    Building_DominationDevice.pawnsToSave.Remove(Takee);
                    var hediffDef = DefDatabase<HediffDef>.GetNamed("Restrained");
                    Hediff hediff = Takee.health.hediffSet.hediffs.Find((Hediff x) => x.def == hediffDef);
                    if (hediff != null)
                    {
                        Takee.health.RemoveHediff(hediff);
                        DominationUtil.RestrainedPawns.Remove(Takee);
                    }
                });
           
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch);
            Toil prepare = Toils_General.WaitWith(TargetIndex.A, 200, true, false);

            yield return prepare;
            yield return Toils_General.Do(delegate
            {
                pawn.jobs.EndCurrentJob(JobCondition.Succeeded, true);

            });
        }



    }
}



