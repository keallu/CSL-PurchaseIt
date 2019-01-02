namespace PurchaseIt
{
    [ConfigurationPath("PurchaseItConfig.xml")]
    public class ModConfig
    {
        public bool ConfigUpdated { get; set; }
        public bool Purchasable { get; set; } = true;
        public bool PurchasableWithoutMilestones { get; set; } = true;
        public bool PurchasableForFixedPrice { get; set; } = false;
        public int FixedPrice { get; set; }

        private static ModConfig instance;

        public static ModConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Configuration<ModConfig>.Load();
                }

                return instance;
            }
        }

        public void Save()
        {
            Configuration<ModConfig>.Save();
            ConfigUpdated = true;
        }
    }
}