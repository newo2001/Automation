using System.Threading.Tasks;
using Automation.Core;
using Automation.Tiles;
using Microsoft.Xna.Framework;

namespace Automation.World {
    public class ChunkGenerator {
        private const float SampleSpacing = 0.1f;

        public void GenerateChunk(Chunk chunk) {
            var world = AutomationGame.Game?.World;
            if (world == null) return;
                
            var noise = new Noise(1, 1, 1, 1, (int) world.Seed);

            for (var x = 0; x < Chunk.Size; x++) {
                for (var y = 0; y < Chunk.Size; y++) {
                    var (globalX, globalY) = (chunk.ChunkPosition.ToVector2() * Chunk.Size + new Vector2(x, y)) * new Vector2(SampleSpacing);
                    var height = noise.Get2D(globalX, globalY);
                    
                    chunk.SetBackgroundType(x, y, height < 0.2 ? TileType.TileSand : TileType.TileWater);
                }
            }

            chunk.State = ChunkState.Generated;
        }
    }
}