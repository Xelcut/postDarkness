using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace postDarkness.Items.Weapons
{
    public class TerraniBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Terrani Blade");
            //Tooltip.SetDefault("A powerful sword forged in the darkness.");
        }

        public override void SetDefaults()
        {
            Item.damage = 315; // Set the damage
            Item.DamageType = DamageClass.Melee; // This is a melee weapon
            Item.width = 60; // Hitbox width
            Item.height = 60; // Hitbox height
            Item.useTime = 20; // Speed of the weapon
            Item.useAnimation = 25; // Animation speed
            Item.useStyle = ItemUseStyleID.Swing; // How the weapon is used
            Item.knockBack = 6; // Knockback power
            Item.value = Item.sellPrice(gold: 10); // Value in coins
            Item.rare = ItemRarityID.Red; // Rarity
            Item.UseSound = SoundID.Item1; // Sound when used
            Item.autoReuse = true; // Whether it auto-reuses
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<PostDarkness.Items.Bars.Terrabar>(), 4);; // Example ingredient
            recipe.AddTile(TileID.LunarCraftingStation); // Crafting station
            recipe.Register();
        }
    }
}
