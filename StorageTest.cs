using RimWorld;
using System;
using UnityEngine;
using Verse;
using HugsLib;
using HugsLib.Utils;
using System.Collections.Generic;

namespace MyRimworldMod
{
    public class StorageTest : ModBase
    {
        public override string ModIdentifier
        {
            get { return "StorageTest"; }
        }
        public static MyBillManager myBillManager;

        public override void WorldLoaded()
        {
            myBillManager = UtilityWorldObjectManager.GetUtilityWorldObject<MyBillManager>();
            myBillManager.billGiverLookup = new Dictionary<Name, IBillGiver>();
        }

        
    }
    public class MyBillManager : UtilityWorldObject
    {
        public Dictionary<Name, IBillGiver> billGiverLookup;

        public IBillGiver GetIBillGiver(Pawn pawn)
        {
            if (billGiverLookup.ContainsKey(pawn.Name))
            {
                return billGiverLookup[pawn.Name];
            }
            else
            {
                IBillGiver giver = new MyBillGiver(pawn);
                billGiverLookup.Add(pawn.Name, giver);
                return giver;
            }
        }
        public override void PostAdd()
        {
            base.PostAdd();
        }

        public override void ExposeData()
        {
            base.ExposeData();
            //Scribe_Values.Look(ref testInt, "testInt", 0);
            //Scribe_Values.Look(ref testString, "testString", "");
        }
    }
    public class MyBillGiver : IBillGiver
    {
        public MyBillGiver(Pawn _pawn)
        {
            pawn = _pawn;
            billStack = new BillStack(this);
        }
        public Pawn pawn;
        BillStack billStack;

        public Map Map { get { return pawn.Map; } }

        public BillStack BillStack => billStack;

        public IEnumerable<IntVec3> IngredientStackCells => pawn.IngredientStackCells;

        public bool CurrentlyUsableForBills()
        {
            return pawn.CurrentlyUsableForBills();
        }
    }
}