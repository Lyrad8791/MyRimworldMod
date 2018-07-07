using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.AI;

namespace MyRimworldMod
{
    public interface ISexType
    {
        List<HediffDef> GetHediffDefs();
        int Duration { get; }
        float AverageDelta { get; }
        String GetReportString(Pawn target);
    }

    public class VaginalSex : ISexType
    {
        public int Duration => 100;

        public float AverageDelta => 0.2f;

        public List<HediffDef> GetHediffDefs()
        {
            return new List<HediffDef>();
        }

        public string GetReportString(Pawn target)
        {
            return "Fucking " + target.Name.ToStringShort + " in her vagina";
        }
    }
    public class AnalSex : ISexType
    {
        public int Duration => 100;

        public float AverageDelta => 0.4f;

        public List<HediffDef> GetHediffDefs()
        {
            return new List<HediffDef>();
        }
        public string GetReportString(Pawn target)
        {
            return "Fucking " + target.Name.ToStringShort + " in her ass";
        }

    }




    public class JobDriver_HaveSex : JobDriver
    {
        private const TargetIndex FemInd = TargetIndex.A;
        private const int TicksBetweenHeartMotes = 100;
        Need_Libido maleLibdo;
        Need_Libido femaleLibdo;

        ISexType sexType;
        private Pawn Female
        {
            get
            {
                return (Pawn)this.job.GetTarget(TargetIndex.A).Thing;
            }
        }

        ISexType GetSexType()
        {
            femaleLibdo = Female.needs.TryGetNeed<Need_Libido>();
            maleLibdo = pawn.needs.TryGetNeed<Need_Libido>();

            
            float delta = maleLibdo.SexualStatus - femaleLibdo.SexualStatus;
            List<ISexType> sexTypes = new List<ISexType> { new VaginalSex(),new AnalSex()};
            return sexTypes.MinBy((x) => delta - x.AverageDelta);


        }

        public override string GetReport()
        {
            return sexType.GetReportString(Female);
        }

        public override bool TryMakePreToilReservations()
        {
            sexType = GetSexType();
            return true;
        }

        [DebuggerHidden]
        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedNullOrForbidden(TargetIndex.A);
            this.FailOnDowned(TargetIndex.A);
            this.FailOnNotCasualInterruptible(TargetIndex.A);
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch);
            Toil prepare = Toils_General.WaitWith(TargetIndex.A, sexType.Duration, false, false);
            prepare.tickAction = delegate
            {
                if (pawn.IsHashIntervalTick(TicksBetweenHeartMotes))
				{
                    MoteMaker.ThrowMetaIcon(pawn.Position, pawn.Map, ThingDefOf.Mote_Heart);
                }
                if (Female.IsHashIntervalTick(TicksBetweenHeartMotes))
				{
                    MoteMaker.ThrowMetaIcon(Female.Position, pawn.Map, ThingDefOf.Mote_Heart);
                }
            };
            yield return prepare;
            yield return Toils_General.Do(delegate
            {
                maleLibdo.CurLevelPercentage = 0f;
                femaleLibdo.CurLevelPercentage *= 0.2f;
                pawn.jobs.EndCurrentJob(JobCondition.Succeeded, true);
                //                PawnUtility.Mated(pawn,Female);

            });
        }
    }
}