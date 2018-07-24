//using RimWorld;
//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using Verse;

//namespace Control
//{
//    public class MainTabWindow_Sex : MainTabWindow_PawnTable
//    {
        
//        public MainTabWindow_Sex()
//        {
           
//        }

//        protected override PawnTableDef PawnTableDef
//        {
//            get
//            {
//                return DefDatabase<PawnTableDef>.GetNamed("CTRL_Sex");
//            }
//        }
//    }
//    public static List<string> infusionRecipeNames = new List<string> {}
//    public static List<RecipeDef> InfusionRecipes = new List<RecipeDef>()

//    public static List<RecipeDef> GetAvailableSeedRecipes()
//    {
//        foreach (RecipeDef current in thingForMedBills.def.AllRecipes)
//        {
//            if (current.AvailableNow)
//            {
//                IEnumerable<ThingDef> enumerable = current.PotentiallyMissingIngredients(null, thingForMedBills.Map);
//                if (!enumerable.Any((ThingDef x) => x.isBodyPartOrImplant))
//                {
//                    if (!enumerable.Any((ThingDef x) => x.IsDrug))
//                    {
//                        if (!enumerable.Any<ThingDef>() || !current.dontShowIfAnyIngredientMissing)
//                        {
//                            if (current.targetsBodyPart)
//                            {
//                                foreach (BodyPartRecord current2 in current.Worker.GetPartsToApplyOn(pawn, current))
//                                {
//                                    list.Add(HealthCardUtility.GenerateSurgeryOption(pawn, thingForMedBills, current, enumerable, current2));
//                                }
//                            }
//                            else
//                            {
//                                list.Add(HealthCardUtility.GenerateSurgeryOption(pawn, thingForMedBills, current, enumerable, null));
//                            }
//                        }
//                    }
//                }
//            }
//        }
//    }

//}