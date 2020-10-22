using System.Data.SqlTypes;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpiryMode.Items.Useables.Throwables
{
	public class ClusterBomb : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cluster Grenade");
			Tooltip.SetDefault("A grenade that splits into 3-4 clusters upon the grenade's destruction\n'He shoots, he scores!'");
		}
        public override void SetDefaults()
        {
            item.knockBack = 5;
            item.noMelee = true;
            item.damage = 15;
			item.thrown = true;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.useAnimation = 32;
            item.useTime = 32;
            item.maxStack = 99;
            item.consumable = true;
            item.width = 30;
            item.height = 30;
            item.value = 18351;
            item.rare = ItemRarityID.Orange;
            item.shoot = ModContent.ProjectileType<Projectiles.ProjsFromThrowables.ClusterBomb>();
            item.shootSpeed = 12f;
            item.scale = 0.5f;
            item.noUseGraphic = true;
        }
		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronBar, 1);
			recipe.AddIngredient(ItemID.ExplosivePowder, 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}

}