using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PostDarkness.Items.Bars
{
    public class Vinbar : ModItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Terrabar");
            //Tooltip.SetDefault("A bar forged from powerful Terraniore.");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.value = 10000;
            Item.rare = 2;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.consumable = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Ores.Vinore>(), 4);
            recipe.AddTile(TileID.LunarCraftingStation); // Ancient Manipulator
            recipe.Register();
        }
    }
}
