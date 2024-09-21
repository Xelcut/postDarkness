using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Generation;
using System.Collections.Generic;
using Terraria.WorldBuilding;

namespace PostDarkness
{
    public class VinoreWorldGen : ModSystem
    {
       public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            if (ShiniesIndex != -1)
            {
                tasks.Insert(ShiniesIndex + 1, new PassLegacy("Vinore", GenerateTerraniore));
            }
        }

        private void GenerateTerraniore(GenerationProgress progress, Terraria.IO.GameConfiguration configuration)
        {
            progress.Message = "Generating Vinore";
            Mod.Logger.Info("Vinore generation started.");

            // Check if Moon Lord has been defeated
            if (NPC.downedMoonlord)
            {
                // Generate Terraniore in the cavern layer
                for (int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 0.01); i++) // Adjusted frequency
                {
                    int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                    int y = WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY); // Cavern layer
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<Tiles.VinoreTile>());
                    Mod.Logger.Info($"Vinore generated at ({x}, {y})");
                }
            }

            Mod.Logger.Info("Vinore generation completed.");
        }

    }
}
