using RimWorld;
using Verse;

namespace MyRimworldMod
{
    public class Need_Discipline : Need
    {
        public static float GetVal(Pawn pawn)
        {
            return pawn.needs.TryGetNeed<Need_Discipline>().CurLevelPercentage;
        }

        public override void SetInitialLevel()
        {
            this.CurLevelPercentage = 1f;
        }
        public override void NeedInterval()
        {
            if (pawn.gender == Gender.Female)
            {
                this.CurLevelPercentage -= Capacity_SelfDiscipline.GetAuthority(pawn);
            }
        }
    }
}