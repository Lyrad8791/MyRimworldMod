using RimWorld;
using Verse;

namespace JTReplaceWalls
{
	public static class GenSpawn_JT
	{

        public static bool SpawningWipes(BuildableDef newEntDef, BuildableDef oldEntDef)
        {
            ThingDef thingDef = newEntDef as ThingDef;
            ThingDef thingDef2 = oldEntDef as ThingDef;
            if (thingDef == null || thingDef2 == null)
            {
                return false;
            }
            if (thingDef.category == ThingCategory.Attachment || thingDef.category == ThingCategory.Mote || thingDef.category == ThingCategory.Filth || thingDef.category == ThingCategory.Projectile)
            {
                return false;
            }
            if (!thingDef2.destroyable)
            {
                return false;
            }
            if (thingDef.category == ThingCategory.Plant)
            {
                return false;
            }
            if (thingDef2.category == ThingCategory.Filth && thingDef.passability != Traversability.Standable)
            {
                return true;
            }
            //if (thingDef.EverTransmitsPower && thingDef2 == ThingDefOf.PowerConduit)
            if (thingDef.EverTransmitsPower && GenConstruct_JT.conduits.Contains(thingDef2.defName))
            {
                return true;
            }
            if (thingDef.IsFrame && GenSpawn.SpawningWipes(thingDef.entityDefToBuild, oldEntDef))
            {
                return true;
            }
            BuildableDef buildableDef = GenConstruct.BuiltDefOf(thingDef);
            BuildableDef buildableDef2 = GenConstruct.BuiltDefOf(thingDef2);
            if (buildableDef == null || buildableDef2 == null)
            {
                return false;
            }
            ThingDef thingDef3 = thingDef.entityDefToBuild as ThingDef;
            if (thingDef2.IsBlueprint)
            {
                if (thingDef.IsBlueprint)
                {
                    if (thingDef3 != null && thingDef3.building != null && thingDef3.building.canPlaceOverWall && thingDef2.entityDefToBuild is ThingDef && (ThingDef)thingDef2.entityDefToBuild == ThingDefOf.Wall)
                    {
                        return true;
                    }
                    if (thingDef2.entityDefToBuild is TerrainDef)
                    {
                        if (thingDef.entityDefToBuild is ThingDef && ((ThingDef)thingDef.entityDefToBuild).coversFloor)
                        {
                            return true;
                        }
                        if (thingDef.entityDefToBuild is TerrainDef)
                        {
                            return true;
                        }
                    }
                }
                return thingDef2.entityDefToBuild == ThingDefOf.PowerConduit && thingDef.entityDefToBuild is ThingDef && (thingDef.entityDefToBuild as ThingDef).EverTransmitsPower;
            }
            if ((thingDef2.IsFrame || thingDef2.IsBlueprint) && thingDef2.entityDefToBuild is TerrainDef)
            {
                ThingDef thingDef4 = buildableDef as ThingDef;
                if (thingDef4 != null && !thingDef4.CoexistsWithFloors)
                {
                    return true;
                }
            }
            if (thingDef2 == ThingDefOf.ActiveDropPod)
            {
                return false;
            }
            if (thingDef == ThingDefOf.ActiveDropPod)
            {
                return thingDef2 != ThingDefOf.ActiveDropPod && (thingDef2.category == ThingCategory.Building && thingDef2.passability == Traversability.Impassable);
            }
            if (thingDef.IsEdifice())
            {
                if (thingDef.BlockPlanting && thingDef2.category == ThingCategory.Plant)
                {
                    return true;
                }
                if (!(buildableDef is TerrainDef) && buildableDef2.IsEdifice())
                {
                    return true;
                }
            }
            return false;
        }

        /* Alpha 15
        public static bool SpawningWipes(BuildableDef newEntDef, BuildableDef oldEntDef)
		{
			ThingDef thingDef = newEntDef as ThingDef;
			ThingDef thingDef2 = oldEntDef as ThingDef;
			if (thingDef == null || thingDef2 == null)
			{
				return false;
			}
			if (thingDef.category == ThingCategory.Attachment || thingDef.category == ThingCategory.Mote || thingDef.category == ThingCategory.Filth || thingDef.category == ThingCategory.Projectile)
			{
				return false;
			}
			if (!thingDef2.destroyable)
			{
				return false;
			}
			if (thingDef.category == ThingCategory.Plant)
			{
				return false;
			}
			if (thingDef2.category == ThingCategory.Filth && thingDef.passability != Traversability.Standable)
			{
				return true;
			}
            //if (thingDef.EverTransmitsPower && thingDef2 == ThingDefOf.PowerConduit)
            if (thingDef.EverTransmitsPower && GenConstruct_JT.conduits.Contains(thingDef2.defName))
            {
				return true;
			}
			if (thingDef.IsFrame && GenSpawn.SpawningWipes(thingDef.entityDefToBuild, oldEntDef))
			{
				return true;
			}
			BuildableDef buildableDef = GenConstruct.BuiltDefOf(thingDef);
			BuildableDef buildableDef2 = GenConstruct.BuiltDefOf(thingDef2);
			if (buildableDef == null || buildableDef2 == null)
			{
				return false;
			}
			ThingDef thingDef3 = thingDef.entityDefToBuild as ThingDef;
			if (thingDef2.IsBlueprint)
			{
				if (thingDef.IsBlueprint)
				{
					if (thingDef3 != null && thingDef3.building != null && thingDef3.building.canPlaceOverWall && thingDef2.entityDefToBuild is ThingDef && (ThingDef)thingDef2.entityDefToBuild == ThingDefOf.Wall)
					{
						return true;
					}
					if (thingDef2.entityDefToBuild is TerrainDef)
					{
						if (thingDef.entityDefToBuild is ThingDef && ((ThingDef)thingDef.entityDefToBuild).coversFloor)
						{
							return true;
						}
						if (thingDef.entityDefToBuild is TerrainDef)
						{
							return true;
						}
					}
				}
				return thingDef2.entityDefToBuild == ThingDefOf.PowerConduit && thingDef.entityDefToBuild is ThingDef && (thingDef.entityDefToBuild as ThingDef).EverTransmitsPower;
			}
			if ((thingDef2.IsFrame || thingDef2.IsBlueprint) && thingDef2.entityDefToBuild is TerrainDef)
			{
				ThingDef thingDef4 = buildableDef as ThingDef;
				if (thingDef4 != null && !thingDef4.CoexistsWithFloors)
				{
					return true;
				}
			}
			if (thingDef2 == ThingDefOf.DropPod)
			{
				return false;
			}
			if (thingDef == ThingDefOf.DropPod)
			{
				return thingDef2 != ThingDefOf.DropPod && (thingDef2.category == ThingCategory.Building && thingDef2.passability == Traversability.Impassable);
			}
			if (thingDef.IsEdifice())
			{
				if (thingDef.BlockPlanting && thingDef2.category == ThingCategory.Plant)
				{
					return true;
				}
				if (!(buildableDef is TerrainDef) && buildableDef2.IsEdifice())
				{
					return true;
				}
			}
			return false;
		}*/

	}
}
