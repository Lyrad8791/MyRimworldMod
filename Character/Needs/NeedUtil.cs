using DubsBadHygiene;
using Verse;

namespace Control
{
    public static class NeedUtil
    {
        public static Need_Sex GetSexNeed(Pawn pawn)
        {
            return pawn.needs.TryGetNeed<Need_Sex>();
        }
        public static Need_Bladder GetBladder(Pawn pawn)
        {
            return pawn.needs.TryGetNeed<Need_Bladder>();
        }
        public const float SexTrigger = 0.51f;
        public const float BladderTrigger = 0.51f;

    }
}