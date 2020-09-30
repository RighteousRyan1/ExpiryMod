using Terraria.ModLoader;
using ExpiryMode.Global_;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using ExpiryMode.Mod_;

namespace ExpiryMode.Items.Equippables.Vanity.Prismatic
{
    [AutoloadEquip(EquipType.Legs)]
    public class PrismaticReflectiveBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prismatic Reflective Boots");
            Tooltip.SetDefault("'Great for impersonating Expiry Devs!'\n'Some things shine brighter than others.'");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 12314;
            item.rare = ExpiryRarity.PrismaticRarity;
            item.vanity = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<InfiniteSuffPlayer>().accPrisLegs = true;
        }
        public override void UpdateVanity(Player player, EquipType type)
        {
            player.GetModPlayer<InfiniteSuffPlayer>().accPrisLegs = true;
        }
    }
}