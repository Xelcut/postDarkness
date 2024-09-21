using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PostDarkness.Items.Ores
{
    public class Terraniore : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terraniore");
            // Tooltip.SetDefault("A mysterious ore from the depths.");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.value = 100;
            Item.rare = ItemRarityID.Blue;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.TerranioreTile>();
        }
    }
}
