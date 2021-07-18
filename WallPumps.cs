using FairONI;
using HarmonyLib;

namespace WallPumps
{
    public class WallPumps : KMod.UserMod2
    {
        public static readonly Tag WallMachineRefinedMetals = TagManager.Create("WallMachineRefinedMetals");
        public static readonly Tag WallMachineMetals = TagManager.Create("WallMachineMetals");

        public override void OnLoad(Harmony harmony)
        {
            Debug.Log(" === WallPumps v. 2.5 OnLoad === ");
            harmony.PatchAll();
        }
    }
    [HarmonyPatch(typeof(Db))]
    [HarmonyPatch("Initialize")]
    public static class Db_Initialize_Patch
    {
        public static void Prefix()
        {
            Debug.Log(" === WallPumps v. 2.5 Db_Initialize Prefix === ");

            AddTags.AddStrings(WallPumps.WallMachineRefinedMetals, "Wall Machine Refined Metals");
            AddTags.AddStrings(WallPumps.WallMachineMetals, "Wall Machine Metals");
            WallPumpsConfig config = WallPumpsConfig.GetConfig();
        }

        public static void Postfix()
        {
            Debug.Log(" === WallPumps v. 2.5 Db_Initialize Postfix === ");
            // Add buildings
            GasWallPump.Setup();
            LiquidWallPump.Setup();
            GasWallVent.Setup();
            GasWallVentHighPressure.Setup();
            LiquidWallVent.Setup();

            // Add tech tree/building menu entries
            GasWallPump.AddToMenus();
            LiquidWallPump.AddToMenus();
            GasWallVent.AddToMenus();
            GasWallVentHighPressure.AddToMenus();
            LiquidWallVent.AddToMenus();
        }
    }

    [HarmonyPatch(typeof(ElementLoader))]
    [HarmonyPatch("FinaliseElementsTable")]
    public static class ElementLoader_FinaliseElementsTable_Patch
    {
        public static void Postfix()
        {
            // Add new material it to all refined metals + insulation
            foreach (Element e in ElementLoader.elementTable.Values)
            {
                if (e.HasTag(GameTags.RefinedMetal))
                {
                    ElementUtils.AddOreTag(e, WallPumps.WallMachineRefinedMetals);
                }
                if (e.HasTag(GameTags.Metal))
                {
                    ElementUtils.AddOreTag(e, WallPumps.WallMachineMetals);
                }
            }
            ElementUtils.AddOreTag(ElementLoader.FindElementByHash(SimHashes.SuperInsulator), WallPumps.WallMachineRefinedMetals);
            ElementUtils.AddOreTag(ElementLoader.FindElementByHash(SimHashes.SuperInsulator), WallPumps.WallMachineMetals);
        }
    }

    //[HarmonyPatch(typeof(Game), "OnPrefabInit")]
    //public static class Game_OnPrefabInit_Patch
    //{
    //    public static void Postfix(Game __instance)
    //    {
    //        Debug.Log(" === WallPumps v. 2.5 OnPrefabInit === ");
    //    }
    //}
}
