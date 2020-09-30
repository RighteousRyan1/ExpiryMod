using Terraria.ModLoader;
using ExpiryMode.Global_;
using Terraria;
using Microsoft.Xna.Framework;
using ExpiryMode.Util;
using ExpiryMode.Mod_;

namespace ExpiryMode.Items.Equippables.Vanity.Prismatic
{
	[AutoloadEquip(EquipType.Body)]
	public class PrismaticVest : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prismatic Vest");
            Tooltip.SetDefault("'Great for impersonating Expiry Devs!'\n'Embrace the cool, be the cool.'");
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<InfiniteSuffPlayer>().accPrisBody = true;
        }
        public override void UpdateVanity(Player player, EquipType type)
        {
            player.GetModPlayer<InfiniteSuffPlayer>().accPrisBody = true;
            if (player.GetModPlayer<InfiniteSuffPlayer>().accPrisLegs && player.GetModPlayer<InfiniteSuffPlayer>().accPrisHead && player.GetModPlayer<InfiniteSuffPlayer>().accPrisBody)
            {
                Lighting.AddLight(player.Center, Main.DiscoColor.ToVector3() * 0.55f * Main.essScale);
            }
        }
        public override void SetDefaults() 
		{
			item.width = 18;
			item.height = 18;
			item.value = 12317;
			item.rare = ExpiryRarity.PrismaticRarity;
			item.vanity = true;
		}
	}
}