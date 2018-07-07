using System.Collections.Generic;
using Verse;

namespace MyRimworldMod
{
    public class MyMapComp : MapComponent
    {
        public Dictionary<int,float> myDict;
        public MyMapComp(Map map) : base(map)
        {
            Log.Message("In Constructor");
            
        }

        public override void MapGenerated()
        {
            base.MapGenerated();
            Log.Message("Generated");
            myDict = new Dictionary<int, float>();
            myDict.Add(6, 32f);
        }
        public override void ExposeData()
        {
            Log.Message("Exposing data");
            Scribe_Collections.Look(ref myDict, "myDict", LookMode.Value, LookMode.Value);
            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {

            }
        }
        public override void MapComponentTick()
        {
            Log.Message("Dict size : " + myDict.Count);
        }


    }

}
