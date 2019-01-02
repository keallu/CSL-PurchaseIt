using Harmony;
using System;
using UnityEngine;

namespace PurchaseIt
{
    [HarmonyPatch(typeof(GameAreaManager), "CanUnlock")]
    public static class GameAreaManagerCanUnlockPatch
    {
        static bool Postfix(bool __result, GameAreaManager __instance, int x, int z)
        {
            try
            {
                if (ModConfig.Instance.PurchasableWithoutMilestones)
                {
                    if (x < 0 || z < 0 || x >= 5 || z >= 5)
                    {
                        return false;
                    }

                    bool result = __instance.IsUnlocked(x, z - 1) || __instance.IsUnlocked(x - 1, z) || __instance.IsUnlocked(x + 1, z) || __instance.IsUnlocked(x, z + 1);
                    __instance.m_AreasWrapper.OnCanUnlockArea(x, z, ref result);
                    return result;
                }
                else
                {
                    return __result;
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Purchase It!] GameAreaManagerCanUnlockPatch:Postfix -> Exception: " + e.Message);
                return __result;
            }
        }
    }

    [HarmonyPatch(typeof(GameAreaManager), "GetFreeBounds")]
    public static class GameAreaManagerGetFreeBoundsPatch
    {
        static Bounds Postfix(Bounds __result)
        {
            try
            {
                if (ModConfig.Instance.PurchasableWithoutMilestones)
                {
                    Vector3 zero = Vector3.zero;
                    Vector3 zero2 = Vector3.zero;

                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            zero.x = Mathf.Min(zero.x, ((float)(j - 1) - 2.5f) * 1920f);
                            zero2.x = Mathf.Max(zero2.x, ((float)(j + 2) - 2.5f) * 1920f);
                            zero.z = Mathf.Min(zero.z, ((float)(i - 1) - 2.5f) * 1920f);
                            zero2.z = Mathf.Max(zero2.z, ((float)(i + 2) - 2.5f) * 1920f);
                            zero2.y = Mathf.Max(zero2.y, 1024f);
                        }
                    }

                    Bounds result = default(Bounds);
                    result.SetMinMax(zero, zero2);
                    return result;
                }
                else
                {
                    return __result;
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Purchase It!] GameAreaManagerGetFreeBoundsPatch:Postfix -> Exception: " + e.Message);
                return __result;
            }
        }
    }
}