//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using RimWorld;
//using Verse;

//namespace MyRimworldMod
//{
//    public class Willpower_Need : Need
//    {
//        private float fallPerTick => def.fallPerDay / 60000f;
//        public int waitCounter = 60;

//        public float BeautyMod = 1f;
//        public float FoodMod = 1f;
//        public float ComfortMod = 1f;
//        public float MoodMod = 1f;
//        public float JoyMod = 1f;
//        public float RestMod = 1f;
//        public float AuthMod = 1f;
//        float needNorm { get { return 1f / (BeautyMod + FoodMod + ComfortMod + MoodMod + JoyMod + RestMod + AuthMod); } }

//        public float PressureFactor = 1f;
//        public float InternalFactor = 1f;
//        public float PassionFactor = 1f;
//        float globalNorm { get { return 1f / (PressureFactor+InternalFactor+PassionFactor); } }





//        Dictionary<WorkTypeDef, float> passionLookup;

//        public Willpower_Need(Pawn pawn)
//            : base(pawn)
//        {
//            threshPercents = new List<float>();
//            threshPercents.Add(0.3f);
//        }

//        public override void SetInitialLevel()
//        {
//            base.CurLevelPercentage = 1f;
//            if (pawn.gender == Gender.Female)
//            {
//                CurLevelPercentage = 0.1f;
//            }
//        }
//        // This class generates a list of Toils (a new one each time as per the 'yield' command)
//        // that are managed by the JobDriver in the Pawn_JobTracker.

//        float getNeedValues()
//        {
//            float needContribution = 0;
//            var needs = pawn.needs;
//            needContribution += needs.beauty.CurLevelPercentage * BeautyMod;
//            needContribution += needs.comfort.CurLevelPercentage * ComfortMod;
//            needContribution += needs.food.CurLevelPercentage * FoodMod;
//            needContribution += needs.joy.CurLevelPercentage * JoyMod;
//            needContribution += needs.mood.CurLevelPercentage * MoodMod;
//            needContribution += needs.rest.CurLevelPercentage * RestMod;
//            needContribution += needs.TryGetNeed<Authority_Need>().CurLevelPercentage * AuthMod;


//            return needContribution* needNorm;
//        }

//        float GetPassionForPotentialWork()
//        {
//            float passion = 0;
//            var workgivers = pawn.workSettings.WorkGiversInOrderNormal;
//            for (int i = 0; i < Math.Min(workgivers.Count,3); i++)
//            {
//                passion += passionLookup[workgivers[i].def.workType];
//            }
//            return passion / Math.Min(workgivers.Count, 3);
//        }

//        void SetupPassionsPerWork()
//        {
//            var workTypeDefs = DefDatabase<WorkTypeDef>.AllDefs;
//            foreach (var workTypeDef in workTypeDefs)
//            {
//                var relevantSkills = workTypeDef.relevantSkills;
//                float passion = 0;
//                foreach (var relevantSkill in relevantSkills)
//                {
//                    var skill = pawn.skills.GetSkill(relevantSkill);
//                    if (skill.passion == Passion.Major)
//                    {
//                        passion += 2;
//                    }
//                    else if (skill.passion == Passion.Minor)
//                    {
//                        passion += 1;
//                    }                    
//                }
//                if (relevantSkills.Count>0)
//                {
//                    passion /= relevantSkills.Count;
//                }
//                passionLookup.Add(workTypeDef, passion);
//            }
            
//        }


//        public override void NeedInterval()
//        {
//            waitCounter--;
//            CurLevelPercentage = 0;
//            CurLevelPercentage += getNeedValues() * InternalFactor;
//            CurLevelPercentage += GetPassionForPotentialWork() * PassionFactor;
//            CurLevelPercentage += pawn.needs.TryGetNeed<Pressure_Need>().CurLevelPercentage * PressureFactor;

//            CurLevelPercentage *= needNorm;
//            pawn.MentalState.a            

//        }
//    }
//}
