using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.AI;

namespace Control
{
    public static class DisciplineUtil
    {
        public static DisciplinaryAction GetDisciplinaryAction(Pawn master,Pawn receiver)
        {
            return new AnalSex(master,receiver);

        }
    }


    public class JobDriver_Discipline : JobDriver
    {
        private const TargetIndex targetInd = TargetIndex.A;
        private const int TicksBetweenHeartMotes = 100;
        DisciplinaryAction action;

        private Pawn Receiver
        {
            get
            {
                return (Pawn)this.job.GetTarget(TargetIndex.A).Thing;
            }
        }                               

        public override bool TryMakePreToilReservations()
        {
            action = DisciplineUtil.GetDisciplinaryAction(pawn,Receiver);
            return true;
        }
        
        [DebuggerHidden]
        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedNullOrForbidden(TargetIndex.A);
            this.FailOnDowned(TargetIndex.A);
            this.FailOnNotCasualInterruptible(TargetIndex.A);
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch);
            Toil prepare = Toils_General.WaitWith(TargetIndex.A, action.Duration, false, false);
            prepare.tickAction = delegate
            {
                if (pawn.IsHashIntervalTick(TicksBetweenHeartMotes))
				{
                    MoteMaker.ThrowMetaIcon(pawn.Position, pawn.Map, ThingDefOf.Mote_Heart);
                }
                if (Receiver.IsHashIntervalTick(TicksBetweenHeartMotes))
				{
                    MoteMaker.ThrowMetaIcon(Receiver.Position, pawn.Map, ThingDefOf.Mote_Heart);
                }
            };
            yield return prepare;
            yield return Toils_General.Do(delegate
            {
                action.ApplyEndEffects();
                pawn.jobs.EndCurrentJob(JobCondition.Succeeded, true);
 
            });
        }
    }
}