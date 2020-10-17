using Terraria.DataStructures;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;
using ExpiryMode.Mod_;
using System;

namespace ExpiryMode.Mod_
{
    // AWhatever - Inverse of tan, sin, cos (get angle of square root of tan, sin, cos)

    public class DeathCountPlayer : ModPlayer
    {
        public int playerDeathCount;
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            DelayedSync(toWho, fromWho, newPlayer);
        }
        private void DelayedSync(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket pack = mod.GetPacket();
            pack.Write((byte)DeathCountMessageType.PlayerSync);
            pack.Write(player.whoAmI);
            pack.Write(playerDeathCount);
            pack.Send(toWho, fromWho);
        }

        /*public override void OnEnterWorld(Player player)
        {
            ModPacket pack = mod.GetPacket();
            pack.Write((byte)DeathCountMessageType.PlayerSync);
            pack.Write(this.player.whoAmI);
            pack.Write(playerDeathCount);
            pack.Send(-1, this.player.whoAmI);
        }*/
        private double numUpdates = -600;

        public override void PreUpdate()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient && numUpdates % 3600 == 0 && player.whoAmI == Main.LocalPlayer.whoAmI)
            {
                ModPacket pack = mod.GetPacket();
                pack.Write((byte)DeathCountMessageType.PlayerValueChange);
                pack.Write(player.whoAmI);
                pack.Write(playerDeathCount);
                pack.Send(-1, player.whoAmI);
            }
            numUpdates++;
            base.PreUpdate();
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            if (Main.LocalPlayer.whoAmI == player.whoAmI && Main.netMode == NetmodeID.MultiplayerClient)
            {
                playerDeathCount++;
                ModPacket pack = mod.GetPacket();
                pack.Write((byte)DeathCountMessageType.PlayerValueChange);
                pack.Write(player.whoAmI);
                pack.Write(playerDeathCount);
                pack.Send(-1, player.whoAmI);
            }
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                playerDeathCount++;
            }

            if (Main.netMode != NetmodeID.Server)
            {
                int deaths = playerDeathCount;
                Color col = Colors.CoinGold;
                if (ModContent.GetInstance<ExpiryConfigServerSide>().trackDeathCounter)
                {
                    Main.NewText($"{player.name} has died {deaths} times this session!", Color.BlanchedAlmond);
                }
            }
        }
    }
}