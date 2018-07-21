using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.AI;
using DubsBadHygiene;

namespace Control
{
    public class JobDriver_Sex : JobDriver
    {
        private const TargetIndex targetInd = TargetIndex.A;
        private const int TicksBetweenHeartMotes = 100;
        int duration = 500;
        bool anal;
        bool pissDrinking;
        float minLibido { get { return NeedUtil.SexTrigger; } }
        float maxBladder { get { return NeedUtil.BladderTrigger; } }
        private Pawn Receiver
        {
            get
            {
                return (Pawn)this.job.GetTarget(TargetIndex.A).Thing;
            }
        }

        public override bool TryMakePreToilReservations()
        {
            duration = 0;
            anal = false;
            pissDrinking = false;
            if (NeedUtil.GetSexNeed(pawn).CurLevelPercentage < minLibido+0.1f || NeedUtil.GetSexNeed(Receiver).CurInstantLevelPercentage < minLibido +0.1f)
            {
                duration += 500;
                anal = true;
            }
            if (NeedUtil.GetBladder(pawn).CurLevelPercentage < maxBladder + 0.1f)
            {
                duration += 100;
                pissDrinking = true;
            }

            return true;
        }

        [DebuggerHidden]
        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedNullOrForbidden(TargetIndex.A);
            this.FailOnDowned(TargetIndex.A);
            this.FailOnNotCasualInterruptible(TargetIndex.A);
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch);
            Toil prepare = Toils_General.WaitWith(TargetIndex.A, duration, false, false);
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
                if (anal)
                {
                    NeedUtil.GetSexNeed(pawn).CurLevelPercentage = 1;
                    NeedUtil.GetSexNeed(Receiver).CurLevelPercentage = 1;
                }
                if (pissDrinking)
                {
                    NeedUtil.GetBladder(pawn).CurLevelPercentage = 1;
                }
                pawn.needs.mood.thoughts.memories.TryGainMemory(DefDatabase<ThoughtDef>.GetNamed("HadSex"), Receiver);

                Receiver.needs.mood.thoughts.memories.TryGainMemory(DefDatabase<ThoughtDef>.GetNamed("HadSex"), pawn);
                pawn.jobs.EndCurrentJob(JobCondition.Succeeded, true);

            });
        }
    }
}