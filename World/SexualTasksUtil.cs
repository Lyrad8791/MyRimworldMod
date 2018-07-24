using HugsLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Control
{

    public interface IDominationBill
    {
        int Duration { get; }

        string Name { get; }

        string GetReport(Pawn female);
        bool IsSkilledEnough(Pawn dominator);
        bool IsProperTarget(Pawn submissive);
        void OnComplete(Pawn male, Pawn female);

    }

 

    public class DominationUtil : ModBase
    {
        public static List<IDominationBill> sexualBills = new List<IDominationBill> { new VaginalSex(),new AnalSex() };
        public override string ModIdentifier
        {
            get { return "SexualTasksUtil"; }
        }     
        public override void Tick(int currentTick)
        {
            if (currentTick%60 == 0)
            {
                //colonyPawns = Find.VisibleMap.mapPawns.FreeColonists.ToList();
                PawnFinder.Update();   
            }
        }
    }

    public class VaginalSex : IDominationBill
    {
        public int Duration => 200;
        
        public string Name => "Vaginal Sex";

        public string GetReport(Pawn female)
        {
            return "Having vaginal sex with " + female.Name.ToStringShort;
        }

        public bool IsProperTarget(Pawn submissive)
        {
            return true;
        }

        public bool IsSkilledEnough(Pawn dominator)
        {
            return dominator.gender == Gender.Male;
        }

        public void OnComplete(Pawn male, Pawn female)
        {
            
        }
    }

    public class AnalSex : IDominationBill
    {
        public int Duration => 200;

        public int intensity => 1;

        public string Name => "Anal Sex";

        public string GetReport(Pawn female)
        {
            return "Having anal sex with " + female.Name.ToStringShort;
        }

        public bool IsProperTarget(Pawn submissive)
        {
            return true;
        }

        public bool IsSkilledEnough(Pawn dominator)
        {
            return dominator.gender == Gender.Male;
        }

        public void OnComplete(Pawn male, Pawn female)
        {

        }
    }

}
