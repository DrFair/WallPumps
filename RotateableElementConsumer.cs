using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSerialization;
using STRINGS;
using UnityEngine;
using Harmony;

namespace WallPumps
{

    [SkipSaveFileSerialization]
    [SerializationConfig(MemberSerialization.OptIn)]
    public class RotatableElementConsumer : ElementConsumer
    {
        // When using this, remember to always use RotateablePump too instead of Pump
        [SerializeField]
        public Vector3 rotatableCellOffset; // Set this instead, since sampleCellOffset will be constantly overridden
    }

    // The patch to apply GetSampleCell

    [HarmonyPatch(typeof(ElementConsumer))]
    [HarmonyPatch("GetSampleCell")]
    public static class ElementConsumer_GetSampleCell_Patch
    {
        public static void Prefix(ElementConsumer __instance)
        {
            if (__instance is RotatableElementConsumer)
            {
                Vector3 rotatableCellOffset = ((RotatableElementConsumer)__instance).rotatableCellOffset;
                Rotatable rotatable = __instance.GetComponent<Rotatable>();
                if (rotatable != null) __instance.sampleCellOffset = Rotatable.GetRotatedOffset(rotatableCellOffset, rotatable.GetOrientation());
                //Debug.Log("GetSampleCell call " + rotatableCellOffset + ", " + __instance.sampleCellOffset);
            }
        }
    }
}
