using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace MyRimworldMod
{

    public class Authority_Need : Need
    {
        static int disciplineID = -1;
        float clothedBonus = 0.20f;
        float genderBonus = 0.08f;
        float disciplineModifier = 0.02f;
        float socialModifier = 0.02f;
        float armedBonus = 0.2f;

        public Authority_Need(Pawn pawn)
            : base(pawn)
        {
            threshPercents = new List<float>();
            threshPercents.Add(0.3f);
            
        }

        public override void SetInitialLevel()
        {
            base.CurLevelPercentage = 0f;
          
        }

        public override void NeedInterval()
        {
            
            if (!pawn.NonHumanlikeOrWildMan())
            {
                //if (disciplineID == -1)
                //{
                //    var discipline = pawn.skills.skills.Where(x => x.def.GetType() == typeof(SkillDef_Discipline)).Single();
                //    disciplineID = pawn.skills.skills.IndexOf(discipline);
                //}
                float my_percentage = 0;
                if (!pawn.apparel.PsychologicallyNude)
                {
                    my_percentage += clothedBonus;
                }
                if (pawn.gender == Gender.Male)
                {
                    my_percentage += genderBonus;
                }
                my_percentage += pawn.skills.skills.Sum((x) => x.levelInt) * 0.01f;
                CurLevelPercentage = my_percentage;
            }
        }
    }
}
