using ExpiryMode.Items.Blocks;
using ExpiryMode.Tiles;
using ExpiryMode.Tiles.Decoration;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
 
namespace ExpiryMode.Items.Decoration
{
    public class BiohazardContainer : ModItem
    {
		public int timerBeforeExplodeIntoDeadlyRadiation;
		public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Labeled: 'Extremely Dangerous'\nDEVNOTE: Tile does not properly drop.");
        }
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 14;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = 150;
            item.createTile = TileType<BiohazardBarrel>();
        }
        public override void PostUpdate()
        {
            if (item.velocity.Y > 2)
            {
                timerBeforeExplodeIntoDeadlyRadiation++;
            }
            if (timerBeforeExplodeIntoDeadlyRadiation > 120 && item.velocity.Y == 0)
            {
				timerBeforeExplodeIntoDeadlyRadiation = 0;
                RainExplosionDeathThatIsCertainForPeopleWhoAreNearTheItem_Therefore_YouJustFuckingDie();
                item.TurnToAir(); // FIXME: This probably isnt working right
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronBar, 5);
            recipe.AddIngredient(ItemID.YellowPaint, 1);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        private void RainExplosionDeathThatIsCertainForPeopleWhoAreNearTheItem_Therefore_YouJustFuckingDie()
        {
            Main.PlaySound(SoundID.Item14, base.item.position);
            for (int i = 0; i < 50; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(item.position.X, item.position.Y), item.width, item.height, 31, 0f, 0f, 100, default, 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }
            for (int j = 0; j < 80; j++)
            {
                int dustIndex2 = Dust.NewDust(new Vector2(base.item.position.X, base.item.position.Y), base.item.width, item.height, 6, 0f, 0f, 100, default, 3f);
                Main.dust[dustIndex2].noGravity = true;
                Main.dust[dustIndex2].velocity *= 5f;
                dustIndex2 = Dust.NewDust(new Vector2(base.item.position.X, base.item.position.Y), base.item.width, base.item.height, 6, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex2].velocity *= 3f;
            }
            for (int g = 0; g < 2; g++)
            {
                int goreIndex = Gore.NewGore(new Vector2(base.item.position.X + (float)(base.item.width / 2) - 24f, base.item.position.Y + (float)(base.item.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(new Vector2(base.item.position.X + (float)(base.item.width / 2) - 24f, base.item.position.Y + (float)(base.item.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(new Vector2(base.item.position.X + (float)(base.item.width / 2) - 24f, base.item.position.Y + (float)(base.item.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                goreIndex = Gore.NewGore(new Vector2(base.item.position.X + (float)(base.item.width / 2) - 24f, base.item.position.Y + (float)(base.item.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
            }
            base.item.position.X = base.item.position.X + (float)(base.item.width / 2);
            base.item.position.Y = base.item.position.Y + (float)(base.item.height / 2);
            base.item.width = 10;
            base.item.height = 10;
            base.item.position.X = base.item.position.X - (float)(base.item.width / 2);
            base.item.position.Y = base.item.position.Y - (float)(base.item.height / 2);
            int explosionRadius = 25;   
            int minTileX = (int)(base.item.position.X / 16f - (float)explosionRadius);
            int maxTileX = (int)(base.item.position.X / 16f + (float)explosionRadius);
            int minTileY = (int)(base.item.position.Y / 16f - (float)explosionRadius);
            int maxTileY = (int)(base.item.position.Y / 16f + (float)explosionRadius);
            bool flag = minTileX < 0;
            if (flag)
            {
                minTileX = 0;
            }
            bool flag2 = maxTileX > Main.maxTilesX;
            if (flag2)
            {
                maxTileX = Main.maxTilesX;
            }
            bool flag3 = minTileY < 0;
            if (flag3)
            {
                minTileY = 0;
            }
            bool flag4 = maxTileY > Main.maxTilesY;
            if (flag4)
            {
                maxTileY = Main.maxTilesY;
            }
            bool canKillWalls = false;
            for (int x = minTileX; x <= maxTileX; x++)
            {
                for (int y = minTileY; y <= maxTileY; y++)
                {
                    float diffX = Math.Abs((float)x - base.item.position.X / 16f);
                    float diffY = Math.Abs((float)y - base.item.position.Y / 16f);
                    double distance = Math.Sqrt((double)(diffX * diffX + diffY * diffY));
                    bool flag5 = distance < (double)explosionRadius && Main.tile[x, y] != null && Main.tile[x, y].wall == 0;
                    if (flag5)
                    {
                        canKillWalls = true;
                        break;
                    }
                }
            }
            for (int m = minTileX; m <= maxTileX; m++)
            {
                for (int n = minTileY; n <= maxTileY; n++)
                {
                    float diffX3 = Math.Abs((float)m - base.item.position.X / 16f);
                    float diffY3 = Math.Abs((float)n - base.item.position.Y / 16f);
                    double distanceToTile2 = Math.Sqrt((double)(diffX3 * diffX3 + diffY3 * diffY3));
                    bool flag7 = distanceToTile2 < (double)explosionRadius;
                    if (flag7)
                    {
                        bool canKillTile = true;
                        bool flag8 = Main.tile[m, n] != null && Main.tile[m, n].active();
                        if (flag8)
                        {
                            canKillTile = true;
                            bool flag9 = Main.tileDungeon[(int)Main.tile[m, n].type] || Main.tile[m, n].type == 88 || Main.tile[m, n].type == 21 || Main.tile[m, n].type == 26 || Main.tile[m, n].type == 107 || Main.tile[m, n].type == 108 || Main.tile[m, n].type == 111 || Main.tile[m, n].type == 226 || Main.tile[m, n].type == 237 || Main.tile[m, n].type == 221 || Main.tile[m, n].type == 222 || Main.tile[m, n].type == 223 || Main.tile[m, n].type == 211 || Main.tile[m, n].type == 404;
                            if (flag9)
                            {
                                canKillTile = false;
                            }
                            bool flag10 = !Main.hardMode && Main.tile[m, n].type == 58;
                            if (flag10)
                            {
                                canKillTile = false;
                            }
                            bool flag11 = !TileLoader.CanExplode(m, n);
                            if (flag11)
                            {
                                canKillTile = false;
                            }
                            bool flag12 = canKillTile;
                            if (flag12)
                            {
                                WorldGen.KillTile(m, n, false, false, false);
                                bool flag13 = !Main.tile[m, n].active() && Main.netMode != NetmodeID.SinglePlayer;
                                if (flag13)
                                {
                                    NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, (float)m, (float)n, 0f, 0, 0, 0);
                                }
                            }
                        }
                        bool flag14 = canKillTile;
                        if (flag14)
                        {
                            for (int x2 = m - 1; x2 <= m + 1; x2++)
                            {
                                for (int y2 = n - 1; y2 <= n + 1; y2++)
                                {
                                    bool flag15 = Main.tile[x2, y2] != null && Main.tile[x2, y2].wall > 0 && canKillWalls && WallLoader.CanExplode(x2, y2, (int)Main.tile[x2, y2].wall);
                                    if (flag15)
                                    {
                                        WorldGen.KillWall(x2, y2, false);
                                        bool flag16 = Main.tile[x2, y2].wall == 0 && Main.netMode != NetmodeID.SinglePlayer;
                                        if (flag16)
                                        {
                                            NetMessage.SendData(MessageID.TileChange, -1, -1, null, 2, (float)x2, (float)y2, 0f, 0, 0, 0);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}