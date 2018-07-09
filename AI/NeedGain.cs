using RimWorld;
using System;
using Verse;

namespace MyRimworldMod
{
    public struct NeedModification
    {
        public NeedDef def;
        Action<Need> endingModification;

        public NeedModification(NeedDef def, Action<Need> endingModification)
        {
            this.def = def;
            this.endingModification = endingModification;
        }
        public NeedModification(string defname, Action<Need> endingModification)
        {
            this.def = DefDatabase<NeedDef>.GetNamed(defname);
            this.endingModification = endingModification;
        }

        public void Apply(Pawn pawn)
        {
            endingModification(pawn.needs.TryGetNeed(def));
        }

    }
}