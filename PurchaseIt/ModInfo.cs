using Harmony;
using ICities;
using System.Reflection;

namespace PurchaseIt
{
    public class ModInfo : IUserMod
    {
        public string Name => "Purchase It!";
        public string Description => "Allows to purchase any of the 25 tiles anytime.";

        public void OnEnabled()
        {
            var harmony = HarmonyInstance.Create("com.github.keallu.csl.purchaseit");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public void OnDisabled()
        {
            var harmony = HarmonyInstance.Create("com.github.keallu.csl.purchaseit");
            harmony.UnpatchAll();
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            UIHelperBase group;
            bool selected;
            float selectedValue;
            int result;

            group = helper.AddGroup(Name);

            selected = ModConfig.Instance.Purchasable;
            group.AddCheckbox("25 tiles purchasable (requires reload if you are in-game)", selected, sel =>
            {
                ModConfig.Instance.Purchasable = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.PurchasableWithoutMilestones;
            group.AddCheckbox("25 tiles purchasable without milestones", selected, sel =>
            {
                ModConfig.Instance.PurchasableWithoutMilestones = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.PurchasableForFixedPrice;
            group.AddCheckbox("25 tiles purchasable for fixed price", selected, sel =>
            {
                ModConfig.Instance.PurchasableForFixedPrice = sel;
                ModConfig.Instance.Save();
            });

            selectedValue = ModConfig.Instance.FixedPrice;

            group.AddTextfield("Fixed price", selectedValue.ToString(), sel =>
            {
                int.TryParse(sel, out result);
                ModConfig.Instance.FixedPrice = result;
                ModConfig.Instance.Save();
            });
        }
    }
}