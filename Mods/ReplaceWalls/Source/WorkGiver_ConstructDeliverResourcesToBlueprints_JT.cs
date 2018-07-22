using Verse;
using Verse.AI;
using RimWorld;

namespace JTReplaceWalls
{
    //Called through def
	public class WorkGiver_ConstructDeliverResourcesToBlueprints_JT : WorkGiver_ConstructDeliverResourcesToBlueprints
    {

        private Job DeconstructExistingEdificeJob(Pawn pawn, Blueprint blue)
        {
            if (!blue.def.entityDefToBuild.IsEdifice())
            {
                return null;
            }
            Thing thing = GenConstruct.MiniToInstallOrBuildingToReinstall(blue);
            Thing thing2 = null;
            CellRect cellRect = blue.OccupiedRect();
            for (int i = cellRect.minZ; i <= cellRect.maxZ; i++)
            {
                int j = cellRect.minX;
                while (j <= cellRect.maxX)
                {
                    IntVec3 c = new IntVec3(j, 0, i);
                    thing2 = c.GetEdifice(pawn.Map);
                    if (thing2 == thing)
                    {
                        thing2 = null;
                    }
                    if (thing2 != null)
                    {
                        ThingDef thingDef = blue.def.entityDefToBuild as ThingDef;
                        //if (thingDef != null && thingDef.building.canPlaceOverWall && thing2.def == ThingDefOf.Wall)
                        if (thingDef != null && thingDef.building.canPlaceOverWall && GenConstruct_JT.walls.Contains(thing2.def.defName))
                        {
                            return null;
                        }
                        //Start of my code, thingDef is new, thing2 is old
                        if (GenConstruct_JT.doors.Contains(thingDef.defName) && GenConstruct_JT.doors.Contains(thing2.def.defName))
                        {
                            return null;
                        }
                        //End of my code
                        break;
                    }
                    else
                    {
                        j++;
                    }
                }
                if (thing2 != null)
                {
                    break;
                }
            }
            if (thing2 == null || !pawn.CanReserve(thing2, 1))
            {
                return null;
            }
            return new Job(JobDefOf.Deconstruct, thing2)
            {
                ignoreDesignations = true
            };
        }
    }
}
