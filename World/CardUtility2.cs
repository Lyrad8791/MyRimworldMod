using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;
using HugsLib;
using HugsLib.Utils;

namespace Control
{

    public class PawnControlSettings
    {
        public bool Control = false;
        public bool Influence = false;
    }

    public class CultManager : UtilityWorldObject
    {
       
        public override void PostAdd()
        {
            base.PostAdd();
 
        }

        public override void ExposeData()
        {
            base.ExposeData();
        }
    }


    public class StorageTest : ModBase
    {
        public override string ModIdentifier
        {
            get { return "StorageTest"; }
        }

        public override void WorldLoaded()
        {
            var obj = UtilityWorldObjectManager.GetUtilityWorldObject<CultManager>();
            
        }

    }
}
