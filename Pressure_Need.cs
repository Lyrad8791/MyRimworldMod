//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using RimWorld;
//using Verse;

//namespace MyRimworldMod
//{
//    public class Pressure_Need : Need
//    {
//        float delta = 0.0001f;
       
//        public Pressure_Need(Pawn pawn)
//            : base(pawn)
//        {
//            threshPercents = new List<float>();
//            threshPercents.Add(0.3f);
//        }

//        public float GetRestingPoint()
//        {
//            // Depends on authority
//            return pawn.needs.TryGetNeed<Authority_Need>().CurLevelPercentage;
//        }

//        public override void SetInitialLevel()
//        {
//            CurLevelPercentage = GetRestingPoint();
//            if ()
//            {

//            }
//            pawn.RaceProps.body
//        }
        
//        public override void NeedInterval()
//        {
//            float restingPoint = GetRestingPoint();
//            if (restingPoint > CurLevelPercentage)
//            {
//                CurLevelPercentage += delta;
//            }
//            else
//            {
//                CurLevelPercentage -= delta;
//            }

//        }
//    }
//}
