using Terraria;
using Terraria.ModLoader;
using ExpiryMode.Global_;
using ExpiryMode.Mod_;
using Terraria.ID;

namespace ExpiryMode.Items.Equippables.Accessories
{
	public class TheWormsTooth : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Corrupt Tooth");
			Tooltip.SetDefault("Allows for the random chance of generating a circle of cursed flame balls upon hit of an enemy\nMelee strikes generate more cursed flames");
		}
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 12317;
            item.accessory = true;
            item.rare = ExpiryRarity.Expiry;
        }
		public override void UpdateEquip(Player player) 
		{
			player.GetModPlayer<InfiniteSuffPlayer>().corruptTooth = true;
		}
	}
}