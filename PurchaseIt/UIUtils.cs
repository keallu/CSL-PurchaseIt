using ColossalFramework.UI;
using UnityEngine;

namespace PurchaseIt
{
    public class UIUtils
    {
        public static UIPanel CreatePanel(UIComponent parent, string name)
        {
            UIPanel panel = parent.AddUIComponent<UIPanel>();
            panel.name = name;

            return panel;
        }

        public static UILabel CreateLabel(UIComponent parent, string name, string text)
        {
            UILabel label = parent.AddUIComponent<UILabel>();
            label.name = name;
            label.text = text;

            return label;
        }

        public static UIButton CreateButton(UIComponent parent, string name, string text)
        {
            UIButton button = parent.AddUIComponent<UIButton>();
            button.name = name;
            button.text = text;

            button.hoveredTextColor = new Color32(7, 132, 255, 255);
            button.pressedTextColor = new Color32(30, 44, 44, 255);
            button.focusedTextColor = new Color32(255, 255, 255, 255);

            button.textHorizontalAlignment = UIHorizontalAlignment.Center;
            button.textVerticalAlignment = UIVerticalAlignment.Middle;

            button.normalBgSprite = "ButtonMenu";
            button.hoveredBgSprite = "ButtonMenuHovered";
            button.pressedBgSprite = "ButtonMenuPressed";
            button.disabledBgSprite = "ButtonMenuDisabled";

            return button;
        }
    }
}
