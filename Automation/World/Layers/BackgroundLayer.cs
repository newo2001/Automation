using System.Collections;
using System.Collections.Generic;
using Automation.Tiles;

namespace Automation.World.Layers {
    public class BackgroundLayer : IWorldLayer<TileType> {
        private readonly TileType[] _tiles = new TileType[Chunk.Size * Chunk.Size];

        public TileType GetTile(int x, int y) => _tiles[y * Chunk.Size + x];

        public void SetTile(int x, int y, TileType type) => _tiles[y * Chunk.Size + x] = type;

        public IEnumerator<TileType> GetEnumerator() => (IEnumerator<TileType>) _tiles.GetEnumerator();
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}