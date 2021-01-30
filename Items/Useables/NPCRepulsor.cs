using ExpiryMode.Global_;
using ExpiryMode.Mod_;
using ExpiryMode.Projectiles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace ExpiryMode.Items.Useables
{
	public class NPCRepulsor : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("NPC Magnet");
			Tooltip.SetDefault($"Left Click to attract NPCs to your mouse\nRight Click to repel NPCs from your mouse\nHold down both mice to VERY slowly damage any NPCs that are trapped\n'Can swap poles!'\n'Fun Fact: NPCs have a huge metal core''");
		}
        public override void SetDefaults()
        {
            Player player = Main.player[Main.myPlayer];
            item.autoReuse = true;
            item.damage = 0;
            item.shoot = ModContent.ProjectileType<LiterallyFuckingNothingLMAO>();
            item.shootSpeed = 3f;
            item.width = 32;
            item.height = 32;
            item.useAnimation = 1;
            item.useTime = 1;
            item.rare = ExpiryRarity.Expiry;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.channel = true;
            item.noMelee = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.autoReuse = true;
                item.damage = 0;
                item.shoot = ModContent.ProjectileType<LiterallyFuckingNothingLMAO>();
                item.shootSpeed = 3f;
                item.width = 32;
                item.height = 32;
                item.useAnimation = 1;
                item.useTime = 1;
                item.useStyle = ItemUseStyleID.HoldingOut;
                item.channel = true;
                item.noMelee = true;
                player.GetModPlayer<InfiniteSuffPlayer>().NPC_RepulseLocally = true;
                player.GetModPlayer<InfiniteSuffPlayer>().NPC_AttractLocally = false;
            }
            else
            {
                item.autoReuse = true;
                item.damage = 0;
                item.shoot = ModContent.ProjectileType<LiterallyFuckingNothingLMAO>();
                item.shootSpeed = 3f;
                item.width = 32;
                item.height = 32;
                item.useAnimation = 1;
                item.useTime = 1;
                item.useStyle = ItemUseStyleID.HoldingOut;
                item.noMelee = true;
                player.GetModPlayer<InfiniteSuffPlayer>().NPC_AttractLocally = true;
                player.GetModPlayer<InfiniteSuffPlayer>().NPC_RepulseLocally = false;
                item.channel = true;
            }
            if (Main.mouseRight && Main.mouseLeft)
            {
                player.GetModPlayer<InfiniteSuffPlayer>().NPC_DamageLocally = true;
            }
            return base.CanUseItem(player);
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
    }
}