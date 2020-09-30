using Terraria;
using Terraria.ModLoader;
using ExpiryMode.Global_;
using ExpiryMode.Mod_;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using ExpiryMode.Util;
using System.Drawing.Imaging;

namespace ExpiryMode.Items.Equippables.Accessories
{
    public class SpiritsofLightAndDark : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Spirits of Light And Dark");
            Tooltip.SetDefault("In the 4 cardinal directions, you may fire a spirit of both light and dark at your enemies upon the use of a magic weapon\nThese projectiles are high damaging and pierce infnitely\n'The spirits of light and dark have awoken!'");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 8));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = 61283;
            item.accessory = true;
            item.rare = ExpiryRarity.Expiry;
        }
        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, ColorHelper.ColorSwitcher(ColorHelper.DarkPurple, Color.HotPink, 20f).ToVector3() * 1.1f * Main.essScale); //
        }
        public override void UpdateEquip(Player player) 
		{
            player.GetModPlayer<InfiniteSuffPlayer>().LAndD = true;
            // Light and Dark momento :)
		}
	}
}