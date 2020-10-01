using System;
using System.Linq;
using ExpiryMode.Mod_;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using static Terraria.ModLoader.ModContent;

namespace ExpiryMode.NPCs.TownNPCs
{
	[AutoloadHead]
	public class WarVeteran_Other : ModNPC
	{
        public override string Texture => "ExpiryMode/NPCs/TownNPCs/WarVeteran_Other";
		public override string[] AltTextures => new[]
		{
			"ExpiryMode/NPCs/TownNPCs/WarVeteran_Other_Alt_1"
		};
        public override void SetStaticDefaults() 
		{
            DisplayName.SetDefault("War Veteran");
			Main.npcFrameCount[npc.type] = 26;
			NPCID.Sets.AttackFrameCount[npc.type] = 4;
			NPCID.Sets.DangerDetectRange[npc.type] = 500;
			NPCID.Sets.AttackType[npc.type] = 1;
			NPCID.Sets.AttackTime[npc.type] = 90;
			NPCID.Sets.AttackAverageChance[npc.type] = 30;
            NPCID.Sets.HatOffsetY[npc.type] = 0;
		}
		public override void SetDefaults()
        {
			npc.townNPC = true;
			npc.friendly = true;
			npc.width = 18;
			npc.height = 40;
			npc.aiStyle = 7;
			npc.damage = 10;
			npc.defense = 15;
			npc.lifeMax = 250;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.knockBackResist = 0.5f;
			animationType = NPCID.Guide;
		}
		public override void HitEffect(int hitDirection, double damage)
		{
			int num = npc.life > 0 ? 1 : 5;
			for (int k = 0; k < num; k++) 
            {
				Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood);
			}
		}
		public override bool CanTownNPCSpawn(int numTownNPCs, int money) 
		{
			int noVeteranActive = NPC.FindFirstNPC(NPCType<WarVeteran>());
			Player player = Main.player[Main.myPlayer];
			return player.GetModPlayer<InfiniteSuffPlayer>().hasVeteranMoveInRequirement && NPC.downedBoss1 && !Main.npc[noVeteranActive].active;
		}
		public override string TownNPCName() 
		{
			switch (WorldGen.genRand.Next(8)) 
			{
				case 0:
					return "Juaquin";
				case 1:
					return "Ryan";
				case 2:
                    return "William";
                case 3:
                    return "Beau";
                case 4:
                    return "Joseph";
                case 5:
                    return "John";
                case 6:
                    return "Bill";
                default:
					return "Jack";
			}
		}
		public override string GetChat() 
		{
			Player player = Main.player[Main.myPlayer];
            int armsDealer = NPC.FindFirstNPC(NPCID.ArmsDealer);
            int warVetOther = NPC.FindFirstNPC(NPCType<WarVeteran>());
            if (armsDealer >= 0 && Main.rand.NextBool(12))
            {
                return $"Can you believe {Main.npc[armsDealer].GivenName} is trying to 1-UP me with his sales? Yeah, I know. I win.";
            }
            if (warVetOther >= 0 && Main.rand.NextBool(12))
            {
                return $"If {Main.npc[warVetOther].GivenName} is here then you cheated him in... You lousy scoundrel >:(";
            }
            switch (Main.rand.Next(12))
            {
                case 0:
                    if (player.townNPCs > 2)
                    {
                        return "All these people are getting on my nerves. It's a good thing I have guns.";
                    }
					break;
                case 1:
                    return "This suit is the ultimate camoflague. Wait... Why are you able to see me?";
                case 2:
                    return "These scars didn't come from nothing.";
                case 3:
                    return "What is your deal with me? All I do is sell guns!";
                case 4:
                    return "Some call me crazy. Some call me stupid. Some call me violent. I call myself protective.";
                case 5:
                    return $"Do you like the stuff I sell, {player.name}? Because these things cost me a pretty penny for you to look at.";
                case 6:
                    return $"Lemme give you some fair warnings, {player.name}, don't play no games with me. I ain't that chill type of homie.";
                case 7:
                    return "These aren't simply guns. They are my babies.";
                case 8:
                    return $"{player.name}, huh? I think I've heard that name before...";
                case 9:
                    return $"I own every bit of ammo there is to own!";
                case 10:
                    return $"I sell ammo! I sell guns! I don't sell indulgences, so don't do anything bad, or you will hear from me and my gun.";
                default:
                    return "There is a box with wings that flies in the sky, and I really want to put a bullet in it so I can get that glorious loot!";
            }
			return base.GetChat();
		}
		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = "Guns";
			button2 = "Ammo";
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop) 
		{
			Player player = Main.player[Main.myPlayer];
			if (firstButton) 
			{
                shop = true;
				player.GetModPlayer<InfiniteSuffPlayer>().GunShopActive = true;
			}
			else 
			{
				player.GetModPlayer<InfiniteSuffPlayer>().GunShopActive = false;
				shop = true;
			}
		}
        public override void SetupShop(Chest shop, ref int nextSlot)
        {
			Player player = Main.player[Main.myPlayer];
            if (player.GetModPlayer<InfiniteSuffPlayer>().GunShopActive)
            {
                shop.item[nextSlot].SetDefaults(ItemID.SnowballCannon);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.Boomstick);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.RedRyder);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.Revolver);
                nextSlot++;
                if (NPC.downedBoss2)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.Musket);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.TheUndertaker);
                    nextSlot++;
                }
                if (NPC.downedBoss3)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.Handgun);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.PhoenixBlaster);
                    // shop.item[nextSlot].shopCustomPrice
                    nextSlot++;
                }
                if (Main.hardMode)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.ClockworkAssaultRifle);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.Gatligator);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.Sandgun);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.StarCannon);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.Uzi);
                    nextSlot++;
                    if (NPC.downedMechBossAny)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.DartPistol);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.DartRifle);
                        nextSlot++;
                    }
                    if (NPC.downedPirates)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.CoinGun);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.OnyxBlaster);
                        nextSlot++;
                    }
                    if (NPC.downedMechBoss1)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.Megashark);
                        nextSlot++;
                    }
                    if (NPC.downedMechBoss3)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.Flamethrower);
                        nextSlot++;
                    }
                    if (NPC.downedPlantBoss)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.VenusMagnum);
                        nextSlot++;
                    }
                    if (NPC.downedGolemBoss)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.Stynger);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.SniperRifle);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.TacticalShotgun);
                        nextSlot++;
                    }
                    if (NPC.downedChristmasSantank)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.EldMelter);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.ChainGun);
                        nextSlot++;
                    }
                    if (NPC.downedHalloweenTree)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.StakeLauncher);
                        nextSlot++;
                    }
                    if (NPC.downedAncientCultist)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.NailGun);
                        nextSlot++;
                    }
                    if (NPC.downedHalloweenKing)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.JackOLanternLauncher);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.CandyCornRifle);
                        nextSlot++;
                    }
                    if (NPC.downedMartians)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.Xenopopper);
                        nextSlot++;
                    }
                    if (NPC.downedTowerVortex)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.VortexBeater);
                        nextSlot++;
                    }
                    if (NPC.downedMoonlord)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.SDMG);
                        nextSlot++;
                    }
                }
            }
            else if (!player.GetModPlayer<InfiniteSuffPlayer>().GunShopActive)
            {
                Item shopItem = shop.item[nextSlot]; // Eh, might make this easier to read later...
                shop.item[nextSlot].SetDefaults(ItemID.Seed);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.WoodenArrow);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.FlamingArrow);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.UnholyArrow);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.JestersArrow);
                nextSlot++;
                // Arrows Above, Bullets Under
                shop.item[nextSlot].SetDefaults(ItemID.Snowball);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.MusketBall);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.SilverBullet);
                nextSlot++;
                if (NPC.downedBoss1)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.BoneArrow);
                    nextSlot++;
                }
                if (NPC.downedBoss3)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.HellfireArrow);
                    nextSlot++;
                }
                if (NPC.downedBoss2)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.MeteorShot);
                    nextSlot++;
                }
                if (NPC.downedQueenBee)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.VenomBullet);
                    nextSlot++;
                }
                if (Main.hardMode)
                {
                    shop.item[nextSlot].SetDefaults(ItemID.HolyArrow);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.CursedArrow);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.FrostburnArrow);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.IchorArrow);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.VenomArrow);
                    nextSlot++;
                    // Arrows ^
                    shop.item[nextSlot].SetDefaults(ItemID.CursedBullet);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.IchorBullet);
                    nextSlot++;
                    // Bullets ^
                    shop.item[nextSlot].SetDefaults(ItemID.RocketI);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.RocketII);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.RocketIII);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.PoisonDart);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.CrystalDart);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.CursedDart);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.IchorDart);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.FallenStar);
                    nextSlot++;
                    if (NPC.downedMechBossAny)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.HighVelocityBullet);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.ExplodingBullet);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.GoldenBullet);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.PartyBullet);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.CrystalBullet);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.EndlessQuiver);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.EndlessMusketPouch);
                        nextSlot++;
                    }
                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.ChlorophyteBullet);
                        nextSlot++;
                    }
                    if (NPC.downedPlantBoss)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.NanoBullet);
                        nextSlot++;
                    }
                    if (NPC.downedGolemBoss)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.StyngerBolt);
                        nextSlot++;
                    }
                    if (NPC.downedAncientCultist)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.Nail);
                        nextSlot++;
                    }
                    if (NPC.downedHalloweenTree)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.Stake);
                        nextSlot++;
                    }
                    if (NPC.downedHalloweenKing)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.ExplosiveJackOLantern);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.CandyCorn);
                        nextSlot++;
                    }
                    if (NPC.downedMoonlord)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.MoonlordArrow);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.MoonlordBullet);
                        nextSlot++;
                    }
                }
            }
        }
		// TODO: Make second shop work, as it is VERY fucking faulty rn

		public override void NPCLoot()
		{
			//Item.NewItem(npc.getRect(), ItemType<Items.Armor.ExampleCostume>());
		}
		public override bool CanGoToStatue(bool toKingStatue) 
		{
			return true;
		}
		public override void OnGoToStatue(bool toKingStatue) 
		{
			if (Main.netMode == NetmodeID.Server) {
				ModPacket packet = mod.GetPacket();
				packet.Write((byte)npc.whoAmI);
				packet.Send();
			}
			else 
			{
				StatueTeleport();
			}
		}

        // Create a square of pixels around the NPC on teleport.
        public void StatueTeleport()
        {
            for (int i = 0; i < 30; i++)
            {
                Vector2 position = Main.rand.NextVector2Square(-20, 21);
                if (Math.Abs(position.X) > Math.Abs(position.Y))
                {
                    position.X = Math.Sign(position.X) * 20;
                }
                else
                {
                    position.Y = Math.Sign(position.Y) * 20;
                }
                Dust.NewDustPerfect(npc.Center + position, DustID.Smoke, Vector2.Zero).noGravity = true;
            }
        }
        public override void DrawTownAttackGun(ref float scale, ref int item, ref int closeness)
        {
            item = ItemID.RedRyder;
            closeness = 10;
        }
        public override void TownNPCAttackStrength(ref int damage, ref float knockback) 
        {
            if (!Main.hardMode)
            {
                damage = 20;
                knockback = 4;
            }
            else if (Main.hardMode)
            {
                damage = 50;
                knockback = 6;
            }
		}
        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ProjectileID.Bullet;
            attackDelay = 1;
        }
        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 15f;
            randomOffset = 1.15f;
        }
        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
			cooldown = 30;
			randExtraCooldown = 30;
		}
	}
}
