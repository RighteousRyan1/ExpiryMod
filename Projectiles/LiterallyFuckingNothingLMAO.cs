using Terraria.ModLoader;

namespace ExpiryMode.Projectiles
{
    public class LiterallyFuckingNothingLMAO : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 30;
        }
    }
}