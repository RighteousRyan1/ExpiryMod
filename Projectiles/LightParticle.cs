using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
namespace ExpiryMode.Projectiles
{ 
    public class LightParticle : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Light Particle");
        }
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 600;
            projectile.light = 0.5f;
            projectile.ignoreWater = false;
            projectile.damage = 36;
            projectile.penetrate = -1;
            projectile.knockBack = 2;
            projectile.arrow = true;
            drawOriginOffsetX += 15;
        }
        public override void AI()
        {
            Dust dust;
            dust = Dust.NewDustPerfect(projectile.Center, 71);
            dust.noGravity = true;
            projectile.rotation = projectile.velocity.ToRotation() - MathHelper.PiOver2;
            projectile.ai[0]++;
            if (projectile.ai[0] <= 15)
            {
                if (Math.Abs(projectile.velocity.Y) > Math.Abs(projectile.velocity.X))
                {
                    projectile.velocity.X--;
                    if (projectile.velocity.X < -5)
                    {
                        projectile.velocity.X = -5;
                    }
                }
                if (Math.Abs(projectile.velocity.Y) < Math.Abs(projectile.velocity.X))
                {
                    projectile.velocity.Y--;
                    if (projectile.velocity.Y < -5)
                    {
                        projectile.velocity.Y = -5;
                    }
                }
            }
            if (projectile.ai[0] > 15)
            {
                if (Math.Abs(projectile.velocity.Y) > Math.Abs(projectile.velocity.X))
                {
                    projectile.velocity.X++;
                    if (projectile.velocity.X > 5)
                    {
                        projectile.velocity.X = 5;
                    }
                }
                if (Math.Abs(projectile.velocity.Y) < Math.Abs(projectile.velocity.X))
                {
                    projectile.velocity.Y++;
                    if (projectile.velocity.Y > 5)
                    {
                        projectile.velocity.Y = 5;
                    }
                }
            }
            if (projectile.ai[0] == 40)
            {
                projectile.ai[0] = 0;
            }
        }
    }
}
