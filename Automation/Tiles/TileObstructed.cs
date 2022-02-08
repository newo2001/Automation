using Automation.Graphics;

namespace Automation.Tiles {
    public class TileObstructed : Tile {
        public TileObstructed(TileType type, TextureName texture) : base(type, texture) {
            Traversable = false;
        }
    }
}