/*using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ExpiryMode.Projectiles
{
    public class TestProj : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 14;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 560;
            drawOriginOffsetX -= 15;
            projectile.damage = 100;
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() - MathHelper.PiOver2;
            int iterate;
            for (iterate = 0; iterate < Main.maxNPCs; iterate++)
            {
                NPC npc = Main.npc[iterate];
                if (!npc.friendly && npc.CanBeChasedBy() && projectile.Distance(npc.Center) <= 1000f && Collision.CanHitLine(projectile.Center, 28, 6, npc.Center, npc.width, npc.height))
                {
                    projectile.velocity = (projectile.velocity + projectile.DirectionTo(npc.Center) * 100f) / 20f;
                } // TODO: Work on this later :byea:
            }
        }
    }
}*/