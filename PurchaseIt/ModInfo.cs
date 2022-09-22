using CitiesHarmony.API;
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
            HarmonyHelper.DoOnHarmonyReady(() => Patcher.PatchAll());
        }

        public void OnDisabled()
        {
            if (HarmonyHelper.IsHarmonyInstalled)
            {
                Patcher.UnpatchAll();
            }
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            UIHelperBase group;

            AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();

            group = helper.AddGroup(Name + " - " + assemblyName.Version.Major + "." + assemblyName.Version.Minor);

            bool selected;
            float selectedValue;
            int value;

            selected = ModConfig.Instance.Purchasable;
            group.AddCheckbox("25 tiles purchasable (requires reload if you are in-game)", selected, sel =>
            {
                ModConfig.Instance.Purchasable = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.PurchasableWithoutMilestones;
            group.AddCheckbox("All tiles purchasable without milestones", selected, sel =>
            {
                ModConfig.Instance.PurchasableWithoutMilestones = sel;
                ModConfig.Instance.Save();
            });

            selected = ModConfig.Instance.PurchasableForFixedPrice;
            group.AddCheckbox("All tiles purchasable for fixed price", selected, sel =>
            {
                ModConfig.Instance.PurchasableForFixedPrice = sel;
                ModConfig.Instance.Save();
            });

            selectedValue = ModConfig.Instance.FixedPrice;

            group.AddTextfield("Fixed price", selectedValue.ToString(), sel =>
            {
                int.TryParse(sel, out value);
                ModConfig.Instance.FixedPrice = value;
                ModConfig.Instance.Save();
            });
        }
    }
}