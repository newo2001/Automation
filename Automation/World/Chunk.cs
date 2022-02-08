using Automation.Graphics;
using Automation.Tiles;
using Automation.World.Layers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Automation.World {
    public class Chunk {
        public const int Size = 32;
        
        public ChunkState State { get; set; }
        private readonly IWorldLayer<TileType> _background = new BackgroundLayer();
        public Point ChunkPosition { get; }

        public Chunk(int x, int y) {
            ChunkPosition = new Point(x, y);
            State = ChunkState.Unloaded;
        }

        public bool IsReady => State == ChunkState.Generated;
        public bool IsBusy => State == ChunkState.Generating || State == ChunkState.Populating;
        
        public TileType GetBackgroundType(int x, int y) => _background.GetTile(x, y);
        public void SetBackgroundType(int x, int y, TileType type) => _background.SetTile(x, y, type);

        public void Draw(SpriteBatch batch) {
            for (var x = 0; x < Size; x++) {
                for (var y = 0; y < Size; y++) {
                    var tCoords = new Vector2(x, y) * Tile.Size + ChunkPosition.ToVector2() * Size * Tile.Size;

                    var tile = GetBackgroundType(x, y).Tile();
                    batch.Draw(tile.Texture, new Rectangle(tCoords.ToPoint(), tile.Dimensions), Color.White);
                }
            }
        }
    }
}