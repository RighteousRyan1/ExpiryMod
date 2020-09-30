using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ExpiryMode.Mod_;
using log4net.Util;
using System.Security.Policy;

namespace ExpiryMode.Global_
{
    public class ProjectileAI : GlobalProjectile
    {
        public override void AI(Projectile projectile)
        {
            /*base.AI(projectile);
            if (projectile.type == ProjectileID.WoodenArrowFriendly)
            {
                projectile.velocity = (projectile.velocity * 19f + (projectile.DirectionTo(Main.player[Main.myPlayer].Center) + new Vector2(0, -20)) * 20f) / 20f;
            }*/ // Experimental shits yes?
        }
    }
}