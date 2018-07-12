using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Control
{
    public class PawnOpinionCache
    {
        public Pawn me;
        Dictionary<int, int> opinionCache = new Dictionary<int, int>();
        List<int> opinions = new List<int>();
        List<Pawn> pawns = new List<Pawn>();
        bool isLeader;
        public PawnOpinionCache(bool isLeader, List<Pawn> pawns,Pawn me)
        {
            this.isLeader = isLeader;
            this.pawns = pawns;
            this.me = me;
            foreach (var pawn in pawns)
            {
                int hash = pawn.GetHashCode();
                opinionCache.Add(hash, LookupOpinionOfMe(pawn));
                opinions.Add(opinionCache[hash]);
            }
        }
        public int TotalOpinion
        {
            get { return opinions.Sum(); }
        }
        public int GetOpinionOfMe(Pawn pawn)
        {
            int hash = pawn.GetHashCode();
            if (!opinionCache.ContainsKey(hash))
            {
                opinionCache.Add(hash, LookupOpinionOfMe(pawn));
                opinions.Add(opinionCache[hash]);
                pawns.Add(pawn);
            }
            return opinionCache[hash];
        }
        public int LookupOpinionOfMe(Pawn pawn)
        {
            return pawn.relations.OpinionOf(me);
        }
        public void Tick(int currentTick, int i)
        {
            if (currentTick % 60 == i)
            {
                opinions = new List<int>();
                foreach (var pawn in pawns)
                {
                    opinionCache[pawn.GetHashCode()] = LookupOpinionOfMe(pawn);
                    opinions.Add(opinionCache[pawn.GetHashCode()]);
                }
            }
        }


    }
}
