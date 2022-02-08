namespace Automation.Tiles {
    public enum TileType {
        TileVoid = 0, TileSand, TileWater
    }

    public static class TileTypeExtension {
        public static Tile Tile(this TileType type) => AutomationGame.Game.TileRegistry.Get(type);
    }
}