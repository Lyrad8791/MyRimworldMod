using HugsLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Control
{

    public interface ISexualBill
    {
        int Duration { get; }
        int intensity { get; }
        string Name { get; }

        string GetReport(Pawn female);
        void OnComplete(Pawn male, Pawn female);
    }

    public class SexualCommand
    {
        public Pawn target;
        public ISexualBill sexualBill;
        public float minLibido;
        public int minOpinion;

        public SexualCommand(Pawn target, ISexualBill sexualBill, float minLibido, int minOpinion)
        {
            this.target = target;
            this.sexualBill = sexualBill;
            this.minLibido = minLibido;
            this.minOpinion = minOpinion;
        }
    }

    public class SexualTasksUtil : ModBase
    {
        public static List<ISexualBill> sexualBills = new List<ISexualBill> { new VaginalSex(),new AnalSex() };
        public static Dictionary<int, List<SexualCommand>> billsLookup = new Dictionary<int, List<SexualCommand>>();
        public static List<Pawn> colonyPawns = new List<Pawn>();

        public override string ModIdentifier
        {
            get { return "SexualTasksUtil"; }
        }

        public static List<SexualCommand> GetSexualCommands(Pawn pawn)
        {
            if (pawn.gender == Gender.Female)
            {
                return new List<SexualCommand>();
            }
            else
            {
                int hash = pawn.GetHashCode();
                if (!billsLookup.ContainsKey(hash))
                {
                    billsLookup.Add(hash, new List<SexualCommand>());
                }
                return billsLookup[hash];
            }
        }
        public static void AssignCommand(Pawn pawn, SexualCommand cmd)
        {
            billsLookup[pawn.GetHashCode()].Add(cmd);
        }

        public override void Tick(int currentTick)
        {
            if (currentTick%60 == 0)
            {
                colonyPawns = Find.VisibleMap.mapPawns.FreeColonists.ToList();
                PawnFinder.Update();   
            }

        }
    }

    public class VaginalSex : ISexualBill
    {
        public int Duration => 200;

        public int intensity => 1;

        public string Name => "Vaginal Sex";

        public string GetReport(Pawn female)
        {
            return "Having vaginal sex with " + female.Name.ToStringShort;
        }

        public void OnComplete(Pawn male, Pawn female)
        {

        }
    }

    public class AnalSex : ISexualBill
    {
        public int Duration => 200;

        public int intensity => 1;

        public string Name => "Anal Sex";

        public string GetReport(Pawn female)
        {
            return "Having vaginal sex with " + female.Name.ToStringShort;
        }

        public void OnComplete(Pawn male, Pawn female)
        {

        }
    }

}
