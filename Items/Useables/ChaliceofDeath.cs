using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using ExpiryMode.Mod_;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace ExpiryMode.Items.Useables
{
	public class ChaliceofDeath : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chalice of Demise");
            Tooltip.SetDefault("HahaFunny");
            //Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 7)); // Note: TicksPerFrame, Frames
        }
        /// <summary>
        /// Where the gayass properties are set.
        /// </summary>
        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;
            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item119;
            item.autoReuse = false;
            //item.noUseGraphic = true;
            //item.shoot = ProjectileType<Projectiles.ChaliceOfDeath>();  
        }
        /*public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            //return spriteBatch.Draw(GetTexture("ExpiryMode/Items/Useables/ChaliceofDeathAnim"), )
            Texture2D texture = Main.itemTexture[item.type];
            for (int i = 0; i < 4; i++)
            {
                Vector2 offsetPositon = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * 2;
                spriteBatch.Draw(GetTexture("ExpiryMode/Items/Useables/ChaliceofDeathAnim"), position, frame, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            }
            return true;
        }*/
        public override bool UseItem(Player player)
        {
            if (Main.npc.Any(n => n.active && n.boss))
            {
                player.KillMe(PlayerDeathReason.ByCustomReason($"{player.name} tried to cheat. What a scumbag."), player.statLifeMax2 + 200, 0, false);
                return false;
            }
            if (SuffWorld.ExpiryModeIsActive)
            {
                SuffWorld.ExpiryModeIsActive = false;
                if (Main.netMode == NetmodeID.Server)
                {
                    SuffWorld.ExpiryModeIsActive = false;
                    NetMessage.SendData(MessageID.WorldData);
                }
                Main.NewText($"Expiry Mode has been disabled by {player.name}. What a wimp.", Color.DarkOrange, true);
                return true;
            }
            if (!SuffWorld.ExpiryModeIsActive)
            {
                SuffWorld.ExpiryModeIsActive = true;
                if (Main.netMode == NetmodeID.Server)
                {
                    SuffWorld.ExpiryModeIsActive = true;
                    NetMessage.SendData(MessageID.WorldData);
                }
                Main.NewText($"Expiry Mode has been enabled. {player.name} is a real man.", Color.DarkOrange, true);
                return true;
            }
            return base.UseItem(player);
        }
		public override bool CanUseItem(Player player)
		{
            return Main.expertMode;
        }
	}
}