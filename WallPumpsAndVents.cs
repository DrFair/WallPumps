using FairONI;
using Harmony;

namespace WallPumpsAndVents
{

    public static class WallPumpsAndVents
    {
        public static readonly Tag WallMachineRefinedMetals = TagManager.Create("WallMachineRefinedMetals");
        public static readonly Tag WallMachineMetals = TagManager.Create("WallMachineMetals");
    }

    [HarmonyPatch(typeof(GeneratedBuildings))]
    [HarmonyPatch("LoadGeneratedBuildings")]
    public static class GeneratedBuildings_LoadGeneratedBuildings_Patch
    {
        public static void Prefix()
        {
            Debug.Log(" === WallPumpsAndVents v. 2.0 LoadGeneratedBuildings === ");

            AddTags.AddStrings(WallPumpsAndVents.WallMachineRefinedMetals, "Wall Machine Refined Metals");
            AddTags.AddStrings(WallPumpsAndVents.WallMachineMetals, "Wall Machine Metals");
            WallPumpsAndVentsConfig config = WallPumpsAndVentsConfig.GetConfig();

            GasWallPump.Setup();
            LiquidWallPump.Setup();
            GasWallVent.Setup();
            GasWallVentHighPressure.Setup();
            LiquidWallVent.Setup();
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
                    ElementUtils.AddOreTag(e, WallPumpsAndVents.WallMachineRefinedMetals);
                }
                if (e.HasTag(GameTags.Metal))
                {
                    ElementUtils.AddOreTag(e, WallPumpsAndVents.WallMachineMetals);
                }
            }
            ElementUtils.AddOreTag(ElementLoader.FindElementByHash(SimHashes.SuperInsulator), WallPumpsAndVents.WallMachineRefinedMetals);
            ElementUtils.AddOreTag(ElementLoader.FindElementByHash(SimHashes.SuperInsulator), WallPumpsAndVents.WallMachineMetals);
        }
    }

    [HarmonyPatch(typeof(Game), "OnPrefabInit")]
    public static class Game_OnPrefabInit_Patch
    {
        public static void Postfix(Game __instance)
        {
            Debug.Log(" === WallPumpsAndVents v. 2.0 OnPrefabInit === ");
        }
    }
}
