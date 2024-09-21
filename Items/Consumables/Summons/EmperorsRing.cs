using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace postDarkness.Items.Consumables.Summons
{
    public class EmperorsRing : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Underground Emperor Summon");
            // Tooltip.SetDefault("Summons the Emperor of the Underground");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 20;
            Item.rare = ItemRarityID.Red;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = false;
        }

        public override bool CanUseItem(Player player)
        {
            // Ensure the boss isn't already spawned
            return !NPC.AnyNPCs(ModContent.NPCType<PostDarkness.NPCs.EmperorOfTheUnderground>()) && player.position.Y > Main.worldSurface * 16.0;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<PostDarkness.NPCs.EmperorOfTheUnderground>());
            }
            else
            {
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: ModContent.NPCType<PostDarkness.NPCs.EmperorOfTheUnderground>());
            }
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
