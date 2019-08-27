using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony;
using FairONI;

namespace WallPumps
{

    [HarmonyPatch(typeof(GeneratedBuildings))]
    [HarmonyPatch("LoadGeneratedBuildings")]
    public static class GeneratedBuildings_LoadGeneratedBuildings_Patch
    {
        public static void Prefix()
        {
            GasWallPump.Setup();
            LiquidWallPump.Setup();
        }
    }

    //[HarmonyPatch(typeof(Game), "OnPrefabInit")]
    //public static class Game_OnPrefabInit_Patch
    //{
    //    public static void Postfix(Game __instance)
    //    {
    //        Debug.Log(" === WallPumps Postfix === ");
    //    }
    //}
}
