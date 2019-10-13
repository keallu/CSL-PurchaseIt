using ColossalFramework;
using ColossalFramework.UI;
using System;
using UnityEngine;

namespace PurchaseIt
{
    public class PurchasePanel : UIPanel
    {
        private bool _initialized;

        private GameAreaManager _gameAreaManager;
        private GameAreaInfoPanel _gameAreaInfoPanel;
        private UIButton _purchaseAllButton;

        public override void Start()
        {
            try
            {
                if (_gameAreaManager == null)
                {
                    _gameAreaManager = Singleton<GameAreaManager>.instance;
                }

                if (_gameAreaInfoPanel == null)
                {
                    _gameAreaInfoPanel = GameObject.Find("(Library) GameAreaInfoPanel").GetComponent<GameAreaInfoPanel>();
                }

                _gameAreaInfoPanel.component.eventVisibilityChanged += (component, value) =>
                {
                    if (value && ModConfig.Instance.PurchasableWithoutMilestones && _gameAreaManager.m_maxAreaCount == 25 && _gameAreaManager.m_areaCount < _gameAreaManager.m_maxAreaCount)
                    {
                        isVisible = true;
                    }
                    else
                    {
                        isVisible = false;
                    }
                };

                CreateUI();
            }
            catch (Exception e)
            {
                Debug.Log("[Purchase It!] PurchasePanel:Start -> Exception: " + e.Message);
            }
        }

        public override void Update()
        {
            try
            {
                if (!_initialized || ModConfig.Instance.ConfigUpdated)
                {
                    _initialized = true;
                    ModConfig.Instance.ConfigUpdated = false;
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Purchase It!] PurchasePanel:Update -> Exception: " + e.Message);
            }
        }

        public override void OnDestroy()
        {
            try
            {
                if (_purchaseAllButton != null)
                {
                    Destroy(_purchaseAllButton);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Purchase It!] PurchasePanel:OnDestroy -> Exception: " + e.Message);
            }
        }

        private void CreateUI()
        {
            try
            {
                height = 57f;
                width = 310f;
                absolutePosition = new Vector3(Mathf.Floor((GetUIView().fixedWidth - width) / 2f), Mathf.Floor((GetUIView().fixedHeight - height) / 1.25f));
                isVisible = false;

                _purchaseAllButton = UIUtils.CreateButton(this, "PurchaseAllButton", "PURCHASE ALL FOR FREE");
                _purchaseAllButton.height = 57f;
                _purchaseAllButton.width = 310f;
                _purchaseAllButton.anchor = UIAnchorStyle.CenterHorizontal;

                _purchaseAllButton.textScale = 1.25f;
                _purchaseAllButton.useDropShadow = true;
                _purchaseAllButton.dropShadowOffset = new Vector2(0f, -1.33f);
                _purchaseAllButton.eventClick += (component, eventParam) =>
                {
                    if (!eventParam.used)
                    {
                        eventParam.Use();

                        ConfirmPanel.ShowModal("Confirmation", "Are you sure you wish to purchase all remaining tiles for free now?", delegate (UIComponent comp, int ret)
                        {
                            if (ret == 1)
                            {
                                isVisible = false;

                                PurchaseUtils.PurchaseAll();
                            }
                        });
                    }
                };
            }
            catch (Exception e)
            {
                Debug.Log("[Purchase It!] PurchasePanel:CreateUI -> Exception: " + e.Message);
            }
        }
    }
}