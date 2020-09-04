using Terraria.ID;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ExpiryMode.Items.Equippables.Vanity.Rune;
using ExpiryMode.Items.Ammo;
using ExpiryMode.Mod_;
using ExpiryMode.Items.Useables;
using ExpiryMode.Buffs.BadBuffs;

namespace ExpiryMode.Global_
{
    public class SuffGlobalItem : GlobalItem
    {
        public override void OpenVanillaBag(string context, Player player, int arg)
        {
            if (SuffWorld.ExpiryModeIsActive)
            {

            }
        }
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.CrossNecklace)
            {
                item.SetNameOverride("Holy Pendant");
            }
        }
        public override bool UseItem(Item item, Player player)
        {
            if (item.healMana > 0)
            {
                player.AddBuff(BuffType<ManaDeficiency>(), Main.rand.Next(240, 480), false);
            }
            return false;
        }
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.prefix == PrefixType<Warm>())
            {
                player.buffImmune[BuffID.Chilled] = true;
            }
        }
        public override bool CanUseItem(Item item, Player player)
        {
            if (item.healMana > 0)
            {
                if (player.HasBuff(BuffType<ManaDeficiency>()))
                {
                    return false;
                }
            }
            return true;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            #region Vanilla ItemTypes
            if (item.type == ItemID.ObsidianRose)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.Name == "Tooltip0")
                    {
                        line1.text = "Reduces damage from touching lava\nMakes you immune to the plaguing debuff in the underworld";
                    }
                }
            }
            if (item.type == ItemID.WaterWalkingBoots)
            {
                foreach (TooltipLine line2 in tooltips)
                {
                    if (line2.mod == "Terraria" && line2.Name == "Tooltip0")
                    {
                        line2.text = $"{mod.DisplayName} makes this work better than ever!";
                    }
                }
            }
            if (item.type == ItemID.ObsidianWaterWalkingBoots)
            {
                foreach (TooltipLine line3 in tooltips)
                {
                    if (line3.mod == "Terraria" && line3.Name == "Tooltip0")
                    {
                        line3.text = $"{mod.DisplayName} makes this work better than ever!";
                    }
                }
            }
            if (item.type == ItemID.LuckyHorseshoe)
            {
                foreach (TooltipLine line4 in tooltips)
                {
                    if (line4.mod == "Terraria" && line4.Name == "Tooltip0")
                    {
                        line4.text = $"Negates no fall damage";
                    }
                }
            }
            if (item.type == ItemID.DivingHelmet)
            {
                foreach (TooltipLine line5 in tooltips)
                {
                    if (line5.mod == "Terraria" && line5.Name == "Tooltip0")
                    {
                        line5.text = $"Greatly extends underwater breathing\nYou take less damage from drowning";
                    }
                }
            }
            if (item.type == ItemID.DivingGear)
            {
                foreach (TooltipLine line6 in tooltips)
                {
                    if (line6.mod == "Terraria" && line6.Name == "Tooltip0")
                    {
                        line6.text = $"You take less damage from drowning\nProvides the ability to swim";
                    }
                }
            }
            if (item.type == ItemID.ArcticDivingGear)
            {
                foreach (TooltipLine line7 in tooltips)
                {
                    if (line7.mod == "Terraria" && line7.Name == "Tooltip0")
                    {
                        line7.text = $"Greatly extends underwater breathing\nYou take less damage from drowning\nProvides the ability to swim";
                    }
                }
            }
            if (item.type == ItemID.JellyfishDivingGear)
            {
                foreach (TooltipLine line8 in tooltips)
                {
                    if (line8.mod == "Terraria" && line8.Name == "Tooltip0")
                    {
                        line8.text = $"Greatly extends underwater breathing\nYou take less damage from drowning\nProvides the ability to swim";
                    }
                }
            }
            if (item.type == ItemID.CrossNecklace)
            {
                foreach (TooltipLine line9 in tooltips)
                {
                    if (line9.mod == "Terraria" && line9.Name == "Tooltip0")
                    {
                        line9.text = $"Increases length of invincibility\nReduces the effects of the Radiance\n";
                    }
                }
            }
            if (item.type == ItemID.CrossNecklace && Main.raining)
            {
                foreach (TooltipLine line9 in tooltips)
                {
                    if (line9.mod == "Terraria" && line9.Name == "Tooltip0")
                    {
                        line9.text = $"Increases length of invincibility\nReduces the effects of the Radiance\nDoesn't Work for as long at it is raining in the Radiance";
                    }
                }
            }
            #endregion
            #region Mod ItemTypes
            if (Mod_.ExpiryModeMod.ShiftIsPressed.Current && item.type == ItemType<RunePlateBoots>())
            {
                foreach (TooltipLine modLine1 in tooltips)
                {
                    if (modLine1.mod == "Terraria" && modLine1.Name == "Tooltip0")
                    {
                        modLine1.text = $"'Built with only the finest of metals'\n[c/303030:'Clink, Clank']";
                    }
                }
            }
            if (Mod_.ExpiryModeMod.ShiftIsPressed.Current && item.type == ItemType<RadiantArrowItem>())
            {
                foreach (TooltipLine modLine2 in tooltips)
                {
                    if (modLine2.mod == "Terraria" && modLine2.Name == "Tooltip0")
                    {
                        modLine2.text = $"Stuns your enemies and gives them radiation poisoning!\n[c/303030:Dev Note: If you know why the projectile has a weird tilt to it, contact me on discord with the tag 'RighteousRyan#4321'.]";
                    }
                }
            }
            if (!SuffWorld.ExpiryModeIsActive && item.type == ItemType<ChaliceofDeath>())
            {
                foreach (TooltipLine modLine3 in tooltips)
                {
                    if (modLine3.mod == "Terraria" && modLine3.Name == "Tooltip0")
                    {
                        modLine3.text = $"Enables Expiry Mode\nBe aware. You can only use this item once. If you enable the mode, you cannot disable it ever again in this world.\nBefore you use it, you must be absolutely sure that you want to enable the mode.";
                    }
                }
            }
            if (SuffWorld.ExpiryModeIsActive && item.type == ItemType<ChaliceofDeath>())
            {
                foreach (TooltipLine modLine3 in tooltips)
                {
                    if (modLine3.mod == "Terraria" && modLine3.Name == "Tooltip0")
                    {
                        modLine3.text = $"You cannot disable Expiry Mode in this world once it has already\nbeen enabled in this world. This item is now worthless.\nIf you want to disable the mode, please debug\nto disable it, as you cannot legitimately disable it.";
                    }
                }
            }
            #endregion
            #region All Items
            if (item.prefix == PrefixType<Warm>())
            {
                TooltipLine toolLine = new TooltipLine(mod, "Warm", "Immune to chills")
                {
                    isModifier = true
                };
                tooltips.Add(toolLine);
            }
            if (item.rare == ExpiryRarity.Expiry)
            {
                TooltipLine expiryAdd = new TooltipLine(mod, "Expiry", "Expiry");
                tooltips.Add(expiryAdd);
            }
            #endregion
        }
    }
    public class ExpiryRarity : GlobalItem
    {
        public static int Expiry = 20;
    }
}