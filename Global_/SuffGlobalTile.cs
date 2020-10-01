using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ExpiryMode.Items.Materials;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ExpiryMode.Global_
{
	public class SuffGlobalTile : GlobalTile
	{
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            #region Top-Tier
            if (!fail && type == TileID.Gold)
            {
                if (Main.rand.NextFloat() <= 0.045f) // Kinda common? Eh, idk.
                {
                    Item.NewItem(new Vector2(i, j).ToWorldCoordinates(), ItemID.GoldCoin, Main.rand.Next(1, 6));
                }
            }
            if (!fail && type == TileID.Platinum)
            {
                if (Main.rand.NextFloat() <= 0.002f) // 1/500 chance, rare!
                {
                    Item.NewItem(new Vector2(i, j).ToWorldCoordinates(), ItemID.PlatinumCoin, Main.rand.Next(1, 3));
                }
            }
            #endregion
            #region Mid-Tier
            if (!fail && (type == TileID.Tungsten || type == TileID.Silver))
            {
                if (Main.rand.NextFloat() <= 0.1) // 1/10 chance. Common
                {
                    Item.NewItem(new Vector2(i, j).ToWorldCoordinates(), ItemID.SilverCoin, Main.rand.Next(1, 26));
                }
            }
            #endregion
            #region Bottom-Tier
            if (!fail && (type == TileID.Tin || type == TileID.Copper))
            {
                if (Main.rand.NextFloat() <= 0.5) // 1/2 chance. Really Common
                {
                    Item.NewItem(new Vector2(i, j).ToWorldCoordinates(), ItemID.CopperCoin, Main.rand.Next(1, 51));
                    //Vector2.Distance(Main.player[Main.myPlayer].position, Main.projectile[200].position); // Mental note
                }
            }
            #endregion
            base.KillTile(i, j, type, ref fail, ref effectOnly, ref noItem);
        }
        /*public override int[] AdjTiles(int type)
        {
            Player player = Main.LocalPlayer;
            List<int> tiles = new List<int>();
            if (player.HasItem(ItemType<PocketWorkBenches>()))
                tiles.Add(TileID.WorkBenches);
            return tiles.ToArray();
        }*/
    }
}
