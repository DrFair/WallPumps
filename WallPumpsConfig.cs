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
                ConfigUtils.LoadConfig("WallPumps", _instance = new WallPumpsConfig());
            }
            return _instance;
        }

        public float wallGasPumpEnergy = 120f;
        public float wallGasPumpRate = 0.2f;

        public float wallLiquidPumpEnergy = 120f;
        public float wallLiquidPumpRate = 4f;

        public float thermalConductivity = 1f;

        // Example of child json object in config file. Remember object must be [Serializable] aswell.
        //public Dictionary<string, object> jsonObject = new Dictionary<string, object>();
    }
}
