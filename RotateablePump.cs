using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony;
using UnityEngine;

namespace WallPumps
{
    public class RotateablePump : Pump
    {
        // Had to create this, since vanilla pump doesn't take rotation into account when getting if it's currently pumpable
    }

    // The patch to apply IsPumpable

    [HarmonyPatch(typeof(Pump))]
    [HarmonyPatch("IsPumpable")]
    [HarmonyPatch(new Type[] { typeof(Element.State), typeof(int) })]
    public static class Pump_IsPumpable_Patch
    {
        public static void Postfix(Pump __instance, ref bool __result, Element.State expected_state)
        {
            if (__instance is RotateablePump)
            {
                Rotatable rotatable = __instance.GetComponent<Rotatable>();
                RotatableElementConsumer consumer = __instance.GetComponent<RotatableElementConsumer>();
                //Debug.Log("IsPumpable call " + consumer.rotatableCellOffset + ", " + consumer.sampleCellOffset);
                // Basically a copy of vanilla Pump IsPumpable, but with different initial
                int num = Grid.PosToCell(__instance.transform.GetPosition() + Rotatable.GetRotatedOffset(consumer.rotatableCellOffset, rotatable.GetOrientation()));
                for (int i = 0; i < consumer.consumptionRadius; i++)
                {
                    for (int j = 0; j < consumer.consumptionRadius; j++)
                    {
                        int num2 = num + j + Grid.WidthInCells * i;
                        bool flag = Grid.Element[num2].IsState(expected_state);
                        if (flag)
                        {
                            __result = true;
                            return;
                        }
                    }
                }
                __result = false;
                return;
            }
        }
    }
}
