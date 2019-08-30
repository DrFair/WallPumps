using System;
using FairONI;

namespace WallPumps
{
    [Serializable]
    public class WallPumpsConfig
    {

        private static WallPumpsConfig _instance = null;
        public static WallPumpsConfig GetConfig()
        {
            if (_instance == null)
            {
                _instance = ConfigUtils.LoadConfig("WallPumps", new WallPumpsConfig());
            }
            return _instance;
        }
        
        public WallPumpConfig gasWallPump = new WallPumpConfig(120, 0.2f, 1f);
        public WallPumpConfig liquidWallPump = new WallPumpConfig(120, 4f, 1f);
        public WallVentConfig gasWallVent = new WallVentConfig(2f, 1f);
        public WallVentConfig gasWallPressureVent = new WallVentConfig(20f, 1f);
        public WallVentConfig liquidWallVent = new WallVentConfig(1000f, 1f);

        // Example of child json object in config file. Remember object must be [Serializable] aswell.
        //public Dictionary<string, object> jsonObject = new Dictionary<string, object>();
    }

    [Serializable]
    public class WallPumpConfig
    {
        public bool enabled = true;
        public float energyConsumption;
        public float pumpRate;
        public float thermalConductivity;

        public WallPumpConfig(float energyConsumption, float pumpRate, float thermalConductivity)
        {
            this.energyConsumption = energyConsumption;
            this.pumpRate = pumpRate;
            this.thermalConductivity = thermalConductivity;
        }
    }

    [Serializable]
    public class WallVentConfig
    {
        public bool enabled = true;
        public float maxPressure;
        public float thermalConductivity;

        public WallVentConfig(float maxPressure, float thermalConductivity)
        {
            this.maxPressure = maxPressure;
            this.thermalConductivity = thermalConductivity;
        }
    }
}
