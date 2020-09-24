using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpiryMode.Buffs.BadBuffs
{
    public class Fleshy : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Extra Bloody");
            Description.SetDefault("Monsters want to eat you more\nYou cannot regenerate life\nSome reduced stats");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegenCount = 0;
			player.aggro += 150;
            player.statDefense -= 2;
            player.bleed = true;
            player.statLifeMax2 = (int)(player.statLifeMax2 * 0.95f);
            if (Main.GameUpdateCount % 20 == 0)
            {
                Dust.NewDust(player.Top, 5, 5, DustID.Blood, player.velocity.X - 5f, player.velocity.Y - 5f, 0, Color.DarkRed, 1);
            }
        }
    }
}