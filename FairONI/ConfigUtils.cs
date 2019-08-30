using System;
using System.IO;
using Newtonsoft.Json;

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
        public static T LoadConfig<T>(string modName, T defaultObject)
        {
            try
            {
                string filePath = GetConfigFilePath(modName);
                if (!File.Exists(filePath))
                {
                    Debug.Log("Creating initial config for " + modName);
                    SaveConfig(modName, defaultObject);
                    return defaultObject;
                }
                string json = File.ReadAllText(filePath);
                T obj = JsonConvert.DeserializeObject<T>(json);
                // Save the config file again, since it may have changed
                SaveConfig(modName, obj);
                return obj;
            }
            catch (Exception e)
            {
                Debug.Log("Error loading " + modName + " config:");
                Debug.Log(e);
            }
            return defaultObject;
        }

        // Look at https://www.newtonsoft.com/json/help/html/Introduction.htm for serializeableObject limitations
        public static void SaveConfig(string modName, object serializeableObject)
        {
            try
            {
                Directory.CreateDirectory(GetConfigDirectoryPath());
                string json = JsonConvert.SerializeObject(serializeableObject, Formatting.Indented);
                File.WriteAllText(GetConfigFilePath(modName), json);
            }
            catch (Exception e)
            {
                Debug.Log("Error saving " + modName + " config:");
                Debug.Log(e);
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
