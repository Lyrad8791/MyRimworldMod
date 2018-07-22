using System;
using RimWorld;
using Verse;
using System.Reflection;
using System.Collections.Generic;

namespace JTReplaceWalls
{
    //Looked at PetFollow for this atrribute
    [StaticConstructorOnStartup] //If I ever change this, check that XML changes bellow work if mods after bellow this mod
    class Vaccine
    {
        static Vaccine()
        {
            MethodInfo methodVanilla;
            MethodInfo methodReplace;
            try
            {
                methodVanilla = typeof(Designator_Build).GetMethod(
                    "CanDesignateCell",
                    BindingFlags.Instance | BindingFlags.Public);
                methodReplace = typeof(Designator_Build_JT).GetMethod(
                    "CanDesignateCell",
                    BindingFlags.Instance | BindingFlags.Public);
                giveAutism(methodVanilla, methodReplace);
                Log.Message(
                    "JTReplaceWalls probably sucessfully replaced Designator_Build.CanDesignateCell(). Code stolen from CCL/RawCode.");
            }
            catch (Exception e)
            {
                Log.Warning("JTReplaceWalls couldn't replace Designator_Build.CanDesignateCell(). The error is the following: " + e.Message);
            }
            try
            {
            methodVanilla = typeof(GenConstruct).GetMethod("PlaceBlueprintForBuild", BindingFlags.Static | BindingFlags.Public);
            methodReplace = typeof(GenConstruct_JT).GetMethod("PlaceBlueprintForBuild", BindingFlags.Static | BindingFlags.Public);
            giveAutism(methodVanilla, methodReplace);
            Log.Message("JTReplaceWalls probably sucessfully replaced GenConstruct.PlaceBlueprintForBuild(). Code stolen from CCL/RawCode.");
            }
            catch (Exception e)
            {
                Log.Warning("JTReplaceWalls couldn't replace GenConstruct.PlaceBlueprintForBuild(). The error is the following: " + e.Message);
            }
            try
            {
            methodVanilla = typeof(GenConstruct).GetMethod("BlocksConstruction", BindingFlags.Static | BindingFlags.Public);
            methodReplace = typeof(GenConstruct_JT).GetMethod("BlocksConstruction", BindingFlags.Static | BindingFlags.Public);
            giveAutism(methodVanilla, methodReplace);
            Log.Message("JTReplaceWalls probably sucessfully replaced GenConstruct.BlocksConstruction(). Code stolen from CCL/RawCode.");
            }
            catch (Exception e)
            {
                Log.Warning("JTReplaceWalls couldn't replace GenConstruct.BlocksFramePlacement(). The error is the following: " + e.Message);
            }
            try
            {
            methodVanilla = typeof(GenSpawn).GetMethod("SpawningWipes",  BindingFlags.Static | BindingFlags.Public);
            methodReplace = typeof(GenSpawn_JT).GetMethod("SpawningWipes", BindingFlags.Static | BindingFlags.Public);
            giveAutism(methodVanilla, methodReplace);
            Log.Message("JTReplaceWalls probably sucessfully replaced GenSpawn.SpawningWipes(). Code stolen from CCL/RawCode.");
            }
            catch (Exception e)
            {
                Log.Warning("JTReplaceWalls couldn't replace GenConstruct.BlocksFramePlacement(). The error is the following: " + e.Message);
            }
            

            //The following is necessary so walls underneath will be removed when new wall is built, not done in XML for some compatibility
            //ThingDefOf.Wall.building.canPlaceOverWall = true; //Old version
            DefDatabase<ThingDef>.GetNamed("Wall").building.canPlaceOverWall = true;

            //Allow other mods to make their walls/doors/conduits replaceable.
            //To do this, create a new static method that returns IEnumerable<string> with the method name as JTReplaceWalls_walls, JTReplaceWalls_doors or JTReplaceWalls_conduits
            ///E.G. this method will let the defs NewDoor and NewSuperDoor be replaced by other doors
            ///namespace mymod {
            ///	class myclass {
            ///		public static IEnumerable<string> JTReplaceWalls_doors() {
            ///			yield return "NewDoor";
            ///			yield return "NewSuperDoor";
            ///		}
            ///	}
            ///}
            foreach (ModContentPack mod in LoadedModManager.RunningMods)
            {
                foreach (Assembly ass in mod.assemblies.loadedAssemblies)
                {
                    foreach (Type type in ass.GetTypes())
                    {
                        foreach (MethodInfo method in type.GetMethods())
                        {
                            switch (method.Name)
                            {
                                case "JTReplaceWalls_walls":
                                    addFromMethod(GenConstruct_JT.walls, method);
                                    break;
                                case "JTReplaceWalls_doors":
                                    addFromMethod(GenConstruct_JT.doors, method);
                                    break;
                                case "JTReplaceWalls_conduits":
                                    addFromMethod(GenConstruct_JT.conduits, method, false);
                                    break;
                            }
                        }
                    }
                }
            }

        }

        public static void addFromMethod(HashSet<string> list, MethodInfo method, bool canPlaceOverWall = true)
        {
            foreach (string s in (IEnumerable<string>)method.Invoke(null, null) ?? new List<string>())
            {
                list.Add(s);
                if (DefDatabase<ThingDef>.GetNamed(s, false) != null)
                {
                    Log.Message("JTReplaceWalls: detected " + s);
                    if (canPlaceOverWall)
                    {
                        DefDatabase<ThingDef>.GetNamed(s, false).building.canPlaceOverWall = true;
                    }
                }
            }
        }

        //This shit stolen from CCL because I'm a pleb programmer. CCL got code from user RawCode.
        static unsafe void giveAutism(MethodInfo source, MethodInfo destination)
        {
            if (IntPtr.Size == sizeof(Int64))
            {
                // 64-bit systems use 64-bit absolute address and jumps
                // 12 byte destructive

                // Get function pointers
                long Source_Base = source.MethodHandle.GetFunctionPointer().ToInt64();
                long Destination_Base = destination.MethodHandle.GetFunctionPointer().ToInt64();

                // Native source address
                byte* Pointer_Raw_Source = (byte*)Source_Base;

                // Pointer to insert jump address into native code
                long* Pointer_Raw_Address = (long*)(Pointer_Raw_Source + 0x02);

                // Insert 64-bit absolute jump into native code (address in rax)
                // mov rax, immediate64
                // jmp [rax]
                *(Pointer_Raw_Source + 0x00) = 0x48;
                *(Pointer_Raw_Source + 0x01) = 0xB8;
                *Pointer_Raw_Address = Destination_Base; // ( Pointer_Raw_Source + 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09 )
                *(Pointer_Raw_Source + 0x0A) = 0xFF;
                *(Pointer_Raw_Source + 0x0B) = 0xE0;
            }
            else
            {
                // 32-bit systems use 32-bit relative offset and jump
                // 5 byte destructive
                // Get function pointers
                int Source_Base = source.MethodHandle.GetFunctionPointer().ToInt32();
                int Destination_Base = destination.MethodHandle.GetFunctionPointer().ToInt32();

                // Native source address
                byte* Pointer_Raw_Source = (byte*)Source_Base;

                // Pointer to insert jump address into native code
                int* Pointer_Raw_Address = (int*)(Pointer_Raw_Source + 1);

                // Jump offset (less instruction size)
                int offset = (Destination_Base - Source_Base) - 5;

                // Insert 32-bit relative jump into native code
                *Pointer_Raw_Source = 0xE9;
                *Pointer_Raw_Address = offset;
            }
            // done!
        }
    }
}
