using Automation.Graphics;
using Microsoft.Xna.Framework;

namespace Automation.Tiles {
    public static class TileLoader {
        public static void LoadTiles() {
            var registry = AutomationGame.Game.TileRegistry;

            var tiles = new Tile[] {
                new Tile(TileType.TileSand, TextureName.TextureSand),
                new TileObstructed(TileType.TileWater, TextureName.TextureWater),
                new TileObstructed(TileType.TileVoid, TextureName.TextureVoid)
            };

            foreach (var tile in tiles) {
                registry.Register(tile.Type, tile);
            }
        }
    }
}