using System.Collections.Generic;
using RimWorld;
using Verse;

namespace JTReplaceWalls
{
    using RimWorld.BaseGen;

    static class GenConstruct_JT
    {

        public static HashSet<string> walls = new HashSet<string> { "Wall" };
        public static HashSet<string> doors = new HashSet<string> { "Door", "Autodoor" };
        public static HashSet<string> conduits = new HashSet<string> { "PowerConduit" };

        public static AcceptanceReport CanPlaceBlueprintAt(BuildableDef entDef, IntVec3 center, Rot4 rot, Map map, ThingDef stuff, bool godMode = false, Thing thingToIgnore = null)
        {
            CellRect cellRect = GenAdj.OccupiedRect(center, rot, entDef.Size);
            CellRect.CellRectIterator iterator = cellRect.GetIterator();
            while (!iterator.Done())
            {
                IntVec3 current = iterator.Current;
                if (!current.InBounds(map))
                {
                    return new AcceptanceReport("OutOfBounds".Translate());
                }
                if (current.InNoBuildEdgeArea(map) && !DebugSettings.godMode)
                {
                    return "TooCloseToMapEdge".Translate();
                }
                iterator.MoveNext();
            }
            if (center.Fogged(map))
            {
                return "CannotPlaceInUndiscovered".Translate();
            }
            List<Thing> thingList = center.GetThingList(map);
            for (int i = 0; i < thingList.Count; i++)
            {
                Thing thing = thingList[i];
                if (thing != thingToIgnore)
                {
                    if (thing.Position == center && thing.Rotation == rot)
                    {
                        if (thing.def == entDef)
                        {
                            //Start my code, this code allows blueprint to be placed, so canPlaceOverWall can do it's thing.
                            if (((walls.Contains(thing.def.defName) && walls.Contains(entDef.defName)) ||
                                (doors.Contains(thing.def.defName) && doors.Contains(entDef.defName))) &&
                                thing.Stuff != stuff)
                            {
                                return AcceptanceReport.WasAccepted;
                            }
                            //End my code

                            return new AcceptanceReport("IdenticalThingExists".Translate());
                        }
                        if (thing.def.entityDefToBuild == entDef)
                        {
                            if (thing is Blueprint)
                            {
                                return new AcceptanceReport("IdenticalBlueprintExists".Translate());
                            }
                            return new AcceptanceReport("IdenticalThingExists".Translate());
                        }
                    }
                }
            }
            ThingDef thingDef = entDef as ThingDef;
            if (thingDef != null && thingDef.hasInteractionCell)
            {
                
                IntVec3 c = ThingUtility.InteractionCellWhenAt(thingDef, center, rot, map);
                if (!c.InBounds(map))
                {
                    return new AcceptanceReport("InteractionSpotOutOfBounds".Translate());
                }
                List<Thing> list = map.thingGrid.ThingsListAtFast(c);
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[j] != thingToIgnore)
                    {
                        if (list[j].def.passability == Traversability.Impassable)
                        {
                            return new AcceptanceReport("InteractionSpotBlocked".Translate(new object[]
                            {
                        list[j].LabelNoCount
                            }).CapitalizeFirst());
                        }
                        Blueprint blueprint = list[j] as Blueprint;
                        if (blueprint != null && blueprint.def.entityDefToBuild.passability == Traversability.Impassable)
                        {
                            return new AcceptanceReport("InteractionSpotWillBeBlocked".Translate(new object[]
                            {
                        blueprint.LabelNoCount
                            }).CapitalizeFirst());
                        }
                    }
                }
            }
            if (entDef.passability != Traversability.Standable)
            {
                foreach (IntVec3 current2 in GenAdj.CellsAdjacentCardinal(center, rot, entDef.Size))
                {
                    if (current2.InBounds(map))
                    {
                        thingList = current2.GetThingList(map);
                        for (int k = 0; k < thingList.Count; k++)
                        {
                            Thing thing2 = thingList[k];
                            if (thing2 != thingToIgnore)
                            {
                                Blueprint blueprint2 = thing2 as Blueprint;
                                ThingDef thingDef3;
                                if (blueprint2 != null)
                                {
                                    ThingDef thingDef2 = blueprint2.def.entityDefToBuild as ThingDef;
                                    if (thingDef2 == null)
                                    {
                                        goto IL_37E;
                                    }
                                    thingDef3 = thingDef2;
                                }
                                else
                                {
                                    thingDef3 = thing2.def;
                                }
                                if (thingDef3.hasInteractionCell && cellRect.Contains(ThingUtility.InteractionCellWhenAt(thingDef3, thing2.Position, thing2.Rotation, thing2.Map)))
                                {
                                    return new AcceptanceReport("WouldBlockInteractionSpot".Translate(new object[]
                                    {
                                entDef.label,
                                thingDef3.label
                                    }).CapitalizeFirst());
                                }
                            }
                        IL_37E:;
                        }
                    }
                }
            }
            TerrainDef terrainDef = entDef as TerrainDef;
            if (terrainDef != null)
            {
                if (map.terrainGrid.TerrainAt(center) == terrainDef)
                {
                    return new AcceptanceReport("TerrainIsAlready".Translate(new object[]
                    {
                terrainDef.label
                    }));
                }
                if (map.designationManager.DesignationAt(center, DesignationDefOf.SmoothFloor) != null)
                {
                    return new AcceptanceReport("BeingSmoothed".Translate());
                }
            }
            if (!GenConstruct.CanBuildOnTerrain(entDef, center, map, rot, thingToIgnore))
            {
                return new AcceptanceReport("TerrainCannotSupport".Translate());
            }
            if (!godMode)
            {
                CellRect.CellRectIterator iterator2 = cellRect.GetIterator();
                while (!iterator2.Done())
                {
                    thingList = iterator2.Current.GetThingList(map);
                    for (int l = 0; l < thingList.Count; l++)
                    {
                        Thing thing3 = thingList[l];
                        if (thing3 != thingToIgnore)
                        {
                            if (!GenConstruct.CanPlaceBlueprintOver(entDef, thing3.def))
                            {
                                //Start my code, entDef is new, thing3 is old
                                //Allows for doors to replace each other only if different stuff or door <--> autodoor
                                if (thing3?.def?.defName != null)
                                {
                                    if ((doors.Contains(entDef.defName) && doors.Contains(thing3.def.defName)) && //New and old are both doors
                                        (thing3.Stuff != stuff || !entDef.defName.Equals(thing3.def.defName))) //Not the same stuff or not same thing
                                    {
                                        return AcceptanceReport.WasAccepted;
                                    }
                                    //Allow placing over mineable
                                    if (thing3 is Mineable && (doors.Contains(entDef.defName) || walls.Contains(entDef.defName)))
                                    {
                                        return AcceptanceReport.WasAccepted;
                                    }
                                    //Allow walls and doors to be placed over walls
                                    if ((walls.Contains(entDef.defName) || doors.Contains(entDef.defName)) && walls.Contains(thing3.def.defName))
                                    {
                                        return AcceptanceReport.WasAccepted;
                                    }
                                    //Allow replacing invisible power lines from PowerSwitch by Haplo
                                    if (conduits.Contains(entDef.defName) && conduits.Contains(thing3.def.defName))
                                    {
                                        return AcceptanceReport.WasAccepted;
                                    }
                                }
                                //End my code

                                return new AcceptanceReport("SpaceAlreadyOccupied".Translate());
                            }
                        }
                    }
                    iterator2.MoveNext();
                }
            }
            if (entDef.PlaceWorkers != null)
            {
                for (int m = 0; m < entDef.PlaceWorkers.Count; m++)
                {
                    AcceptanceReport result = entDef.PlaceWorkers[m].AllowsPlacing(entDef, center, rot, map, thingToIgnore);
                    if (!result.Accepted)
                    {
                        return result;
                    }
                }
            }
            return AcceptanceReport.WasAccepted;
        }

        public static Blueprint_Build PlaceBlueprintForBuild(BuildableDef sourceDef, IntVec3 center, Map map, Rot4 rotation, Faction faction, ThingDef stuff)
        {
            Blueprint_Build blueprint_Build = (Blueprint_Build)ThingMaker.MakeThing(sourceDef.blueprintDef, null);
            blueprint_Build.SetFactionDirect(faction);
            blueprint_Build.stuffToUse = stuff;
            mineOut(map, center);
            GenSpawn.Spawn(blueprint_Build, center, map, rotation);
            return blueprint_Build;
        }

        public static bool mineOut(Map map, IntVec3 pos)
        {
            Designator_Mine mine = Find.ReverseDesignatorDatabase.Get<Designator_Mine>();
            if (mine != null && mine.CanDesignateCell(pos).Equals(AcceptanceReport.WasAccepted))
            {
                mine.DesignateSingleCell(pos);
                return true;
            }
            return false;
        }

        public static bool BlocksConstruction(Thing constructible, Thing t)
        {
            if (t == constructible)
                return false;
            ThingDef thingDef = !(constructible is Blueprint) ? 
                                    (!(constructible is Frame) 
                                         ? constructible.def.blueprintDef 
                                         : constructible.def.entityDefToBuild.blueprintDef) 
                                    : constructible.def;
            if (t.def.category == ThingCategory.Building &&
                GenSpawn.SpawningWipes(thingDef.entityDefToBuild, (BuildableDef) t.def))
                return true;
            if (t.def.category == ThingCategory.Plant)
                return (double) t.def.plant.harvestWork >= 200.0;
            if (!thingDef.clearBuildingArea ||
                t.def == ThingDefOf.SteamGeyser 
                && thingDef.entityDefToBuild.ForceAllowPlaceOver((BuildableDef) t.def))
                return false;
            ThingDef entityDefToBuild = thingDef.entityDefToBuild as ThingDef;
            return (entityDefToBuild == null ||
                    (!entityDefToBuild.EverTransmitsPower 
                     || t.def != ThingDefOf.PowerConduit 
                     || entityDefToBuild == ThingDefOf.PowerConduit) 
                    && (!walls.Contains(t.def.defName) || 
                        entityDefToBuild.building == null ||
                        !entityDefToBuild.building.canPlaceOverWall)) 
                   && (t.def.IsEdifice() 
                       && entityDefToBuild.IsEdifice() 
                       || t.def.category == ThingCategory.Pawn 
                       || (t.def.category == ThingCategory.Item 
                           && thingDef.entityDefToBuild.passability == Traversability.Impassable 
                           || t.def.Fillage >= FillCategory.Partial));
        }
        
        //A17
        public static bool BlocksFramePlacement(Blueprint blue, Thing t)
        {
            if (t.def.category == ThingCategory.Plant)
            {
                return t.def.plant.harvestWork > 200f;
            }
            if (blue.def.entityDefToBuild is TerrainDef || blue.def.entityDefToBuild.passability == Traversability.Standable)
            {
                return false;
            }
            if (t.def == ThingDefOf.SteamGeyser && blue.def.entityDefToBuild.ForceAllowPlaceOver(t.def))
            {
                return false;
            }
            ThingDef thingDef = blue.def.entityDefToBuild as ThingDef;
            if (thingDef != null)
            {
                if (thingDef.EverTransmitsPower && t.def == ThingDefOf.PowerConduit && thingDef != ThingDefOf.PowerConduit)
                {
                    return false;
                }
                //if (t.def == ThingDefOf.Wall && thingDef.building != null && thingDef.building.canPlaceOverWall)
                if (walls.Contains(t.def.defName) && thingDef.building != null && thingDef.building.canPlaceOverWall)
                {
                    return false;
                }
            }
            return (t.def.IsEdifice() && thingDef.IsEdifice()) || (t.def.category == ThingCategory.Pawn || (t.def.category == ThingCategory.Item && blue.def.entityDefToBuild.passability == Traversability.Impassable)) || (t.def.Fillage >= FillCategory.Partial && thingDef != null && thingDef.Fillage >= FillCategory.Partial);
        }
    }
}
