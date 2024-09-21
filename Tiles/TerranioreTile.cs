using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PostDarkness.Tiles
{
    public class TerranioreTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileOreFinderPriority[Type] = 410;
            Main.tileShine2[Type] = true;
            Main.tileShine[Type] = 975;
            Main.tileOreFinderPriority[Type] = 410;

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(0, 255, 0), name);

            DustType = DustID.Stone;
            MineResist = 7f;
            HitSound = SoundID.Tink;
            MinPick = 65;
        }
    }
}
