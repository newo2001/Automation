using System.Drawing;
using Automation.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Point = Microsoft.Xna.Framework.Point;

namespace Automation.Tiles {
    public class Tile {
        public const int Size = 32;
        public Point Dimensions = new Point(Size, Size);
        public bool Traversable { get; protected set; } = false;
        public Texture2D Texture { get; }
        public TileType Type { get; }

        public Tile(TileType type, TextureName texture) {
            Type = type;
            Texture = AutomationGame.Game.TextureRegistry.Get(texture);
        }
    }
}