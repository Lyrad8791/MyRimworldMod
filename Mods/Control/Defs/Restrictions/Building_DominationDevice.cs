using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace Control
{
    

    public class Building_DominationDevice : Building_WorkTable, IAssignableBuilding,IBillGiver
    {
        
        public List<Pawn> owners = new List<Pawn>();
        public static List<Building_DominationDevice> targetsAway = new List<Building_DominationDevice>();
        public static List<Pawn> pawnsToSave = new List<Pawn>();
        public IEnumerable<Pawn> AssigningCandidates
        {
            get
            {
                if (!base.Spawned)
                {
                    return Enumerable.Empty<Pawn>();
                }
                return base.Map.mapPawns.FreeColonists;
            }
        }

        public IEnumerable<Pawn> AssignedPawns
        {
            get
            {
                return this.owners;
            }
        }

        public int MaxAssignedPawnsCount
        {
            get
            {
                return 1;
            }
        }

        private bool PlayerCanSeeOwners
        {
            get
            {
                if (base.Faction == Faction.OfPlayer)
                {
                    return true;
                }
                for (int i = 0; i < this.owners.Count; i++)
                {
                    if (this.owners[i].Faction == Faction.OfPlayer || this.owners[i].HostFaction == Faction.OfPlayer)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public Building_DominationDevice()
        {
            this.billStack = new BillStack(this);

        }
        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Deep.Look<BillStack>(ref this.billStack, "billStack", new object[]
            {
                this
            });
            Scribe_Collections.Look(ref owners, "owners", LookMode.Reference);
        }


        public override void DrawExtraSelectionOverlays()
        {
            base.DrawExtraSelectionOverlays();
            Room room = this.GetRoom(RegionType.Set_Passable);
            if (room != null && Building_Bed.RoomCanBePrisonCell(room))
            {
                room.DrawFieldEdges();
            }
        }

        
        public override IEnumerable<Gizmo> GetGizmos()
        {
            foreach (Gizmo g in base.GetGizmos())
            {
                yield return g;
            }
            if (this.def.building.bed_humanlike && base.Faction == Faction.OfPlayer)
            {
                Command_Toggle pris = new Command_Toggle();

                yield return new Command_Action
                {
                    defaultLabel = "CommandBedSetOwnerLabel".Translate(),
                    icon = ContentFinder<Texture2D>.Get("UI/Commands/AssignOwner", true),
                    defaultDesc = "CommandBedSetOwnerDesc".Translate(),
                    action = delegate
                    {
                        Find.WindowStack.Add(new Dialog_AssignBuildingOwner(this));
                    },
                    hotKey = KeyBindingDefOf.Misc3
                };
            }
        }


        


        public override void DrawGUIOverlay()
        {
            if (Find.CameraDriver.CurrentZoom == CameraZoomRange.Closest && this.PlayerCanSeeOwners)
            {
                Color defaultThingLabelColor = GenMapUI.DefaultThingLabelColor;
                if (!this.owners.Any<Pawn>())
                {
                    GenMapUI.DrawThingLabel(this, "Unowned".Translate(), defaultThingLabelColor);
                }
                else if (this.owners.Count == 1)
                {

                    GenMapUI.DrawThingLabel(this, this.owners[0].NameStringShort, defaultThingLabelColor);
                }

            }
        }

        public void TryAssignPawn(Pawn pawn)
        {
 
            this.owners = new List<Pawn> { pawn };
            targetsAway.Add(this);
        }

        public void TryUnassignPawn(Pawn pawn)
        {
            if (owners[0].health.hediffSet.HasHediff(DefDatabase<HediffDef>.GetNamed("Restrained")))
            {
                pawnsToSave.Add(owners[0]);
            }
            this.owners = new List<Pawn> ();
            targetsAway.Remove(this);
        }

        
    }
}


