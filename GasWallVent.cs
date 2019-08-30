using UnityEngine;
using TUNING;
using FairONI;
using STRINGS;

namespace WallPumps
{
    public class GasWallVent : IBuildingConfig
    {
        public const string ID = "FairGasWallVent";
        
        public static void Setup()
        {
            AddBuilding.AddStrings(ID, "Gas Wall Vent", "A gas vent that's also a wall", "Releases " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " into a room");
            if (WallPumpsConfig.GetConfig().gasWallVent.enabled)
            {
                AddBuilding.AddBuildingToPlanScreen("HVAC", ID, "GasVent");
                AddBuilding.IntoTechTree("GasPiping", ID);
            }

        }

        public override BuildingDef CreateBuildingDef()
        {
            string[] constructionMats = { WallPumps.WallMachineMetals.Name };
            BuildingDef def = BuildingTemplates.CreateBuildingDef(
                ID,
                1,
                1,
                "fairgaswallvent_kanim",
                30,
                30f,
                TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER1,
                constructionMats,
                1600f,
                BuildLocationRule.Tile,
                TUNING.BUILDINGS.DECOR.PENALTY.TIER1,
                NOISE_POLLUTION.NONE,
                0.2f);
            BuildingTemplates.CreateFoundationTileDef(def);
            
            def.InputConduitType = ConduitType.Gas;
            def.Floodable = false;
            def.Overheatable = false;
            def.ViewMode = OverlayModes.GasConduits.ID;
            def.AudioCategory = "Metal";
            def.UtilityInputOffset = new CellOffset(0, 0);
            def.UtilityOutputOffset = new CellOffset(0, 1);
            def.PermittedRotations = PermittedRotations.R360;
            // Tile properties
            def.ThermalConductivity = WallPumpsConfig.GetConfig().gasWallVent.thermalConductivity;
            def.UseStructureTemperature = false;
            def.Entombable = false;
            def.BaseTimeUntilRepair = -1f;
            def.ObjectLayer = ObjectLayer.Building;
            def.SceneLayer = Grid.SceneLayer.TileMain;
            def.ForegroundLayer = Grid.SceneLayer.TileMain;
            def.isSolidTile = true;
            return def;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            GeneratedBuildings.MakeBuildingAlwaysOperational(go);
            go.AddOrGet<RotatableExhaust>();
            Vent vent = go.AddOrGet<Vent>();
            vent.conduitType = ConduitType.Gas;
            vent.endpointType = Endpoint.Sink;
            vent.overpressureMass = WallPumpsConfig.GetConfig().gasWallVent.maxPressure;
            ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
            conduitConsumer.conduitType = ConduitType.Gas;
            conduitConsumer.ignoreMinMassCheck = true;
            Storage storage = BuildingTemplates.CreateDefaultStorage(go, false);
            storage.showInUI = true;
            go.AddOrGet<SimpleVent>();
            SimCellOccupier simCellOccupier = go.AddOrGet<SimCellOccupier>();
            simCellOccupier.notifyOnMelt = true;
            go.AddOrGet<Insulator>();
            go.AddOrGet<TileTemperature>();
            BuildingHP buildingHP = go.AddOrGet<BuildingHP>();
            buildingHP.destroyOnDamaged = true;
        }

        public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
        {
            AddVisualizer(go, true);
        }

        public override void DoPostConfigureUnderConstruction(GameObject go)
        {
            AddVisualizer(go, false);
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            go.AddOrGetDef<VentController.Def>();
            AddVisualizer(go, false);

            GeneratedBuildings.RemoveLoopingSounds(go);
        }

        private static void AddVisualizer(GameObject go, bool movable)
        {
            StationaryChoreRangeVisualizer stationaryChoreRangeVisualizer = go.AddOrGet<StationaryChoreRangeVisualizer>();
            Rotatable rotatable = go.AddOrGet<Rotatable>();
            CellOffset offset = Rotatable.GetRotatedCellOffset(new CellOffset(0, 1), rotatable.GetOrientation());
            stationaryChoreRangeVisualizer.x = offset.x;
            stationaryChoreRangeVisualizer.y = offset.y;
            stationaryChoreRangeVisualizer.width = 1;
            stationaryChoreRangeVisualizer.height = 1;
            stationaryChoreRangeVisualizer.movable = movable;
        }
    }
}
