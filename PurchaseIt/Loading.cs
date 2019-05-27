using ColossalFramework.UI;
using ICities;
using System;
using UnityEngine;

namespace PurchaseIt
{
    public class Loading : LoadingExtensionBase
    {
        private LoadMode _loadMode;
        private GameObject _purchasePanelGameObject;

        public override void OnLevelLoaded(LoadMode mode)
        {
            try
            {
                _loadMode = mode;

                if (_loadMode != LoadMode.LoadGame && _loadMode != LoadMode.NewGame && _loadMode != LoadMode.NewGameFromScenario)
                {
                    return;
                }
                UIView uiView = UnityEngine.Object.FindObjectOfType<UIView>();
                if (uiView != null)
                {
                    _purchasePanelGameObject = new GameObject("PurchaseItPurchasePanel");
                    _purchasePanelGameObject.transform.parent = uiView.transform;
                    _purchasePanelGameObject.AddComponent<PurchasePanel>();
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Purchase It!] Loading:OnLevelLoaded -> Exception: " + e.Message);
            }
        }

        public override void OnLevelUnloading()
        {
            try
            {
                if (_loadMode != LoadMode.LoadGame && _loadMode != LoadMode.NewGame && _loadMode != LoadMode.NewGameFromScenario)
                {
                    return;
                }

                if (_purchasePanelGameObject != null)
                {
                    UnityEngine.Object.Destroy(_purchasePanelGameObject);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Purchase It!] Loading:OnLevelUnloading -> Exception: " + e.Message);
            }
        }
    }
}