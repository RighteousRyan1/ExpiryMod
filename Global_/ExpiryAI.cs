using ExpiryMode.Buffs.MiscBuffs;
using ExpiryMode.Items.Equippables.Accessories;
using ExpiryMode.Items.Materials;
using ExpiryMode.Mod_;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System.Linq;
using System.Net;
using ExpiryMode.Util;
using System;
using ExpiryMode.Items.Useables;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ExpiryMode.Global_
{
    public class ExpiryAI : GlobalNPC
    {
        public override bool CloneNewInstances => true;
        public override bool InstancePerEntity => true;

        public int timer = 0;
        public int timer2 = 0;

        public int counterToCellSplit;
        public override void AI(NPC npc)
        {
            base.AI(npc);
            Player player = Main.player[Main.myPlayer];
            if (SuffWorld.ExpiryModeIsActive)
            {   
                if (npc.type == NPCID.Creeper)
                {
                    counterToCellSplit++;
                    if (npc.active)
                    {
                        if (npc.justHit)
                        {
                            counterToCellSplit -= 500;
                        }
                        if (counterToCellSplit == 3600)
                        {
                            if (Main.rand.NextFloat() <= 0.6f)
                            {
                                CombatText.NewText(npc.Hitbox, Color.Brown, "Split!");
                                Gore.NewGore(new Vector2(npc.Center.X, npc.Center.Y), Vector2.Zero, 402);
                                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.Creeper);
                                counterToCellSplit = 0;
                            }
                        }
                    }
                }
            }
            if (npc.FullName.Contains("Bat"))
            {
                if (SuffWorld.ExpiryModeIsActive)
                {
                    if (npc.collideX)
                    {
                        npc.velocity.X = npc.oldVelocity.X * -1.5f;
                        if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f)
                        {
                            npc.velocity.X = 3f;
                        }
                        if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f)
                        {
                            npc.velocity.X = -3f;
                        }
                    }
                    if (npc.collideY)
                    {
                        npc.velocity.Y = npc.oldVelocity.Y * -1.5f;
                        if (npc.velocity.Y > 0f && npc.velocity.Y < 1f)
                        {
                            npc.velocity.Y = 2f;
                        }
                        if (npc.velocity.Y < 0f && npc.velocity.Y > -1f)
                        {
                            npc.velocity.Y = -2f;
                        }
                    }
                }
                // Basically just you know, make bouncing even better :byeah:
            }
            List<int> Jellies = new List<int>
            {
                NPCID.PinkJellyfish,
                NPCID.BlueJellyfish,
                NPCID.GreenJellyfish,
                NPCID.BloodJelly,
            };
            if (SuffWorld.ExpiryModeIsActive)
            {
                if (Jellies.Contains(npc.type))
                {
                    bool shockMidDash = false;
                    if (npc.wet && npc.ai[1] == 1f)
                    {
                        shockMidDash = true;
                    }
                    else
                    {
                        npc.dontTakeDamage = false;
                    }
                    if (npc.wet)
                    {
                        if (npc.target >= 0 && Main.player[npc.target].wet && !Main.player[npc.target].dead && (Main.player[npc.target].Center - npc.Center).Length() < 300f)
                        {
                            if (npc.ai[1] == 0f)
                            {
                                npc.ai[2] += 3f;
                            }
                            else
                            {
                                npc.ai[2] -= 0.5f;
                            }
                        }
                        if (shockMidDash)
                        {
                            npc.dontTakeDamage = true;
                            npc.ai[2] += 1f;
                            if (npc.ai[2] >= 120f)
                            {
                                npc.ai[1] = 0f;
                            }
                        }
                        else
                        {
                            npc.ai[2] += 1f;
                            if (npc.ai[2] >= 240f)
                            {
                                npc.ai[1] = 1f;
                                npc.ai[2] = 0f;
                            }
                        }
                    }
                    else
                    {
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                    }
                }
            }
            if (npc.type == NPCID.GreenSlime ||
            npc.type == NPCID.BlueSlime ||
            npc.type == NPCID.PurpleSlime ||
            npc.type == NPCID.YellowSlime ||
            npc.type == NPCID.RedSlime ||
            npc.type == NPCID.BlackSlime ||
            npc.type == NPCID.Pinky ||
            npc.type == NPCID.MotherSlime ||
            npc.type == NPCID.BabySlime ||
            npc.type == NPCID.IceSlime ||
            npc.type == NPCID.SandSlime ||
            npc.type == NPCID.DungeonSlime ||
            npc.type == NPCID.UmbrellaSlime ||
            npc.type == NPCID.SlimeRibbonGreen ||
            npc.type == NPCID.SlimeRibbonRed ||
            npc.type == NPCID.SlimeRibbonYellow ||
            npc.type == NPCID.SlimeRibbonWhite ||
            npc.type == NPCID.SlimeMasked)
            {
                if (SuffWorld.ExpiryModeIsActive)
                {
                    if (!npc.wet && !Main.player[npc.target].npcTypeNoAggro[npc.type])
                    {
                        ;
                        if (npc.Distance(Main.player[npc.target].Center) < 300f && npc.velocity.Y == 0f)
                        {
                            npc.ai[0] = -1f;
                            if (npc.velocity.Y == 0f)
                            {
                                npc.velocity.X = npc.velocity.X * 1.2f;
                            }
                            if (Main.netMode != NetmodeID.MultiplayerClient && npc.localAI[0] == -1f)
                            {
                                for (int i = 0; i < 10; i++)
                                {
                                    Vector2 velocity = new Vector2(i - 4, -1f);
                                    npc.velocity.X *= 2.5f + Utils.NextFloat(Main.rand, -2f, 5f);
                                    npc.velocity.Y *= 2.5f + Utils.NextFloat(Main.rand, -1.5f, -0.5f);
                                    npc.velocity.Normalize();
                                    npc.velocity *= 2f + Utils.NextFloat(Main.rand, -1.5f, 0.75f);
                                    npc.localAI[0] = -1f;
                                }
                            }
                        }
                    }
                }
            }
            if (npc.type == NPCID.ChaosBall || npc.type == NPCID.WaterSphere || npc.type == NPCID.BurningSphere)
            {
                if (SuffWorld.ExpiryModeIsActive)
                {
                    npc.velocity.X = npc.velocity.X * 1.025f;
                    npc.velocity.Y = npc.velocity.Y * 1.025f;
                }
            }
            if (npc.type == NPCID.DarkCaster || npc.type == NPCID.DiabolistRed || npc.type == NPCID.DiabolistWhite || npc.type == NPCID.GoblinSorcerer || npc.type == NPCID.FireImp)
            {
                if (SuffWorld.ExpiryModeIsActive)
                {
                    if (npc.ai[0] == 0f)
                    {
                        npc.ai[0] = 1f;
                    }
                }
            }
            if (npc.aiStyle == NPCAIStyleID.Fighter)
            {
            }
            if (npc.FullName.Contains("Zombie"))
            {
                if (!player.GetModPlayer<InfiniteSuffPlayer>().NPC_AttractLocally && !player.GetModPlayer<InfiniteSuffPlayer>().NPC_RepulseLocally)
                {
                    if (SuffWorld.ExpiryModeIsActive)
                    {
                        if (npc.velocity.Y == 0)
                        {
                            if (npc.direction == 1)
                            {
                                if (npc.life >= npc.lifeMax / 2)
                                {
                                    npc.velocity.X++;
                                    if (npc.velocity.X > 2)
                                    {
                                        npc.velocity.X = 2;
                                    }
                                }
                                else if (npc.life < npc.lifeMax / 2)
                                {

                                    npc.velocity.X++;
                                    if (npc.velocity.X > 4)
                                    {
                                        npc.velocity.X = 4;
                                    }
                                }
                            }
                            if (npc.direction == -1)
                            {
                                if (npc.life >= npc.lifeMax / 2)
                                {
                                    npc.velocity.X--;
                                    if (npc.velocity.X < 2)
                                    {
                                        npc.velocity.X = -2;
                                    }
                                }
                                else if (npc.life < npc.lifeMax / 2)
                                {
                                    npc.velocity.X--;
                                    if (npc.velocity.X < 4)
                                    {
                                        npc.velocity.X = -4;
                                    }
                                }
                            }
                        }
                    }
                }
                if (npc.type == NPCID.Spazmatism)
                {
                    if (SuffWorld.ExpiryModeIsActive)
                    {
                        npc.ai[10]++;
                        if (npc.ai[10] == 200)
                        {
                            if (!player.dead)
                            {
                                Vector2 position = npc.Center;
                                float numberProjectiles = 6f;
                                float rotation = MathHelper.ToRadians(180f);
                                int i = 0;
                                while (i < numberProjectiles)
                                {
                                    Vector2 perturbedSpeed = Utils.RotatedBy(new Vector2(npc.velocity.X + Math.Abs(npc.velocity.X) + 25f, npc.velocity.Y + Math.Abs(npc.velocity.Y) + 25f), MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1f)), default) * 0.2f;
                                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.CursedFlameHostile, player.HeldItem.damage, 0, player.whoAmI, 0f, 0f);
                                    i++;
                                }
                            }
                            npc.ai[10] = 0;
                        }
                    }
                }
                if (npc.type == NPCID.Retinazer)
                {
                    if (SuffWorld.ExpiryModeIsActive)
                    {
                        npc.ai[10]++;
                        if (npc.ai[10] == 240)
                        {
                            if (!player.dead)
                            {
                                Vector2 position = npc.Center;
                                float numberProjectiles = 6f;
                                float rotation = MathHelper.ToRadians(180f);
                                int i = 0;
                                while (i < numberProjectiles)
                                {
                                    Vector2 perturbedSpeed = Utils.RotatedBy(new Vector2(npc.velocity.X + Math.Abs(npc.velocity.X) + 25f, npc.velocity.Y + Math.Abs(npc.velocity.Y) + 25f), MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1f)), default) * 0.2f;
                                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.EyeLaser, player.HeldItem.damage, 0, player.whoAmI, 0f, 0f);
                                    i++;
                                }
                            }
                            npc.ai[10] = 0;
                        }
                    }
                }
            }
            NetMessage.SendData(MessageID.SyncNPC, number: npc.whoAmI);
            if (player.GetModPlayer<InfiniteSuffPlayer>().NPC_RepulseLocally)
            {
                if (!npc.friendly && npc.CanBeChasedBy() && !npc.boss && (npc.type != NPCID.GolemFistLeft && npc.type != NPCID.SolarCrawltipedeBody && npc.type != NPCID.SolarCrawltipedeTail && npc.type != NPCID.WaterSphere && npc.type != NPCID.ChaosBall))
                {
                    if (!GetInstance<ExpiryModeMod>().MagnetRange_IsInfinite)
                    {
                        if (npc.Distance(Main.MouseWorld) <= 120f)
                        {
                            if (!GetInstance<ExpiryModeMod>().MagnetRange_IsInfinite)
                            {
                                if (player.direction == Math.Sign(npc.Center.X - player.Center.X))
                                {
                                    if (npc.Distance(Main.MouseWorld) <= 80f)
                                    {
                                        CombatText.NewText(npc.Hitbox, Color.Red, "-", true, true);
                                    }
                                    float rotation = Utils.ToRotation(new Vector2(Main.mouseX + Main.screenPosition.X, Main.mouseY + Main.screenPosition.Y) - npc.position);
                                    npc.velocity -= Utils.ToRotationVector2(rotation);
                                }
                            }
                        }
                    }
                    if (GetInstance<ExpiryModeMod>().MagnetRange_IsInfinite)
                    {
                        if (GetInstance<ExpiryModeMod>().MagnetRange_IsInfinite)
                        {
                            if (player.direction == Math.Sign(npc.Center.X - player.Center.X))
                            {
                                if (npc.Distance(Main.MouseWorld) <= 80f)
                                {
                                    CombatText.NewText(npc.Hitbox, Color.Red, "-", true, true);
                                }
                                float rotation = Utils.ToRotation(new Vector2(Main.mouseX + Main.screenPosition.X, Main.mouseY + Main.screenPosition.Y) - npc.position);
                                npc.velocity -= Utils.ToRotationVector2(rotation);
                            }
                        }
                    }
                }
            }
            if (player.GetModPlayer<InfiniteSuffPlayer>().NPC_AttractLocally)
            {
                if (!npc.friendly && npc.CanBeChasedBy() && !npc.boss && (npc.type != NPCID.GolemFistLeft && npc.type != NPCID.SolarCrawltipedeBody && npc.type != NPCID.SolarCrawltipedeTail && npc.type != NPCID.WaterSphere && npc.type != NPCID.ChaosBall))
                {
                    if (!GetInstance<ExpiryModeMod>().MagnetRange_IsInfinite)
                    {
                        if (npc.Distance(Main.MouseWorld) <= 120f)
                        {
                            if (player.direction == Math.Sign(npc.Center.X - player.Center.X))
                            {
                                if (npc.Distance(Main.MouseWorld) <= 80f)
                                {
                                    CombatText.NewText(npc.Hitbox, Color.LightBlue, "+", true, true);
                                }
                                float rotation = Utils.ToRotation(new Vector2(Main.mouseX + Main.screenPosition.X, Main.mouseY + Main.screenPosition.Y) - npc.position);
                                npc.velocity += Utils.ToRotationVector2(rotation);
                            }
                        }
                    }
                    else if (GetInstance<ExpiryModeMod>().MagnetRange_IsInfinite)
                    {
                        if (player.direction == Math.Sign(npc.Center.X - player.Center.X))
                        {
                            if (npc.Distance(Main.MouseWorld) <= 80f)
                            {
                                CombatText.NewText(npc.Hitbox, Color.LightBlue, "+", true, true);
                            }
                            float rotation = Utils.ToRotation(new Vector2(Main.mouseX + Main.screenPosition.X, Main.mouseY + Main.screenPosition.Y) - npc.position);
                            npc.velocity += Utils.ToRotationVector2(rotation);
                        }
                    }
                }
            }
        }
	}
}
