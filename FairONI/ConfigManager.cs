using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony;

namespace FairONI
{
    public static class ConfigManager
    {

    }
    
    [HarmonyPatch(typeof(ModsScreen), "OnActivate")]
    internal class ModsScreen_OnSpawn_Patch
    {

        public static void PostFix(ModsScreen __instance)
        {
            // Change mods screen with config button?
        }

    }
}
