using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ExpiryMode.Buffs.BadBuffs;
using Microsoft.Xna.Framework;
using ExpiryMode.Util;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Globalization;
using IL.Terraria.Utilities;
using Microsoft.Xna.Framework.Graphics;

namespace ExpiryMode.NPCs.Friendly
{
	public class BoxOfGoodies : ModNPC
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flying Supply Crate");
            Main.npcFrameCount[npc.type] = 6;
        }
        public override void SetDefaults()
        {
            npc.buffImmune[BuffType<RadiatedWater>()] = true;
            npc.width = 38;
            npc.height = 32;
            npc.defense = 2;
            npc.lifeMax = 175;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/BoxHit");
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/BoxBreak");
            npc.dontTakeDamageFromHostiles = true;
            npc.knockBackResist = 0f;
            npc.value = 0;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = numPlayers * 175;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.sky && Main.dayTime ? 0.2f : 0f;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            for (int proj = 0; proj < Main.maxProjectiles; proj++)
            {
                Projectile projectile = Main.projectile[proj];
                if (projectile.active && projectile.Distance(npc.Center) <= 20f)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        Dust.NewDust(npc.Center, 38, 32, 7, projectile.oldVelocity.X / 4, projectile.oldVelocity.Y / 4, 0, default, 1.1f);
                    }
                }
            }
            for (int i = 0; i < 9; i++)
            {
                Dust.NewDust(npc.Center, 38, 32, 7, 0, 0, 0, default, 1.1f);
            }
            if (npc.life <= 0)
            {
                int BoxGores1 = mod.GetGoreSlot("Gores/FriendlyGores/BoxGore1"); // My laziness is immesurable
                int BoxGores2 = mod.GetGoreSlot("Gores/FriendlyGores/BoxGore2");
                int BoxGores3 = mod.GetGoreSlot("Gores/FriendlyGores/BoxGore3");
                Gore.NewGore(npc.Center, npc.velocity, BoxGores1, npc.scale);
                Gore.NewGore(npc.Center, npc.velocity, BoxGores2, npc.scale);
                Gore.NewGore(npc.Center, npc.velocity, BoxGores3, npc.scale);
            }
        }
        /*public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (npc.life < npc.lifeMax)
            {
                spriteBatch.Draw(GetTexture("ExpiryMode/NPCs/Friendly/BoxOfGoodies2"), npc.Center - Main.screenPosition, npc.frame, drawColor, npc.rotation, npc.Size / 2f, npc.scale, SpriteEffects.None , 0);
                return false;
            }
            return base.PreDraw(spriteBatch, drawColor);
        }*/ // This MIGHT be used for a new animation, idk
        public override void UpdateLifeRegen(ref int damage)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0; // Set this here because we don't want a non-hostile NPC to be given regen, as it is not an aggressor
            }
            if (npc.HasBuff(BuffID.OnFire))
            {
                npc.lifeRegen = -32; // This box is made of wood, come on now...
            }
        }
        public override int SpawnNPC(int tileX, int tileY)
        {
            return base.SpawnNPC(tileX, tileY);
        }
        public override void AI()
        {
            if (npc.velocity.X > 0)
            {
                npc.spriteDirection = -1;
                npc.direction = -1;
            }
            if (npc.velocity.X < 0)
            {
                npc.spriteDirection = 1;
                npc.direction = 1;
            }
            //if (npc.life == npc.lifeMax)
            //{
            npc.ai[0]++;
            if (npc.ai[0] != 0)
            {
                npc.velocity.X = (float)Math.Sin(npc.ai[0] / 60f * -1);
                if (npc.velocity.X < -1)
                {
                    npc.velocity.X = -1;
                }
                npc.velocity.X = (float)Math.Sin(npc.ai[0] / 60f * 1);
                if (npc.velocity.X > 1)
                {
                    npc.velocity.X = 1;
                }
                npc.velocity.Y = (float)Math.Sin(npc.ai[0] / 10f * -1);
                if (npc.velocity.Y < -1)
                {
                    npc.velocity.Y = -1;
                }
                npc.velocity.Y = (float)Math.Sin(npc.ai[0] / 10f * 1);
                if (npc.velocity.Y > 1)
                {
                    npc.velocity.Y = 1;
                }
            }
            npc.ai[1]++;
            if (npc.ai[1] == 4)
            {
                npc.frame.Y += 38;
                npc.ai[1] = 0;
            }
            if (npc.frame.Y >= 228)
            {
                npc.frame.Y = 0;
            }
        }
        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), ItemID.CopperCoin, Main.rand.Next(1, 100));
            if (Main.rand.NextFloat() <= 0.6f)
            {
                Item.NewItem(npc.getRect(), ItemID.SilverCoin, Main.rand.Next(1, 51));
            }
            if (Main.rand.NextFloat() <= 0.3f)
            {
                Item.NewItem(npc.getRect(), ItemID.GoldCoin, Main.rand.Next(1, 26));
            }
            if (Main.rand.NextFloat() <= 0.075f)
            {
                Item.NewItem(npc.getRect(), ItemID.PlatinumCoin, Main.rand.Next(1, 6));
            }
            if (!Main.hardMode)
            {
                #region PRE-HM
                if (NPC.downedBoss3) // Skeletron
                {
                    if (Main.rand.NextFloat() <= 0.6f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.Meteorite, Main.rand.Next(20, 51));
                    }
                    if (Main.rand.NextFloat() <= 0.6f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.HealingPotion, Main.rand.Next(3, 10));
                    }
                    if (Main.rand.NextFloat() <= 0.1f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.WaterBolt);
                    }
                    else if (Main.rand.NextFloat() <= 0.1f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.Muramasa);
                    }
                    else if (Main.rand.NextFloat() <= 0.1f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.CobaltShield);
                    }
                    else if (Main.rand.NextFloat() <= 0.1f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.Valor);
                    }
                    else if (Main.rand.NextFloat() <= 0.1f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.AquaScepter);
                    }
                    else if (Main.rand.NextFloat() <= 0.1f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.Handgun);
                    }
                    else if (Main.rand.NextFloat() <= 0.1f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.MagicMissile);
                    }
                    else if (Main.rand.NextFloat() <= 0.1f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.BlueMoon);
                    }
                    else if (Main.rand.NextFloat() <= 0.1f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.ShadowKey);
                    }
                    else if (Main.rand.NextFloat() <= 0.1f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.BoneWelder);
                    }
                    else if (Main.rand.NextFloat() <= 0.1f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.GoldenKey, Main.rand.Next(1, 6));
                    }
                    else if (Main.rand.NextFloat() <= 0.1f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.TallyCounter);
                    }
                    else if (Main.rand.NextFloat() <= 0.1f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.Nazar);
                    }
                }
                if (NPC.downedBoss2) // BoC, EoW
                {
                    if (Main.rand.NextFloat() <= 0.6f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.Meteorite, Main.rand.Next(20, 51));
                    }
                    if (Main.rand.NextFloat() <= 0.4f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.LesserHealingPotion, Main.rand.Next(3, 15));
                    }
                    if (!WorldGen.crimson)
                    {
                        if (Main.rand.NextFloat() <= 0.6f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.DemoniteOre, Main.rand.Next(50, 91));
                        }
                        if (Main.rand.NextFloat() <= 0.1f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.BandofStarpower);
                        }
                        else if (Main.rand.NextFloat() <= 0.1f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.BallOHurt);
                        }
                        else if (Main.rand.NextFloat() <= 0.1f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.LightsBane);
                        }
                        else if (Main.rand.NextFloat() <= 0.1f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.Vilethorn);
                        }
                        else if (Main.rand.NextFloat() <= 0.1f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.ShadowOrb);
                        }
                        else if (Main.rand.NextFloat() <= 0.1f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.Musket);
                            Item.NewItem(npc.getRect(), ItemID.MusketBall, 100);
                        }
                        else if (Main.rand.NextFloat() <= 0.1f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.VilePowder);
                        }
                    }
                    if (WorldGen.crimson)
                    {
                        if (Main.rand.NextFloat() <= 0.6f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.CrimtaneOre, Main.rand.Next(20, 51));
                        }
                        if (Main.rand.NextFloat() <= 0.1f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.ViciousPowder);
                        }
                        else if (Main.rand.NextFloat() <= 0.1f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.TheRottedFork);
                        }
                        else if (Main.rand.NextFloat() <= 0.1f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.CrimsonRod);
                        }
                        else if (Main.rand.NextFloat() <= 0.1f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.PanicNecklace);
                        }
                        else if (Main.rand.NextFloat() <= 0.1f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.TheUndertaker);
                            Item.NewItem(npc.getRect(), ItemID.MusketBall, 100);
                        }
                        else if (Main.rand.NextFloat() <= 0.1f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.CrimsonHeart);
                        }
                        else if (Main.rand.NextFloat() <= 0.1f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.BloodButcherer);
                        }
                        else if (Main.rand.NextFloat() <= 0.1f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.TheMeatball);
                        }
                    }
                }
                if (NPC.downedBoss1) // EoC
                {
                    if (!WorldGen.crimson)
                    {
                        if (Main.rand.NextFloat() <= 0.6f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.DemoniteOre, Main.rand.Next(20, 51));
                        }
                    }
                    if (WorldGen.crimson)
                    {
                        if (Main.rand.NextFloat() <= 0.6f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.CrimtaneOre, Main.rand.Next(20, 51));
                        }
                    }
                }
            }
            #endregion
            #region HM
            if (Main.hardMode)
            {
                if (!NPC.downedMechBossAny) // Any mech boss is NOT downed
                {
                    if (Main.rand.NextFloat() <= 0.4f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.HealingPotion, Main.rand.Next(3, 15));
                    }
                    if (Main.rand.NextFloat() <= 0.2f)
                    {
                        if (Main.rand.NextFloat() <= 0.5f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.CobaltOre, Main.rand.Next(10, 31));
                        }
                        else
                        {
                            Item.NewItem(npc.getRect(), ItemID.PalladiumOre, Main.rand.Next(10, 31));
                        }
                    }
                    else if (Main.rand.NextFloat() <= 0.2f)
                    {
                        if (Main.rand.NextFloat() <= 0.5f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.MythrilOre, Main.rand.Next(5, 26));
                        }
                        else
                        {
                            Item.NewItem(npc.getRect(), ItemID.OrichalcumOre, Main.rand.Next(5, 26));
                        }
                    }
                    else if (Main.rand.NextFloat() <= 0.2f)
                    {
                        if (Main.rand.NextFloat() <= 0.5f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.AdamantiteOre, Main.rand.Next(5, 21));
                        }
                        else
                        {
                            Item.NewItem(npc.getRect(), ItemID.TitaniumOre, Main.rand.Next(5, 21));
                        }
                    }
                }
                if (NPC.downedMechBossAny && !NPC.downedPlantBoss) // any mech boss not downed
                {
                    if (Main.rand.NextFloat() <= 0.4f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.GreaterHealingPotion, Main.rand.Next(3, 11));
                    }
                    if (Main.rand.NextFloat() <= 0.2f)
                    {
                        if (Main.rand.NextFloat() <= 0.5f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.CobaltOre, Main.rand.Next(15, 36));
                        }
                        else
                        {
                            Item.NewItem(npc.getRect(), ItemID.PalladiumOre, Main.rand.Next(15, 36));
                        }
                    }
                    else if (Main.rand.NextFloat() <= 0.2f)
                    {
                        if (Main.rand.NextFloat() <= 0.5f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.MythrilOre, Main.rand.Next(10, 31));
                        }
                        else
                        {
                            Item.NewItem(npc.getRect(), ItemID.OrichalcumOre, Main.rand.Next(10, 31));
                        }
                    }
                    else if (Main.rand.NextFloat() <= 0.2f)
                    {
                        if (Main.rand.NextFloat() <= 0.5f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.AdamantiteOre, Main.rand.Next(10, 26));
                        }
                        else
                        {
                            Item.NewItem(npc.getRect(), ItemID.TitaniumOre, Main.rand.Next(10, 26));
                        }
                    }
                }
                if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3) // any mech boss not downed
                {
                    if (Main.rand.NextFloat() <= 0.2f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.ChlorophyteOre, Main.rand.Next(25, 51));
                    }
                }
                if (NPC.downedPlantBoss && !NPC.downedGolemBoss)
                {
                    if (Main.rand.NextFloat() <= 0.025f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.PiranhaGun);
                    }
                    else if (Main.rand.NextFloat() <= 0.025f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.ScourgeoftheCorruptor);
                    }
                    else if (!WorldGen.crimson && Main.rand.NextFloat() <= 0.025f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.ScourgeoftheCorruptor);
                    }
                    else if (WorldGen.crimson && Main.rand.NextFloat() <= 0.025f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.VampireKnives);
                    }
                    else if (Main.rand.NextFloat() <= 0.025f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.StaffoftheFrostHydra);
                    }
                    else if (Main.rand.NextFloat() <= 0.025f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.RainbowGun);
                    }
                    else if (Main.rand.NextFloat() <= 0.075)
                    {
                        Item.NewItem(npc.getRect(), ItemID.SniperRifle);
                    }
                    else if (Main.rand.NextFloat() <= 0.075)
                    {
                        Item.NewItem(npc.getRect(), ItemID.TacticalShotgun);
                    }
                    else if (Main.rand.NextFloat() <= 0.075)
                    {
                        Item.NewItem(npc.getRect(), ItemID.WispinaBottle);
                    }
                    else if (Main.rand.NextFloat() <= 0.075)
                    {
                        Item.NewItem(npc.getRect(), ItemID.Keybrand);
                    }
                    else if (Main.rand.NextFloat() <= 0.075)
                    {
                        Item.NewItem(npc.getRect(), ItemID.BlackBelt);
                    }
                    else if (Main.rand.NextFloat() <= 0.075)
                    {
                        Item.NewItem(npc.getRect(), ItemID.Kraken);
                    }
                    else if (Main.rand.NextFloat() <= 0.075)
                    {
                        Item.NewItem(npc.getRect(), ItemID.Tabi);
                    }
                    else if (Main.rand.NextFloat() <= 0.075)
                    {
                        Item.NewItem(npc.getRect(), ItemID.MagnetSphere);
                    }
                    else if (Main.rand.NextFloat() <= 0.075)
                    {
                        Item.NewItem(npc.getRect(), ItemID.Ectoplasm, Main.rand.Next(5, 11));
                    }
                    else if (Main.rand.NextFloat() <= 0.075)
                    {
                        Item.NewItem(npc.getRect(), ItemID.InfernoFork);
                    }
                    else if (Main.rand.NextFloat() <= 0.075)
                    {
                        Item.NewItem(npc.getRect(), ItemID.RocketLauncher);
                    }
                    else if (Main.rand.NextFloat() <= 0.075)
                    {
                        Item.NewItem(npc.getRect(), ItemID.SpectreStaff);
                    }
                    else if (Main.rand.NextFloat() <= 0.075)
                    {
                        Item.NewItem(npc.getRect(), ItemID.ShadowbeamStaff);
                    }
                    else if (Main.rand.NextFloat() <= 0.075)
                    {
                        Item.NewItem(npc.getRect(), ItemID.AdhesiveBandage);
                    }
                    else if (Main.rand.NextFloat() <= 0.075)
                    {
                        Item.NewItem(npc.getRect(), ItemID.ArmorPolish);
                    }
                    else if (Main.rand.NextFloat() <= 0.075)
                    {
                        Item.NewItem(npc.getRect(), ItemID.RifleScope);
                    }
                }
                if (NPC.downedGolemBoss && !NPC.downedMoonlord)
                {
                    if (Main.rand.NextFloat() <= 0.4f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.SuperHealingPotion, Main.rand.Next(3, 7));
                    }
                    if (Main.rand.NextFloat() <= 0.1f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.LihzahrdAltar);
                    }
                    else if (Main.rand.NextFloat() <= 0.1f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.LihzahrdFurnace);
                    }
                    else if (Main.rand.NextFloat() <= 0.04f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.SolarTablet);
                    }
                    else if (Main.rand.NextFloat() <= 0.1f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.LunarTabletFragment);
                    }
                    else if (Main.rand.NextFloat() <= 0.1f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.LihzahrdPowerCell, Main.rand.Next(1, 3));
                    }
                }
                if (NPC.downedMoonlord)
                {
                    if (Main.rand.NextFloat() <= 0.4f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.SuperHealingPotion, Main.rand.Next(3, 7));
                    }
                    if (Main.rand.NextFloat() <= 0.5f)
                    {
                        Item.NewItem(npc.getRect(), ItemID.LunarOre, Main.rand.Next(20, 41));
                    }
                    if (Main.rand.NextFloat() <= 0.3f)
                    {
                        if (Main.rand.NextFloat() <= 0.25f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.FragmentSolar, Main.rand.Next(10, 26));
                        }
                        else if (Main.rand.NextFloat() <= 0.25f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.FragmentNebula, Main.rand.Next(10, 26));
                        }
                        else if (Main.rand.NextFloat() <= 0.25f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.FragmentStardust, Main.rand.Next(10, 26));
                        }
                        else if (Main.rand.NextFloat() <= 0.25f)
                        {
                            Item.NewItem(npc.getRect(), ItemID.FragmentVortex, Main.rand.Next(10, 26));
                        }
                    }
                }
            }
            #endregion
            // Why did I waste my life doing this, anyways, it was worth it
        }
    }
}