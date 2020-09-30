using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using static Terraria.ModLoader.ModContent;
using ExpiryMode.Global_;
using ExpiryMode.Projectiles;
using System;
using ExpiryMode.Mod_;
using Terraria.Localization;

namespace ExpiryMode.Items.Weapons.Guns
{
	public class Blunderbuss : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Blunderbuss");
			Tooltip.SetDefault("Shoots a cannonball\n'Heavy Gun calls for heavy duty'");
		}
        public override void SetDefaults()
        {
            item.rare = ItemRarityID.Orange;
            item.damage = 41;
            item.shoot = ProjectileID.CannonballFriendly;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 130;
            item.useTime = 130;
            item.ranged = true;
            item.shootSpeed = 22f;
            item.scale = 1.5f;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Bang");
            item.noMelee = true;
            item.width = 60;
            item.height = 40;
            item.crit = 26;
        }
        public override void HoldItem(Player player)
        {
            if (player.GetModPlayer<InfiniteSuffPlayer>().igniter)
            {
                if (player.itemAnimation == 30) // NOTE: 35 frames from the animation ENDING
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Reload1911"), player.position);
                }
            }
            if (!player.GetModPlayer<InfiniteSuffPlayer>().igniter)
            {
                if (player.itemAnimation == 30) // NOTE: 20 frames from the animation ENDING
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Reload1911"), player.position);
                }
            }
            if (player.velocity.X > 25)
            {
                CombatText.NewText(player.Hitbox, Color.Red, "Your Speed has been capped!", false, false);
                player.velocity.X = 25;
            }
            if (player.velocity.Y > 25)
            {
                CombatText.NewText(player.Hitbox, Color.Red, "Your Speed has been capped!", false, false);
                player.velocity.Y = 25;
            }
            if (player.velocity.X < -25)
            {
                CombatText.NewText(player.Hitbox, Color.Red, "Your Speed has been capped!", false, false);
                player.velocity.X = -25;
            }
            if (player.velocity.Y < -25)
            {
                CombatText.NewText(player.Hitbox, Color.Red, "Your Speed has been capped!", false, false);
                player.velocity.Y = -25;
            }
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-16, -3);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 shootFrom = Vector2.Normalize(new Vector2(speedX, speedY)) * -5f;
            if (Collision.CanHit(position, 0, 0, position + shootFrom, 0, 0))
            {
                position += shootFrom;
            }
            if (!player.GetModPlayer<InfiniteSuffPlayer>().igniterNoVisual)
            {
                for (int push = 0; push < 18; push++)
                {
                    float rotation = Utils.ToRotation(new Vector2(Main.mouseX + Main.screenPosition.X, Main.mouseY + Main.screenPosition.Y) - player.position);
                    player.fallStart = (int)((player.fallStart * 9f + Utils.ToTileCoordinates(player.position).Y) / 10f);
                    player.velocity -= Utils.ToRotationVector2(rotation);
                }
            }
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 20);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 16);
            recipe.AddIngredient(ItemID.GoldBar, 5);
            recipe.AddIngredient(ItemID.Spike, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
			
			