using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ModLoader.IO;

namespace PostDarkness
{
    public class VinoreSystem : ModSystem
    {
        private bool MoonLordDefeated;

        public override void OnWorldLoad()
        {
            // Reset the flag when the world loads
            MoonLordDefeated = false;
        }

        public override void OnWorldUnload()
        {
            // Reset the flag when the world unloads
            MoonLordDefeated = false;
        }

        public override void PostUpdateWorld()
        {
            // Check if Moon Lord has been defeated and the ores haven't been generated yet
            if (NPC.downedMoonlord && !MoonLordDefeated)
            {
                MoonLordDefeated = true;
                GenerateVinore();
                
            }
        }

        private void GenerateVinore()
        {
            // Generate Terraniore in the cavern layer
            for (int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 0.0001); i++) // Set frequency
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY); // Cavern layer

                // Check if the tile is in the desert, tundra, or jungle biomes
                if (!IsInRestrictedBiome(x, y))
                {
                    if (Main.tile[x, y].TileType == TileID.JungleGrass || Main.tile[x, y].TileType == TileID.JunglePlants || Main.tile[x, y].TileType == TileID.JungleVines || Main.tile[x, y].TileType == TileID.Mud)
                    {
                        WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<Tiles.VinoreTile>());
                    }
                }
            }     
        }

        private bool IsInRestrictedBiome(int x, int y)
        {  
            // Check for desert biome
            if (Main.tile[x, y].TileType == TileID.Sand || Main.tile[x, y].TileType == TileID.HardenedSand || Main.tile[x, y].TileType == TileID.Sandstone)
            {
                return true;
            }

            // Check for tundra biome
            if (Main.tile[x, y].TileType == TileID.SnowBlock || Main.tile[x, y].TileType == TileID.IceBlock || Main.tile[x, y].TileType == TileID.CorruptIce || Main.tile[x, y].TileType == TileID.FleshIce)
            {
                return true;
            }

            return false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            tag["MoonLordDefeated"] = MoonLordDefeated;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            MoonLordDefeated = tag.GetBool("MoonLordDefeated");
        }

        public override void NetSend(System.IO.BinaryWriter writer)
        {
            writer.Write(MoonLordDefeated);
        }

        public override void NetReceive(System.IO.BinaryReader reader)
        {
            MoonLordDefeated = reader.ReadBoolean();
        }
    }
}
