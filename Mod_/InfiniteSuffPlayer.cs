using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader;
using ExpiryMode.Buffs.BadBuffs;
using ExpiryMode.Buffs.GoodBuffs;
using System.Collections.Generic;
using ExpiryMode.Items.Materials;
using Terraria.GameInput;
using ExpiryMode.Items.Useables;
using ExpiryMode.Items.Fish.Quest;
using System.Linq;
using ExpiryMode.Util;
using ExpiryMode.Global_;
using ExpiryMode.Items.Weapons.ExpiryExclusive;
using ExpiryMode.Items.Weapons.Guns;
using Microsoft.Xna.Framework;
using System.Runtime.CompilerServices;
using IL.Terraria.Utilities;
using System;
using ExpiryMode.NPCs.Friendly;

namespace ExpiryMode.Mod_
{
    public class InfiniteSuffPlayer : ModPlayer
    {
        /// <summary>
        /// Whether the player pressed "Guns" in the War Veteran UI
        /// </summary>
        public bool GunShopActive = false;
        /// <summary>
        /// Whether the player has > 200 musket balls
        /// </summary>
        public bool hasVeteranMoveInRequirement = false;
        /// <summary>
        /// Light And Dark accessory?
        /// </summary>
        public bool LAndD = false;
        /// <summary>
        /// Hold both mice down to damage enemies inside of the field.
        /// </summary>
        public bool NPC_DamageLocally = false;
        /// <summary>
        /// Are you using the magnet?
        /// </summary>
        public bool MagnetActive = false;
        /// <summary>
        /// Attracts enemies from a set distance
        /// </summary>
        public bool NPC_AttractLocally = false;
        /// <summary>
        /// Repulses enemies from a set distance
        /// </summary>
        public bool NPC_RepulseLocally = false;
        /// <summary>
        /// Wearing the Corrupt Tooth
        /// </summary>
        public bool corruptTooth = false;
        /// <summary>
        /// Wearing Brain Bulwark
        /// </summary>
        public bool inForceField = false;
        /// <summary>
        /// Wearing force field?
        /// </summary>
        public bool wearingForceField = false;
        /// <summary>
        /// Igniter is equipped
        /// </summary>
        public bool igniter = false;
        /// <summary>
        /// Igniter, but hideVisual is true
        /// </summary>
        public bool igniterNoVisual = false;
        /// <summary>
        /// Prime Utils is equipped
        /// </summary>
        public bool primeUtils = false;
        /// <summary>
        /// Determines whether the Prismatic Head is equipped
        /// </summary>
        public bool accPrisHead = false;
        /// <summary>
        /// Determines whether the Prismatic Body is equipped
        /// </summary>
        public bool accPrisBody = false;
        /// <summary>
        /// Determines whether the Prismatic Legs is equipped
        /// </summary>
        public bool accPrisLegs = false;
        /// <summary>
        /// The # of Radiated Gravel
        /// </summary>
        public int DoomBlockCount = 0;
        /// <summary>
        /// Checks if the player is in the Radiation biome
        /// </summary>
        public bool ZoneRadiated = false;
        /// <summary>
        /// Checks if Expiry Mode is active
        /// </summary>
        public bool ExpiryModeIsActive = false;
        /// <summary>
        /// Checks if the player has the Bump Stock on their player/equipped
        /// </summary>
        public bool bumpStock = false;
        /// <summary>
        /// checks if the mechanical Scarf is equipped
        /// </summary>
        public bool mechScarf = false;
        /// <summary>
        /// Determines whether this item is a gun
        /// </summary>
        public bool isGun;
        public override void UpdateLifeRegen()
        {
        }
        public override void UpdateBadLifeRegen()
        {
            if (MagnetActive)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegen -= 2 * 5;
            }
        }
        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            return base.Shoot(item, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
        public override void ResetEffects()
        {
            bumpStock = false;
            mechScarf = false;
            primeUtils = false;
            igniter = false;
            igniterNoVisual = false;
            wearingForceField = false;
            corruptTooth = false;
            NPC_RepulseLocally = false;
            MagnetActive = false;
            NPC_AttractLocally = false;
            NPC_DamageLocally = false;
            LAndD = false;
            hasVeteranMoveInRequirement = false;
            GunShopActive = false;
        }
        public override bool PreItemCheck()
        {
            Item item = player.HeldItem;
            if (item.useAmmo == AmmoID.Bullet && player.HeldItem.type > ItemID.None)
            {
                isGun = true;
            }
            if (bumpStock && item.useAmmo == AmmoID.Bullet && !item.autoReuse && player.HeldItem.type > ItemID.None)
            {
                item.autoReuse = true;
            }
            else if (!bumpStock && player.HeldItem.type > ItemID.None)
            {
                player.HeldItem.autoReuse = player.HeldItem.GetGlobalItem<OnTerrariaHook>().defAutoReuse;
            }
            if (igniter && player.HeldItem.type > ItemID.None)
            {
                if (item.type == ItemType<SlimyBlunderbuss>())
                {
                    item.useAnimation = 30;
                    item.useTime = 30;
                }
            }
            if (!igniter)
            {
                if (item.type == ItemType<SlimyBlunderbuss>())
                {
                    item.useAnimation = 50;
                    item.useTime = 50;
                }
            }
            if (igniter)
            {
                if (item.type == ItemType<Blunderbuss>())
                {
                    item.useAnimation = 85;
                    item.useTime = 85;
                }
			}
            else if (!igniter)
            {
                //player.HeldItem.reuseDelay = player.HeldItem.GetGlobalItem<OnTerrariaHook>().defReuseDelayInt;
                if (item.type == ItemType<Blunderbuss>())
                {
                    item.useAnimation = 110;
                    item.useTime = 110;
                }

            }
            return base.PreItemCheck();
        }
        public override float UseTimeMultiplier(Item item)
        {
            if (bumpStock && item.useAmmo == AmmoID.Bullet && !item.autoReuse)
            {
                return 0.8f;
            }
            if (igniter && item.type == ItemType<SlimyBlunderbuss>())
            {
                return 0.6f;
            }
            return 1f;
        }
        public static long GetSavings(Player player)
        {
            long inv = Utils.CoinsCount(out _, player.inventory, new int[]
            {
                58, //Mouse item
                57, //Ammo slots
                56,
                55,
                54
            });
            int[] empty = new int[0];
            long piggy = Utils.CoinsCount(out _, player.bank.item, empty);
            long safe = Utils.CoinsCount(out _, player.bank2.item, empty);
            long forge = Utils.CoinsCount(out _, player.bank3.item, empty);
            return Utils.CoinsCombineStacks(out _, new long[]
            {
                inv,
                piggy,
                safe,
                forge
            });
        }
        public override void ProcessTriggers(TriggersSet triggersSet) { }
        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff) { }
        public override void UpdateBiomeVisuals()
        {
            player.ManageSpecialBiomeVisuals("InfniteSuffering:RadiatedBiomeSky", ZoneRadiated);
        }
        public override void ModifyNursePrice(NPC nurse, int health, bool removeDebuffs, ref int price)
        {
            if (SuffWorld.ExpiryModeIsActive)
            {
                if (!Main.expertMode && !Main.hardMode)
                {
                    price = (int)(price * 5f);
                    removeDebuffs = true;
                }
                else if (Main.expertMode && !Main.hardMode)
                {
                    price = (int)(price * 10f);
                    removeDebuffs = false;
                }
                else if (!Main.expertMode && Main.hardMode)
                {
                    price = (int)(price * 15f);
                    removeDebuffs = true;
                }
                else if (Main.expertMode && Main.hardMode)
                {
                    price = (int)(price * 25f);
                    removeDebuffs = false;
                }
            }
        }
        public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
        {
            Item item1 = new Item();
            item1.SetDefaults(ItemType<PendantPiece>(), false);
            items.Add(item1);
            Item item2 = new Item();
            item2.SetDefaults(ItemType<ChaliceofDeath>(), false);
            items.Add(item2);
        }
        public override void UpdateBiomes()
        {
            ZoneRadiated = SuffWorld.DoomBlockCount >= 125;
        }
        public override void PostUpdate()
        {
            if (player.CountItem(ItemID.MusketBall, 200) >= 200)
            {
                hasVeteranMoveInRequirement = true;
            }
            if (NPC_RepulseLocally)
            {
                for (int i = 0; i < 180; i++)
                {
                    Dust.NewDustPerfect(Main.MouseWorld + Utils.RotatedBy(new Vector2(80f, 0f), MathHelper.ToRadians(i * 2), default), 124, null, 0, new Color(255, 0, 0), 0.6f).noGravity = true;
                }
            }
            if (NPC_AttractLocally)
            {
                for (int i = 0; i < 180; i++)
                {
                    Dust.NewDustPerfect(Main.MouseWorld + Utils.RotatedBy(new Vector2(80f, 0f), MathHelper.ToRadians(i * 2), default), 124, null, 0, new Color(0, 167, 255), 0.6f).noGravity = true;
                }
            }
            if (wearingForceField && !player.brainOfConfusion)
            {
                for (int i = 0; i < 180; i++)
                {
                    Dust.NewDustPerfect(player.Center + Utils.RotatedBy(new Vector2(100f, 0f), MathHelper.ToRadians(i * 2), default), 20, null, 0, new Color(255, 255, 255), 0.25f).noGravity = true;
                }
                for (int projectile_iteration = 0; projectile_iteration < Main.maxProjectiles; projectile_iteration++)
                {
                    Projectile projectile = Main.projectile[projectile_iteration];
                    for (int npc_iteration = 0; npc_iteration < Main.maxNPCs; npc_iteration++)
                    {
                        NPC npc = Main.npc[npc_iteration];
                        if (projectile.active
                            && projectile.hostile && projectile.damage <= 20
                            && projectile.Distance(player.Center) <= 100f
                            || npc.type == NPCID.WaterSphere
                            && npc.Distance(player.Center) <= 100f
                            || npc.type == NPCID.ChaosBall
                            && npc.Distance(player.Center) <= 100f)
                        {
                            projectile.Kill();
                            for (int dustTimes = 0; dustTimes <= 3; dustTimes++)
                            {
                                Dust.NewDust(projectile.Center, projectile.width, projectile.height, DustID.Electric);
                            }
                        }
                    }
                }
            }
            if (wearingForceField && player.brainOfConfusion)
            {
                for (int i = 0; i < 180; i++)
                {
                    Dust.NewDustPerfect(player.Center + Utils.RotatedBy(new Vector2(125f, 0f), MathHelper.ToRadians(i * 2), default), 20, null, 0, new Color(255, 255, 255), 0.25f).noGravity = true;
                }
                for (int projectile_iteration = 0; projectile_iteration < Main.maxProjectiles; projectile_iteration++)
                {
                    Projectile projectile = Main.projectile[projectile_iteration];
                    for (int npc_iteration = 0; npc_iteration < Main.maxNPCs; npc_iteration++)
                    {
                        NPC npc = Main.npc[npc_iteration];
                        if (projectile.active
                            && projectile.hostile && projectile.damage <= 30
                            && projectile.Distance(player.Center) <= 125f) 
                        {
                            projectile.Kill();
                            Dust.NewDust(projectile.Center, projectile.width, projectile.height, DustID.Electric);
                        }
                    }
                }
            }

            if (ZoneRadiated && player.whoAmI == Main.myPlayer)
            {
                Main.sunTexture = GetTexture("ExpiryMode/Assets/RottenSun");
                Main.rainTexture = GetTexture("ExpiryMode/Assets/RadiatedRain");
            }
            else
            {
                Main.sunTexture = GetTexture("Terraria/Sun");
                Main.rainTexture = GetTexture("Terraria/Rain");
            }
            if (SuffWorld.ExpiryModeIsActive)
            {
                if (player.HasBuff(BuffType<AbsoluteDoom>()))
                {
                    player.statDefense = (int)(player.statDefense * 0.2f);
                    player.buffImmune[BuffType<DoomLess>()] = true;
                }
                if (player.HasBuff(BuffType<DoomLess>()))
                {
                    player.buffImmune[BuffType<AbsoluteDoom>()] = true;
                    player.statDefense = (int)(player.statDefense * 0.6f);
                }
            }
        }

        public override void PostUpdateEquips()
        {
            if (SuffWorld.ExpiryModeIsActive)
            {
                #region AccessoryChecks
                if (player.lavaRose || player.lavaCD > 0 || player.fireWalk)
                {
                    player.ClearBuff(BuffType<AAAHHH>());
                }
                if (player.accDivingHelm || player.arcticDivingGear)
                {
                    player.ClearBuff(BuffType<WaterPain>());
                    player.ClearBuff(BuffType<WaterPainPlus>());
                }
                if (player.waterWalk || player.waterWalk2)
                {
                    player.waterWalk = false;
                    player.waterWalk2 = false;
                }
                if (player.noFallDmg)
                {
                    player.noFallDmg = false;
                }
                if (player.nightVision)
                {
                    player.ClearBuff(BuffID.Blackout);
                    player.ClearBuff(BuffID.Darkness);
                }
                if (player.longInvince && !Main.raining)
                //if (item.type == ItemID.CrossNecklace)
                {
                    player.ClearBuff(BuffType<AbsoluteDoom>());
                }
                if (!player.longInvince)
                {
                    player.ClearBuff(BuffType<DoomLess>());
                }
                if (player.longInvince && Main.raining && player.GetModPlayer<InfiniteSuffPlayer>().ZoneRadiated)
                {
                    player.longInvince = false;
                    player.ClearBuff(BuffType<DoomLess>());
                }
            }
            #endregion
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (GetInstance<ExpiryConfigClientSide>().oofHurt)
            {
                playSound = false;
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/MinecraftOof"), player.Center);

            }
            else
            {
                playSound = true;
            }
            return true;
        }
        public override void PostUpdateMiscEffects()
        {
            if (SuffWorld.ExpiryModeIsActive)
            {
                player.breathMax = 100;
                #region WetChecks
                if (player.breath < 1 && player.wet && player.Center.Y <= Main.rockLayer * 16)
                {
                    player.AddBuff(BuffType<WaterPain>(), 2);
                    // player.noBuilding = true; too evil
                }
                if (GetInstance<ExpiryConfigServerSide>().makeSpaceTerrible)
                {
                    if (player.ZoneSkyHeight)
                    {
                        player.gravity = 0f;
                    }
                }
                #endregion
            }
        }
        public override void PostUpdateBuffs()
        {
            if (SuffWorld.ExpiryModeIsActive)
            {
                if (player.lavaImmune)
                {
                    player.buffImmune[BuffType<AAAHHH>()] = true;
                }
                if (player.ZoneBeach)
                {
                    player.buffImmune[BuffType<HeatStroke>()] = true;
                    player.buffImmune[BuffType<LesserHeatStroke>()] = true;
                }
                #region underground checks
                if (player.ZoneDirtLayerHeight && !player.ZoneUnderworldHeight && player.Center.Y <= Main.rockLayer * 16)
                {
                    player.gravity = .5f;
                    player.AddBuff(BuffType<GravityPlus>(), 2);
                    // Rectangle _ = new Rectangle(100, 100, 100, 100);
                }
                #endregion
                #region Cavern Gravity effects and more
                if (player.Center.Y >= Main.rockLayer * 16 && player.breath < 1 && player.wet)
                {
                    player.AddBuff(BuffType<WaterPainPlus>(), 2);
                }
                if (player.ZoneDirtLayerHeight)
                {
                    player.AddBuff(BuffType<GravityPlus>(), 2);
                    player.gravity = .5f;
                }
                if (!player.ZoneOverworldHeight && player.ZoneRockLayerHeight && !player.ZoneUnderworldHeight && !player.ZoneDirtLayerHeight)
                {
                    player.gravity = .65f;
                    player.AddBuff(BuffType<GravityPlusPlus>(), 2);
                }
                #endregion
                #region other debuffs
                if (player.ZoneUnderworldHeight)
                {
                    player.gravity = .8f;
                    player.AddBuff(BuffType<GravityPlusPlusExtra>(), 2);
                    player.AddBuff(BuffType<AAAHHH>(), 2);
                }
                if (player.lavaWet)
                {
                    player.Hurt(PlayerDeathReason.ByCustomReason($"{player.name} died to lava instantly."), player.statLifeMax2, 0);
                }
                if (player.ZoneCorrupt)
                {
                    player.AddBuff(BuffType<RottingAway>(), 2);
                }
                if (player.ZoneSnow)
                {
                    player.AddBuff(BuffID.Chilled, 2);
                }
                if (player.ZoneHoly)
                {
                    if (GetInstance<ExpiryConfigServerSide>().noGoodBuffs)
                    {
                        player.AddBuff(BuffType<PurityBuff>(), 7200);
                    }
                }

                if (ZoneRadiated)
                {
                    player.AddBuff(BuffType<AbsoluteDoom>(), 2);
                    player.AddBuff(BuffType<DoomLess>(), 2);
                }
                if (ZoneRadiated && player.ZoneDesert && !player.longInvince)
                {
                    player.buffImmune[BuffType<DoomLess>()] = true;
                    player.buffImmune[BuffType<HeatStroke>()] = true;
                    player.buffImmune[BuffType<LesserHeatStroke>()] = true;
                }
                if (ZoneRadiated && player.wet)
                {
                    player.AddBuff(BuffType<RadiatedWater>(), 2);
                    if (player.HasBuff(BuffType<RadiatedWater>()))
                    {
                        Main.PlaySound(SoundID.Item15);
                    }
                }
                if (player.ZoneDesert)
                {
                    player.AddBuff(BuffType<HeatStroke>(), 2);
                }

                if (player.ZoneDesert && !Main.dayTime)
                {
                    player.buffImmune[BuffType<HeatStroke>()] = true;
                    player.buffImmune[BuffType<LesserHeatStroke>()] = true;
                }
                if (player.ZoneDesert && player.wet)
                {
                    player.AddBuff(BuffType<LesserHeatStroke>(), 600);
                }

                if (player.HasBuff(BuffType<LesserHeatStroke>()))
                {
                    player.buffImmune[BuffType<HeatStroke>()] = true;
                }

                if (player.ZoneDesert && player.wet && !Main.dayTime)
                {
                    player.AddBuff(BuffID.Chilled, 600);
                }
                #endregion
                #region More Evil Debuffs
                if (player.ZoneCrimson && player.ZoneBeach || player.ZoneCorrupt && player.ZoneBeach /* || player.ZoneCrimson && player.ZoneDesert || player.ZoneCorrupt && player.ZoneDesert*/)
                {
                    player.buffImmune[BuffType<Refreshed>()] = true;
                    player.buffImmune[BuffType<HeatStroke>()] = true;
                    player.buffImmune[BuffType<LesserHeatStroke>()] = true;
                }
                if (player.ZoneCrimson)
                {
                    player.AddBuff(BuffType<Fleshy>(), 2);
                }

                if (player.Center.Y <= 1600)
                {
                    player.AddBuff(BuffType<CantBreathe>(), 2);
                }
                if (player.ZoneBeach)
                {
                    if (GetInstance<ExpiryConfigServerSide>().noGoodBuffs)
                    {
                        player.AddBuff(BuffType<Refreshed>(), 1800);
                    }
                }

                if (player.ZoneJungle)
                {
                    player.AddBuff(BuffType<Murky>(), 2);
                }
                #endregion
                #region if this, dont do that thanks
                if (player.ZoneDirtLayerHeight)
                {
                    player.AddBuff(BuffID.Darkness, 2);
                }

                if (player.ZoneRockLayerHeight || player.ZoneUnderworldHeight)
                {
                    player.AddBuff(BuffID.Blackout, 2);
                }
                #endregion
            }
        }
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            bool notKilledByNPC = damageSource.SourceNPCIndex <= 0;
            {
                if (player.HasBuff(ModContent.BuffType<AbsoluteDoom>()) && notKilledByNPC)
                {
                    damageSource = PlayerDeathReason.ByCustomReason($"{player.name} lost control of their body.");
                }
                if (player.HasBuff(ModContent.BuffType<RadiatedWater>()) && notKilledByNPC)
                {
                    damageSource = PlayerDeathReason.ByCustomReason($"{player.name}'s radiation levels got too high.");
                }
                if (player.HasBuff(ModContent.BuffType<CantBreathe>()) && notKilledByNPC)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        damageSource = PlayerDeathReason.ByCustomReason($"{player.name} couldn't breathe.");
                    }
                    else
                    {
                        damageSource = PlayerDeathReason.ByCustomReason($"{player.name} suffocated in space.");
                    }
                }
                if (player.HasBuff(ModContent.BuffType<RottingAway>()) && notKilledByNPC)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        damageSource = PlayerDeathReason.ByCustomReason($"{player.name} rotted away.");
                    }
                    else
                    {
                        damageSource = PlayerDeathReason.ByCustomReason($"{player.name}'s skin became loose flesh.");
                    }
                }
                if (notKilledByNPC && player.HasBuff(ModContent.BuffType<WaterPain>()) || player.HasBuff(ModContent.BuffType<WaterPainPlus>()) && notKilledByNPC)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        damageSource = PlayerDeathReason.ByCustomReason($"{player.name} let the water vortex consume them.");
                    }
                    else
                        damageSource = PlayerDeathReason.ByCustomReason($"{player.name}'s plead for death was answered by water.");
                }
                if (player.HasBuff(ModContent.BuffType<Murky>()) && notKilledByNPC)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        damageSource = PlayerDeathReason.ByCustomReason($"{player.name} soaked in the overly-moist environment for too long.");
                    }
                    else
                        damageSource = PlayerDeathReason.ByCustomReason($"{player.name} let the murkiness overcome them.");
                }
                if (player.HasBuff(ModContent.BuffType<AAAHHH>()) && notKilledByNPC)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        damageSource = PlayerDeathReason.ByCustomReason($"{player.name} burned in hell. Literally.");
                    }
                    else
                    {
                        damageSource = PlayerDeathReason.ByCustomReason($"The forever burning flames of hell consumed {player.name} piece by piece.");
                    }
                }
                if (MagnetActive)
                {
                        switch (Main.rand.Next(5))
                        {
                            default:
                                player.KillMe(PlayerDeathReason.ByCustomReason($"{player.name} has an overall net charge of 0."), 0, 0);
                                break;
                            case 1:
                                player.KillMe(PlayerDeathReason.ByCustomReason($"{player.name} had all the electrons sapped out of them."), 0, 0);
                                break;
                            case 2:
                                player.KillMe(PlayerDeathReason.ByCustomReason($"{player.name} had all the protons sapped out of them."), 0, 0);
                                break;
                            case 3:
                                player.KillMe(PlayerDeathReason.ByCustomReason($"{player.name} is no longer charged."), 0, 0);
                                break;
                            case 4:
                                player.KillMe(PlayerDeathReason.ByCustomReason($"{player.name} channeled for too long."), 0, 0);
                                break;
                        }
                }
                return true;
            }
        }
        public override bool ModifyNurseHeal(NPC nurse, ref int health, ref bool removeDebuffs, ref string chatText)
        {
            if (SuffWorld.ExpiryModeIsActive)
            {
                if (Main.npc.Any(n => n.active && n.boss))
                {
                    chatText = "I'm too frightened by that boss to heal you!";
                    return false;
                }
                if (nurse.life != nurse.lifeMax)
                {
                    chatText = "Are you really asking a hurt woman to heal you? Give me a break.";
                    return false;
                }
                if ((float)player.statLife / player.statLifeMax2 >= 0.5f)
                {
                    chatText = "You aren't hurt enough. Come back when you are more hurt.";
                    return false;
                }
            }
            return base.ModifyNurseHeal(nurse, ref health, ref removeDebuffs, ref chatText);
        }
        public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
        {
            if (SuffWorld.ExpiryModeIsActive)
            {
                if (junk)
                {
                    return;
                }
                if (questFish == ItemType<GlowingCatfish>() && liquidType == LiquidID.Water && Main.rand.NextBool(3) && Main.player[Main.myPlayer].GetModPlayer<InfiniteSuffPlayer>().ZoneRadiated)
                {
                    caughtType = ItemType<GlowingCatfish>();
                }
            }
        }
    }
    public class NPCInstances : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;
    }
}