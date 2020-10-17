using Terraria;
using Terraria.ModLoader;
using ExpiryMode.Global_;
using ExpiryMode.Mod_;
using Terraria.ID;
using System.Collections.Generic;

namespace ExpiryMode.Items.Equippables.Accessories
{
	public class TheWormsTooth : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Player player = Main.player[Main.myPlayer];
			DisplayName.SetDefault("Corrupt Tooth");
			Tooltip.SetDefault($"Allows for the random chance of generating a circle of cursed flame balls upon hit of an enemy\nMelee strikes generate more cursed flames");
		}
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 12317;
            item.accessory = true;
            item.rare = ExpiryRarity.Expiry;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[Main.myPlayer];
            tooltips.Add(new TooltipLine(mod, "Yes", "Your Name")
            {
                text = $"'Come on {player.name}! You just HAD to take its tooth.'"
            });
            base.ModifyTooltips(tooltips);
        }
        public override void UpdateEquip(Player player) 
		{
			player.GetModPlayer<InfiniteSuffPlayer>().corruptTooth = true;
		}
	}
}