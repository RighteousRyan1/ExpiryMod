using Terraria;
using Terraria.ModLoader;
using ExpiryMode.Global_;
using ExpiryMode.Mod_;
using Terraria.ID;

namespace ExpiryMode.Items.Equippables.Accessories
{
	public class BumpStock : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Bump Stock");
			Tooltip.SetDefault("Allows for any gun to be used automatically\nIncreases all gun damage by 18%\nMakes all guns affected to fire 20% faster\nIncreases gun crit chance by 10%\n'I don't know how much longer I can hold this'");
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
			if (player.HeldItem.useAmmo == AmmoID.Bullet)
            {
                player.rangedCrit += 10;
				player.rangedDamageMult = 1.18f;
			}
            player.GetModPlayer<InfiniteSuffPlayer>().bumpStock = true;
		}
	}
}