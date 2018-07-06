using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;
namespace MyRimworldMod
{
    [StaticConstructorOnStartup]
    public static class CardUtility
    {
        private static Vector2 scrollPosition = Vector2.zero;

        private static float scrollViewHeight = 0f;

        private static bool highlight = true;
        
        private static Vector2 billsScrollPosition = Vector2.zero;

        private static float billsScrollHeight = 1000f;

        private static bool showAllHediffs = false;

        private static bool showHediffsDebugInfo = false;

        public const float TopPadding = 20f;

        private const float ThoughtLevelHeight = 25f;

        private const float ThoughtLevelSpacing = 4f;

        private const float IconSize = 20f;

        private static readonly Color HighlightColor = new Color(0.5f, 0.5f, 0.5f, 1f);

        private static readonly Color StaticHighlightColor = new Color(0.75f, 0.75f, 0.85f, 1f);

        private static readonly Color VeryPoorColor = new Color(0.4f, 0.4f, 0.4f);

        private static readonly Color PoorColor = new Color(0.55f, 0.55f, 0.55f);

        private static readonly Color WeakenedColor = new Color(0.7f, 0.7f, 0.7f);

        private static readonly Color EnhancedColor = new Color(0.5f, 0.5f, 0.9f);

        private static readonly Color MediumPainColor = new Color(0.9f, 0.9f, 0f);

        private static readonly Color SeverePainColor = new Color(0.9f, 0.5f, 0f);

        private static readonly Texture2D BleedingIcon = ContentFinder<Texture2D>.Get("UI/Icons/Medical/Bleeding", true);

        private static List<ThingDef> tmpMedicineBestToWorst = new List<ThingDef>();

        public static void DrawPawnCard(Rect outRect, Pawn pawn, Thing thingForActionBills)
        {
            
            outRect = outRect.Rounded();
            Rect rect = new Rect(outRect.x, outRect.y, outRect.width * 0.375f, outRect.height).Rounded();
            Rect rect2 = new Rect(rect.xMax, outRect.y, outRect.width - rect.width, outRect.height);
            rect.yMin += 11f;
            CardUtility.Draw(rect, pawn,thingForActionBills);
        }

        public static void Draw(Rect rect, Pawn pawn, Thing thingForBills)
        {

            GUI.color = Color.white;
            Widgets.DrawMenuSection(rect);

            string label = (!pawn.RaceProps.IsMechanoid) ? "MedicalOperationsShort".Translate(new object[]
            {
                pawn.BillStack.Count
            }) : "MedicalOperationsMechanoidsShort".Translate(new object[]
            {
                pawn.BillStack.Count
            });
                      
            rect = rect.ContractedBy(9f);
            GUI.BeginGroup(rect);
            float curY = 0f;
            Text.Font = GameFont.Medium;
            GUI.color = Color.white;
            Text.Anchor = TextAnchor.UpperCenter;
         
            PlayerKnowledgeDatabase.KnowledgeDemonstrated(ConceptDefOf.MedicalOperations, KnowledgeAmount.FrameDisplayed);
            curY = CardUtility.DrawSexActionTab(rect, pawn, thingForBills, curY);
            
            Text.Font = GameFont.Small;
            GUI.color = Color.white;
            Text.Anchor = TextAnchor.UpperLeft;
            GUI.EndGroup();
        }          
        private static float DrawSexActionTab(Rect leftRect, Pawn pawn, Thing thingForMedBills, float curY)
        {
            curY += 2f;
            Func<List<FloatMenuOption>> recipeOptionsMaker = delegate
            {
                List<FloatMenuOption> list = new List<FloatMenuOption>();
                foreach (RecipeDef current in thingForMedBills.def.AllRecipes)
                {
                    if (current.AvailableNow && current.appliedOnFixedBodyParts.Contains(DefDatabase<BodyPartDef>.GetNamed("Anus")) && pawn.gender == Gender.Female)
                    {
                        IEnumerable<ThingDef> enumerable = current.PotentiallyMissingIngredients(null, thingForMedBills.Map);
                        if (!enumerable.Any((ThingDef x) => x.isBodyPartOrImplant))
                        {
                            if (!enumerable.Any((ThingDef x) => x.IsDrug))
                            {
                                if (!enumerable.Any<ThingDef>() || !current.dontShowIfAnyIngredientMissing)
                                {
                                    if (current.targetsBodyPart)
                                    {
                                        foreach (BodyPartRecord current2 in current.Worker.GetPartsToApplyOn(pawn, current))
                                        {
                                            list.Add(CardUtility.GenerateOption(pawn, thingForMedBills, current, enumerable, current2));
                                        }
                                    }
                                    else
                                    {
                                        list.Add(CardUtility.GenerateOption(pawn, thingForMedBills, current, enumerable, null));
                                    }
                                }
                            }
                        }
                    }
                }
                return list;
            };
            Rect rect = new Rect(leftRect.x - 9f, curY, leftRect.width, leftRect.height - curY - 20f);
            ((IBillGiver)thingForMedBills).BillStack.DoListing(rect, recipeOptionsMaker, ref CardUtility.billsScrollPosition, ref CardUtility.billsScrollHeight);
            return curY;
        }

        private static FloatMenuOption GenerateOption(Pawn pawn, Thing thingForMedBills, RecipeDef recipe, IEnumerable<ThingDef> missingIngredients, BodyPartRecord part = null)
        {
            string text = recipe.Worker.GetLabelWhenUsedOn(pawn, part);
            if (part != null && !recipe.hideBodyPartNames)
            {
                text = text + " (" + part.def.label + ")";
            }
            FloatMenuOption floatMenuOption;
            if (missingIngredients.Any<ThingDef>())
            {
                text += " (";
                bool flag = true;
                foreach (ThingDef current in missingIngredients)
                {
                    if (!flag)
                    {
                        text += ", ";
                    }
                    flag = false;
                    text += "MissingMedicalBillIngredient".Translate(new object[]
                    {
                        current.label
                    });
                }
                text += ")";
                floatMenuOption = new FloatMenuOption(text, null, MenuOptionPriority.Default, null, null, 0f, null, null);
            }
            else
            {
                Action action = delegate
                {
                    Pawn pawn2 = thingForMedBills as Pawn;
                    if (pawn2 == null)
                    {
                        return;
                    }
                    Bill_Sexual bill = new Bill_Sexual(recipe);
                    pawn2.BillStack.AddBill(bill);
                    bill.Part = part;
                    if (recipe.conceptLearned != null)
                    {
                        PlayerKnowledgeDatabase.KnowledgeDemonstrated(recipe.conceptLearned, KnowledgeAmount.Total);
                    }
                    Map map = thingForMedBills.Map;
                    if (!map.mapPawns.FreeColonists.Any((Pawn col) => recipe.PawnSatisfiesSkillRequirements(col)))
                    {
                        Bill.CreateNoPawnsWithSkillDialog(recipe);
                    }

                    WarnCircumstances(recipe, part, pawn2);
                };
                floatMenuOption = new FloatMenuOption(text, action, MenuOptionPriority.Default, null, null, 0f, null, null);
            }
            floatMenuOption.extraPartWidth = 29f;
            floatMenuOption.extraPartOnGUI = ((Rect rect) => Widgets.InfoCardButton(rect.x + 5f, rect.y + (rect.height - 24f) / 2f, recipe));
            return floatMenuOption;
        }

        private static void WarnCircumstances(RecipeDef recipe, BodyPartRecord part, Pawn pawn2)
        {
            // Do Check for location
            if (pawn2.Faction != null && !pawn2.Faction.def.hidden && !pawn2.Faction.HostileTo(Faction.OfPlayer) && recipe.Worker.IsViolationOnPawn(pawn2, part, Faction.OfPlayer))
            {
                Messages.Message("SexWillAngerFaction".Translate(new object[]
                {
                            pawn2.Faction
                }), pawn2, MessageTypeDefOf.CautionInput);
            }
            ThingDef minRequiredMedicine = CardUtility.GetMinRequiredMedicine(recipe);
            if (minRequiredMedicine != null && pawn2.playerSettings != null && !pawn2.playerSettings.medCare.AllowsMedicine(minRequiredMedicine))
            {
                Messages.Message("MessageTooLowMedCare".Translate(new object[]
                {
                            minRequiredMedicine.label,
                            pawn2.LabelShort,
                            pawn2.playerSettings.medCare.GetLabel()
                }), pawn2, MessageTypeDefOf.CautionInput);
            }
        }

        private static ThingDef GetMinRequiredMedicine(RecipeDef recipe)
        {
            CardUtility.tmpMedicineBestToWorst.Clear();
            List<ThingDef> allDefsListForReading = DefDatabase<ThingDef>.AllDefsListForReading;
            for (int i = 0; i < allDefsListForReading.Count; i++)
            {
                if (allDefsListForReading[i].IsMedicine)
                {
                    CardUtility.tmpMedicineBestToWorst.Add(allDefsListForReading[i]);
                }
            }
            CardUtility.tmpMedicineBestToWorst.SortByDescending((ThingDef x) => x.GetStatValueAbstract(StatDefOf.MedicalPotency, null));
            ThingDef thingDef = null;
            for (int j = 0; j < recipe.ingredients.Count; j++)
            {
                ThingDef thingDef2 = null;
                for (int k = 0; k < CardUtility.tmpMedicineBestToWorst.Count; k++)
                {
                    if (recipe.ingredients[j].filter.Allows(CardUtility.tmpMedicineBestToWorst[k]))
                    {
                        thingDef2 = CardUtility.tmpMedicineBestToWorst[k];
                    }
                }
                if (thingDef2 != null && (thingDef == null || thingDef2.GetStatValueAbstract(StatDefOf.MedicalPotency, null) > thingDef.GetStatValueAbstract(StatDefOf.MedicalPotency, null)))
                {
                    thingDef = thingDef2;
                }
            }
            CardUtility.tmpMedicineBestToWorst.Clear();
            return thingDef;
        }

        private static float DrawLeftRow(Rect leftRect, float curY, string leftLabel, string rightLabel, Color rightLabelColor, TipSignal tipSignal)
        {
            Rect rect = new Rect(0f, curY, leftRect.width, 20f);
            if (Mouse.IsOver(rect))
            {
                GUI.color = CardUtility.HighlightColor;
                GUI.DrawTexture(rect, TexUI.HighlightTex);
            }
            GUI.color = Color.white;
            Widgets.Label(new Rect(0f, curY, leftRect.width * 0.65f, 30f), leftLabel);
            GUI.color = rightLabelColor;
            Widgets.Label(new Rect(leftRect.width * 0.65f, curY, leftRect.width * 0.35f, 30f), rightLabel);
            TooltipHandler.TipRegion(new Rect(0f, curY, leftRect.width, 20f), tipSignal);
            curY += 20f;
            return curY;
        }

     
        
    }
}
