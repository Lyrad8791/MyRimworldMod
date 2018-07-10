using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace Control
{
    public class Hediff_Control : HediffWithComps
    {

        public override void Tick()
        {
            base.Tick();            
        }

        //public void InfluenceOthers()
        //{
        //    var influenceTargets = InfluenceCalculator.GetInfluenceTargets();
        //    int totalPriority = influenceTargets.Sum(x => x.InfluencePriority);
        //    float myInfluenceScore = InfluenceCalculator.GetInfluenceOffense(pawn);
        //    foreach (var influenceTarget in influenceTargets)
        //    {
        //        influenceTarget.GetInfluenced(ControlLevel * myInfluenceScore * influenceTarget.InfluencePriority/totalPriority);
        //    }
        //} 
    }
       
   
}
