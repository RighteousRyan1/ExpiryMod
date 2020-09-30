using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using static Terraria.ModLoader.ModContent;
using ExpiryMode.Global_;
using ExpiryMode.Projectiles;
using System;
using ExpiryMode.Mod_;

namespace ExpiryMode.Items.Weapons.ExpiryExclusive
{
	public class SlimyBlunderbuss : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Slimy Blunderbuss");
			Tooltip.SetDefault("Shoots a plethorae of gels\nPushes you away from where you shoot\n'Ew. Sticky.'");
		}
        public override void SetDefaults()
        {
            item.rare = ExpiryRarity.Expiry;
            item.damage = 11;
            item.shoot = ProjectileType<BlueGel>();
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 50;
            item.useTime = 50;
            item.ranged = true;
            item.shootSpeed = 18f;
            item.scale = 1.5f;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Bang");
            item.noMelee = true;
            item.width = 60;
            item.height = 40;
            item.crit = 16;
        }
        public override void HoldItem(Player player)
        {
            if (player.GetModPlayer<InfiniteSuffPlayer>().igniter)
            {
                if (player.itemAnimation == 30) // NOTE: 30 frames from the animation ENDING
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
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 shootFrom = Vector2.Normalize(new Vector2(speedX, speedY)) * 20f;
            if (Collision.CanHit(position, 0, 0, position + shootFrom, 0, 0))
            {
                position += shootFrom;
            }
            int numberProjectiles = 4 + Main.rand.Next(5); // 4 or 5 shots
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX - (Main.rand.NextFloat() * 0.2f), speedY - (Main.rand.NextFloat() * 0.2f)).RotatedByRandom(MathHelper.ToRadians(20));
                Vector2 perturbedSpeed2 = new Vector2(speedX - (Main.rand.NextFloat() * 0.2f), speedY - (Main.rand.NextFloat() * 0.2f)).RotatedByRandom(MathHelper.ToRadians(20));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<PinkGel>(), damage, knockBack, player.whoAmI);
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed2.X, perturbedSpeed2.Y, ProjectileType<BlueGel>(), damage, knockBack, player.whoAmI); ;
            }
            if (!player.GetModPlayer<InfiniteSuffPlayer>().igniterNoVisual)
            {
                for (int push = 0; push < 13; push++)
                {
                    float rotation = Utils.ToRotation(new Vector2(Main.mouseX + Main.screenPosition.X, Main.mouseY + Main.screenPosition.Y) - player.position);
                    player.fallStart = (int)((player.fallStart * 9f + Utils.ToTileCoordinates(player.position).Y) / 10f);
                    player.velocity -= Utils.ToRotationVector2(rotation);
                }
            }
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-16, -3);
        }
    }
}
			
			