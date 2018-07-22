using System.Collections.Generic;

namespace JTReplaceWalls.Source.Compat
{
    class Compat
    {
        public static IEnumerable<string> JTReplaceWalls_walls()
        {
            List<string> temp = new List<string> { "Embrasure", "ConcreteWall", "ConcreteEmbrasure", "WallScrapMetal" };
            for (int i = 0; i < temp.Count; i++)
            {
                yield return temp[i];
            }
        }
        public static IEnumerable<string> JTReplaceWalls_doors()
        {
            yield return "ConcreteAutodoor";
        }
        public static IEnumerable<string> JTReplaceWalls_conduits()
        {
            yield return "PowerConduitInvisible";
        }
    }
}
