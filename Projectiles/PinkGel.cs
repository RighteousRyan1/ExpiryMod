using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ExpiryMode.Projectiles
{
    public class PinkGel : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 14;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = true;
            projectile.timeLeft = 99999;
            projectile.light = 0f;
            projectile.ignoreWater = false;
            projectile.damage = 7;
            projectile.penetrate = 1;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.penetrate--;
            if (projectile.penetrate <= 0)
            {
                Dust dust;
                projectile.Kill();
                Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 0, 1, 0);
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.Center;
                for (int i = 0; i < 3; i++)
                {
                    dust = Main.dust[Terraria.Dust.NewDust(position, 30, 30, 100, 0f, 0f, 0, new Color(255, 0, 176), 1f)];
                    dust.noLight = true;
                    dust.fadeIn = 1.342105f;
                }
                for (int i = 0; i < 6; i++)
                {
                    dust = Main.dust[Terraria.Dust.NewDust(position, 30, 30, 100, oldVelocity.X - 5f, oldVelocity.Y - 5f, 0, new Color(255, 0, 176), 1f)];
                    dust.noLight = true;
                    dust.fadeIn = 1.342105f;
                }
            }
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.Kill();
            Dust dust;
            Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 0, 1, 0);
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            Vector2 position = projectile.Center;
            for (int i = 0; i < 3; i++)
            {
                dust = Main.dust[Terraria.Dust.NewDust(position, 30, 30, 100, 0f, 0f, 0, new Color(255, 0, 176), 1f)];
                dust.noLight = true;
                dust.fadeIn = 1.342105f;
            }
            for (int i = 0; i < 6; i++)
            {
                dust = Main.dust[Terraria.Dust.NewDust(position, 30, 30, 100, projectile.oldVelocity.X - 5f, projectile.oldVelocity.Y - 5f, 0, new Color(255, 0, 176), 1f)];
                dust.noLight = true;
                dust.fadeIn = 1.342105f;
            }
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override void AI()
        {
            // projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
            projectile.rotation = projectile.velocity.Y + projectile.velocity.X + 2f;
            projectile.velocity.Y = projectile.velocity.Y + 0.1f; // 0.1f for arrow gravity, 0.4f for knife gravity
            if (projectile.velocity.Y > 16f) // This check implements "terminal velocity". We don't want the projectile to keep getting faster and faster. Past 16f this projectile will travel through blocks, so this check is useful.
            {
                projectile.velocity.Y = 16f;
            }
        }
    }
}
