using ICities;
using System;
using UnityEngine;

namespace PurchaseIt
{
    public class Areas : AreasExtensionBase
    {
        public override void OnCreated(IAreas areas)
        {
            try
            {
                if (ModConfig.Instance.Purchasable)
                {
                    areas.maxAreaCount = 25;
                }
                else
                {
                    areas.maxAreaCount = 9;
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Purchase It!] Areas:OnCreated -> Exception: " + e.Message);
            }
        }

        public override bool OnCanUnlockArea(int x, int z, bool originalResult)
        {
            try
            {
                if (ModConfig.Instance.PurchasableWithoutMilestones)
                {
                    return true;
                }
                else
                {
                    return originalResult;
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Purchase It!] Areas:OnCanUnlockArea -> Exception: " + e.Message);
                return originalResult;
            }
        }

        public override int OnGetAreaPrice(uint ore, uint oil, uint forest, uint fertility, uint water, bool road, bool train, bool ship, bool plane, float landFlatness, int originalPrice)
        {
            try
            {
                if (ModConfig.Instance.PurchasableForFixedPrice)
                {
                    return ModConfig.Instance.FixedPrice * 100;
                }
                else
                {
                    return originalPrice;
                };
            }
            catch (Exception e)
            {
                Debug.Log("[Purchase It!] Areas:OnGetAreaPrice -> Exception: " + e.Message);
                return originalPrice;
            }
        }
    }
}