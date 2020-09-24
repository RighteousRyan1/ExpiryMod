using Terraria;
using Terraria.ModLoader;
using ExpiryMode.Global_;
using ExpiryMode.Mod_;
using Terraria.ID;

namespace ExpiryMode.Items.Equippables.Accessories
{
	public class BrainBulwark: ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Brain Bulwark");
			Tooltip.SetDefault("Creates a field of knowledge around the player\nAny projectile that enters the field that is 35 damage or less is instantly discentegrated\nAny Enemy that walks in starts slowly losing life\nThe Brain of Cthulhu improves these abilities to 55 damage or less, and aura damage is increased\nAs a consequence, you take 5% more damage from other sources");
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
			player.endurance -= 0.05f;
            player.GetModPlayer<InfiniteSuffPlayer>().wearingForceField = true;
		}
	}
}