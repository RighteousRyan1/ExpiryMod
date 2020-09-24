using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ExpiryMode.Projectiles
{
    public class BlueGel : ModProjectile
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
                projectile.Kill();
                Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 0, 1, 0);
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.Center;
                for (int i = 0; i < 12; i++)
                {
                    dust = Main.dust[Terraria.Dust.NewDust(position, 30, 30, 176, 0f, 0f, 191, new Color(0, 92, 255), 1f)];
                }

            }
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.Kill();
            Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 0, 1, 0);
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            for (int i = 0; i < 8; i++)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.Center;
                dust = Main.dust[Terraria.Dust.NewDust(position, 30, 30, 176, 0f, 0f, 191, new Color(0, 92, 255), 1f)];
            }

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