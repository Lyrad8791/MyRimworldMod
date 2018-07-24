using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;

namespace Control
{
    public class JobDriver_TakeToRestrain : JobDriver
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

        protected Building_DominationDevice device
        {
            get
            {
                return (Building_DominationDevice)this.job.GetTarget(TargetIndex.B).Thing;
            }
        }

        public override bool TryMakePreToilReservations()
        {
            return this.pawn.Reserve(this.Takee, this.job, 1, -1, null) && this.pawn.Reserve(this.device, this.job, 1, 1, null);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDestroyedOrNull(TargetIndex.A);
            this.FailOnDestroyedOrNull(TargetIndex.B);
            this.FailOnAggroMentalStateAndHostile(TargetIndex.A);
           
            base.AddFinishAction(delegate
                {
                    Log.Message("Finished");
                    Building_DominationDevice.targetsAway.Remove(device);

                    Takee.health.AddHediff(DefDatabase<HediffDef>.GetNamed("Restrained"));
                    DominationUtil.RestrainedPawns.Add(Takee);
                });
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.ClosestTouch).FailOnDespawnedNullOrForbidden(TargetIndex.A).FailOnDespawnedNullOrForbidden(TargetIndex.B).FailOn(() => this.job.def == JobDefOf.Arrest && !this.Takee.CanBeArrestedBy(this.pawn)).FailOn(() => !this.pawn.CanReach(this.device, PathEndMode.OnCell, Danger.Deadly, false, TraverseMode.ByPawn)).FailOn(() => this.job.def == JobDefOf.Rescue && !this.Takee.Downed).FailOnSomeonePhysicallyInteracting(TargetIndex.A);
            yield return new Toil
            {
                initAction = delegate
                {
                    pawn.jobs.curJob.count = 1;
                }
            };
            Toil startCarrying = Toils_Haul.StartCarryThing(TargetIndex.A, false, false, false);
            yield return startCarrying;
            yield return Toils_Goto.GotoThing(TargetIndex.B, PathEndMode.Touch);
            yield return new Toil
            {
                initAction = delegate
                    {
                        if (this.Takee.playerSettings == null)

                        {
                            this.Takee.playerSettings = new Pawn_PlayerSettings(this.Takee);
                        }
                    }
            };
            yield return Toils_Reserve.Release(TargetIndex.B);
            yield return new Toil
            {
                initAction = delegate
                {
                    IntVec3 position = this.device.Position;
                    Thing thing;

                    this.pawn.carryTracker.TryDropCarriedThing(position, ThingPlaceMode.Direct, out thing, null);

                },
                defaultCompleteMode = ToilCompleteMode.Instant
            };
        }

        
        
    }
}



