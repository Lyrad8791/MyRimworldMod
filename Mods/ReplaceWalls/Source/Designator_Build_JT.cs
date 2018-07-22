using Verse;
using RimWorld;

namespace JTReplaceWalls
{
	public class Designator_Build_JT : Designator_Place //Replicate same inheritance and you can access private fields with dummy fields
    {
        //Dummy fields; this is because the replacing method is called from the original Designator_Build class and has access to the original fields even if private
        //This also means that class fields (I.E. bellow) aren't actually ever accessed and is only here so this compiles.
        public BuildableDef entDef;
        public ThingDef stuffDef;

        //Static class fields can be accessed by the replacing method: be careful not to make dummy fields static.
        public static bool wall = false;
        public static bool check = true;
        public static bool embrasure = true;
        public static bool concrete = true;

        public override AcceptanceReport CanDesignateCell(IntVec3 c)
        {
           
            return GenConstruct_JT.CanPlaceBlueprintAt(entDef, c, placingRot, Map, stuffDef, DebugSettings.godMode, null);
        }

        //Should never be called, here just because compiler complains
        public override BuildableDef PlacingDef
        {
            get
            {
                return entDef;
            }
        }

    }
}
