using RimWorld;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Verse;
using Verse.AI;

namespace MyRimworldMod
{
    public struct PawnDistTuple
    {
        public Pawn pawn;
        public float distance;

        public PawnDistTuple(Pawn pawn, float distance)
        {
            this.pawn = pawn;
            this.distance = distance;
        }
    }

    public static class PawnFinder
    {

        private const int DefaultLocalTraverseRegionsBeforeGlobal = 30;
        const int maxDist = 30;

        public static Dictionary<int, List<Pawn>> nearbyPawnsLookup = new Dictionary<int, List<Pawn>>();
        static List<Pawn> chachedPawns = new List<Pawn>();
        static int indexr = 0;

        public static List<Pawn> GetNearbyPawns(Pawn pawn)
        {
            int key = pawn.GetHashCode();
            if (!nearbyPawnsLookup.ContainsKey(key))
            {
                nearbyPawnsLookup.Add(key, RetrieveNearbyPawns(pawn));
                chachedPawns.Add(pawn);
            }
            return nearbyPawnsLookup[key];
        }

        private static List<Pawn> RetrieveNearbyPawns(Pawn pawn)
        {
            return GetNearbyPawns
                            (pawn.Position,
                            pawn.Map,
                            ThingRequest.ForDef(pawn.def),
                            PathEndMode.Touch,
                            TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, false),
                            maxDist);
        }

        public static void Update()
        {
            if (chachedPawns.Count > 0)
            {
                nearbyPawnsLookup[chachedPawns[indexr].GetHashCode()] = RetrieveNearbyPawns(chachedPawns[indexr]);
                indexr++;
                if (indexr == chachedPawns.Count)
                {
                    indexr = 0;
                }
            }
        }

        private static bool EarlyOutSearch(IntVec3 start, Map map, ThingRequest thingReq, IEnumerable<Thing> customGlobalSearchSet)
        {
            if (thingReq.group == ThingRequestGroup.Everything)
            {
                Log.Error("Cannot do ClosestThingReachable searching everything without restriction.");
                return true;
            }
            if (!start.InBounds(map))
            {
                Log.Error(string.Concat(new object[]
                {
                    "Did FindClosestThing with start out of bounds (",
                    start,
                    "), thingReq=",
                    thingReq
                }));
                return true;
            }
            return thingReq.group == ThingRequestGroup.Nothing || (customGlobalSearchSet == null && !thingReq.IsUndefined && map.listerThings.ThingsMatching(thingReq).Count == 0);
        }

        static List<Pawn> GetNearbyPawns(IntVec3 root, Map map, ThingRequest thingReq, PathEndMode peMode, TraverseParms traverseParams, float maxDistance = 9999f, Predicate<Thing> validator = null, IEnumerable<Thing> customGlobalSearchSet = null, int searchRegionsMin = 0, int searchRegionsMax = -1, bool forceGlobalSearch = false, RegionType traversableRegionTypes = RegionType.Set_Passable, bool ignoreEntirelyForbiddenRegions = false)
        {

            Predicate<Thing> validator2 = (Thing t) => map.reachability.CanReach(root, t, peMode, traverseParams) && (validator == null || validator(t));
            IEnumerable<Thing> searchSet = customGlobalSearchSet ?? map.listerThings.ThingsMatching(thingReq);

            var pawns = PawnFinder.FindPawns(root, searchSet, maxDistance, validator2, null);
            Log.Message("Pawns : " + pawns.Count());
            return pawns;
        }

        static List<Pawn> FindPawns(IntVec3 center, IEnumerable searchSet, float maxDistance = 99999f, Predicate<Thing> validator = null, Func<Thing, float> priorityGetter = null)
        {
            if (searchSet == null)
            {
                return null;
            }


            float maxDist2 = maxDistance * maxDistance;
            var nearbyPawns = new List<Pawn>();
            foreach (Thing thing in searchSet)
            {
                if (thing.Spawned)
                {
                    float squaredist = (float)(center - thing.Position).LengthHorizontalSquared;
                    if (squaredist <= maxDist2)
                    {
                        Pawn pawn = thing as Pawn;
                        nearbyPawns.Add(pawn);
                    }
                }
            }
            return nearbyPawns;
        }

        static Thing ClosestThing_Global_Reachable(IntVec3 center, Map map, IEnumerable<Thing> searchSet, PathEndMode peMode, TraverseParms traverseParams, float maxDistance = 9999f, Predicate<Thing> validator = null, Func<Thing, float> priorityGetter = null)
        {
            if (searchSet == null)
            {
                return null;
            }
            int num = 0;
            int num2 = 0;
            Thing result = null;
            float num3 = -3.40282347E+38f;
            float num4 = maxDistance * maxDistance;
            float num5 = 2.14748365E+09f;
            foreach (Thing current in searchSet)
            {
                if (current.Spawned)
                {
                    num2++;
                    float num6 = (float)(center - current.Position).LengthHorizontalSquared;
                    if (num6 <= num4)
                    {
                        if (priorityGetter != null || num6 < num5)
                        {
                            if (map.reachability.CanReach(center, current, peMode, traverseParams))
                            {
                                if (validator == null || validator(current))
                                {
                                    float num7 = 0f;
                                    if (priorityGetter != null)
                                    {
                                        num7 = priorityGetter(current);
                                        if (num7 < num3)
                                        {
                                            continue;
                                        }
                                        if (num7 == num3 && num6 >= num5)
                                        {
                                            continue;
                                        }
                                    }
                                    result = current;
                                    num5 = num6;
                                    num3 = num7;
                                    num++;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
