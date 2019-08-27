using System;
using System.IO;
using Harmony;
using UnityEngine;

namespace FairONI
{
    public static class ConfigUtils
    {

        public static string GetConfigDirectoryPath()
        {
            string pathSeperator = Path.DirectorySeparatorChar.ToString();
            return Path.Combine(Util.RootFolder(), "mods" + pathSeperator + "settings");
        }

        public static string GetConfigFilePath(string modName)
        {
            string pathSeperator = Path.DirectorySeparatorChar.ToString();
            return Path.Combine(GetConfigDirectoryPath(), modName + ".json");
        }

        // Look at https://docs.unity3d.com/Manual/JSONSerialization.html for serializeableObject limitations
        // serializeableObject must have [Serializable] annotation
        public static void LoadConfig(string modName, object serializeableObject)
        {
            try
            {
                string filePath = GetConfigFilePath(modName);
                if (!File.Exists(filePath))
                {
                    Debug.Log("Creating initial config for " + modName);
                    SaveConfig(modName, serializeableObject);
                    return;
                }
                string json = File.ReadAllText(filePath);
                JsonUtility.FromJsonOverwrite(json, serializeableObject);
            }
            catch (Exception e)
            {
                Debug.LogError("Error loading " + modName + " config:");
                Debug.LogError(e);
            }
        }

        // Look at https://docs.unity3d.com/Manual/JSONSerialization.html for serializeableObject limitations
        // serializeableObject must have [Serializable] annotation
        public static void SaveConfig(string modName, object serializeableObject)
        {
            try
            {
                Directory.CreateDirectory(GetConfigDirectoryPath());
                string json = JsonUtility.ToJson(serializeableObject, true);
                File.WriteAllText(GetConfigFilePath(modName), json);
            }
            catch (Exception e)
            {
                Debug.LogError("Error saving " + modName + " config:");
                Debug.LogError(e);
            }
        }
    }
    
    //[HarmonyPatch(typeof(ModsScreen), "OnActivate")]
    //internal class ModsScreen_OnSpawn_Patch
    //{

    //    public static void PostFix(ModsScreen __instance)
    //    {
    //        // Change mods screen with config button?
    //    }

    //}
}
