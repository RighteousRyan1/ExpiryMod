using ExpiryMode.Global_;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using ExpiryMode.Mod_;
using Terraria.ID;

namespace ExpiryMode.Items.Equippables.Vanity.Prismatic
{
	[AutoloadEquip(EquipType.Head)]
	public class PrismaticDome : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prismatic Dome");
            Tooltip.SetDefault("'Great for impersonating Expiry Devs!'\n'Take it in, don't let it out.'");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 12342;
            // item.rare = ExpiryRarity.PrismaticRarity;
			item.rare = ItemRarityID.Expert;
            item.vanity = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<InfiniteSuffPlayer>().accPrisHead = true;
        }
        public override void UpdateVanity(Player player, EquipType type)
        {
            player.GetModPlayer<InfiniteSuffPlayer>().accPrisHead = true;
        }
        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = false;
            drawAltHair = false;
        }
	}
}