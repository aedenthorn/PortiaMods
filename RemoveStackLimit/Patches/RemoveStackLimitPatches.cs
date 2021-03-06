﻿using System;
using Harmony12;
using Pathea.ItemSystem;

namespace RemoveStackLimit.Patches
{
    /// <summary>
    /// Patches to remove stack limit.
    /// </summary>
    public static class RemoveStackLimitPatches
    {
        private static bool Enabled => Main.Enabled;

        private static void UpdateStackLimit(ItemBaseConfData data)
        {
            if (data == null || data.StackLimitedNumber <= 0) return;

            var oldLimit = data.StackLimitedNumber;
            var isStackable = oldLimit > 1;
            data.StackLimitedNumber = isStackable ? Main.Settings.StackSizeForStackable : Main.Settings.StackSizeForUnstackable;
        }

        [HarmonyPatch(typeof(ItemDataMgr), nameof(ItemDataMgr.GetItemBaseData))]
        public static class ItemDataMgrGetItemBaseData
        {
            public static void Postfix(ItemBaseConfData __result)
            {
                if (!Enabled) return;

                try
                {
                    UpdateStackLimit(__result);
                }
                catch (Exception exception)
                {
                    Main.Logger.Exception(exception);
                }
            }
        }

        [HarmonyPatch(typeof(ItemDataMgr), nameof(ItemDataMgr.GetItemBaseConfDataByIndex))]
        public static class ItemDataMgrGetItemBaseConfDataByIndex
        {
            public static void Postfix(ItemBaseConfData __result)
            {
                if (!Enabled) return;

                try
                {
                    UpdateStackLimit(__result);
                }
                catch (Exception exception)
                {
                    Main.Logger.Exception(exception);
                }
            }
        }
    }
}