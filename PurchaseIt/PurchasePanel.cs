using ColossalFramework;
using ColossalFramework.UI;
using System;
using UnityEngine;

namespace PurchaseIt
{
    public class PurchasePanel : UIPanel
    {
        private bool _initialized;

        private GameAreaInfoPanel _gameAreaInfoPanel;
        private UIButton _purchaseAllButton;

        public override void Awake()
        {
            try
            {

            }
            catch (Exception e)
            {
                Debug.Log("[Purchase It!] PurchasePanel:Awake -> Exception: " + e.Message);
            }
        }

        public override void OnEnable()
        {
            try
            {

            }
            catch (Exception e)
            {
                Debug.Log("[Purchase It!] PurchasePanel:OnEnable -> Exception: " + e.Message);
            }
        }

        public override void Start()
        {
            try
            {
                CreateUI();

                if (_gameAreaInfoPanel == null)
                {
                    _gameAreaInfoPanel = GameObject.Find("(Library) GameAreaInfoPanel").GetComponent<GameAreaInfoPanel>();
                }

                _gameAreaInfoPanel.component.eventVisibilityChanged += (component, value) =>
                {
                    if (value && Singleton<GameAreaManager>.instance.m_areaCount < 25)
                    {
                        isVisible = true;
                    }
                    else
                    {
                        isVisible = false;
                    }
                };
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

        public override void OnDisable()
        {
            try
            {

            }
            catch (Exception e)
            {
                Debug.Log("[Purchase It!] PurchasePanel:OnDisable -> Exception: " + e.Message);
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
                absolutePosition = new Vector3(Mathf.Floor((GetUIView().fixedWidth - width) / 2f), Mathf.Floor((GetUIView().fixedHeight - height) / 1.35f));
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

                        ConfirmPanel.ShowModal("Confirmation", "Are you sure you wish to purchase all tiles for free now?", delegate (UIComponent comp, int ret)
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