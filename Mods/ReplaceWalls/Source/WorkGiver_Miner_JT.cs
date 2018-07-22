using RimWorld;
using System.Collections.Generic;
using System.Diagnostics;
using Verse;

namespace JTReplaceWalls
{
    using System.Linq;

    //Called through def
	public class WorkGiver_Miner_JT : WorkGiver_Miner
    {

        [DebuggerHidden]
        public override IEnumerable<Thing> PotentialWorkThingsGlobal(Pawn pawn)
        {
            foreach (Designation des in pawn.Map.designationManager.allDesignations.Where(x=>x.def == DesignationDefOf.Mine))
            {
                bool mayBeAccessible = false;
                for (int i = 0; i < 4; i++)
                {
                    IntVec3 c = des.target.Cell + GenAdj.CardinalDirections[i];
                    if (c.InBounds(pawn.Map) && c.Walkable(pawn.Map))
                    {
                        mayBeAccessible = true;
                        break;
                    }
                }
                if (mayBeAccessible)
                {
                    Thing j = MineUtility.MineableInCell(des.target.Cell, pawn.Map);
                    if (j != null)
                    {
                        if (hasBlueprint(pawn.Map, j.Position))
                        {
                            yield return j;
                        }
                    }
                }
            }
        }

        public bool hasBlueprint(Map map, IntVec3 pos)
        {
            List<Thing> list = map.thingGrid.ThingsListAt(pos);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] is Blueprint)
                {
                    return true;
                }
            }
            return false;
        }

        /* Alpha 15
        [DebuggerHidden]
		public override IEnumerable<Thing> PotentialWorkThingsGlobal(Pawn pawn)
		{
			IEnumerator<Designation> enumerator = Find.DesignationManager.DesignationsOfDef(DesignationDefOf.Mine).GetEnumerator();
			while (enumerator.MoveNext())
			{
				Designation current = enumerator.Current;
				bool flag = false;
				for (int i = 0; i < 4; i++)
				{
					IntVec3 c = current.target.Cell + GenAdj.CardinalDirections[i];
					if (c.InBounds() && c.Walkable())
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					Thing thing = MineUtility.MineableInCell(current.target.Cell);
					if (thing != null)
					{
                        if (hasBlueprint(thing.Position))
						yield return thing;
					}
				}
			}
			yield break;
		}
        
        public bool hasBlueprint(IntVec3 pos)
        {
            List<Thing> list = Find.ThingGrid.ThingsListAt(pos);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] is Blueprint)
                {
                    return true;
                }
            }
            return false;
        }*/

    }
}
