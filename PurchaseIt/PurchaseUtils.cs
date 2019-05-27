using ColossalFramework;
using System;
using System.Collections;
using UnityEngine;

namespace PurchaseIt
{
    public static class PurchaseUtils
    {
        public static void PurchaseAll()
        {
            try
            {
                SimulationManager simulationManager = Singleton<SimulationManager>.instance;

                if (simulationManager != null)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            simulationManager.AddAction(Purchase(i, j));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Rebuild It!] PurchaseUtils:PurchaseAll -> Exception: " + e.Message);
            }
        }

        private static IEnumerator Purchase(int x, int y)
        {
            try
            {
                GameAreaManager gameAreaManager = Singleton<GameAreaManager>.instance;

                if (gameAreaManager != null)
                {
                    if (!gameAreaManager.IsUnlocked(x, y))
                    {
                        int tileIndex = gameAreaManager.GetTileIndex(x, y);
                        gameAreaManager.UnlockArea(tileIndex);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Rebuild It!] PurchaseUtils:Purchase -> Exception: " + e.Message);
            }

            yield return null;
        }
    }
}
