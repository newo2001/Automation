using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Automation.Core.Util;
using Automation.Tiles;

namespace Automation.World {
    public class ChunkManager {
        private readonly ConcurrentDictionary<int, ConcurrentDictionary<int, Chunk>> _chunks;

        public ChunkManager() {
            _chunks = new ConcurrentDictionary<int, ConcurrentDictionary<int, Chunk>>();
            Scheduler.ScheduleTask(UpdateChunks, TimeSpan.FromSeconds(3));
        }
        
        public Task GenerateChunk(int x, int y) {
            if (ChunkIsLoaded(x, y))
                throw new Exception($"Attempted to regenerate already existing chunk ({x}, {y})!");

            if (!_chunks.ContainsKey(x))
                _chunks[x] = new ConcurrentDictionary<int, Chunk>();
            
            var generator = new ChunkGenerator();

            var chunk = new Chunk(x, y);
            _chunks[x][y] = chunk;
            
            return Task.Run(() => generator.GenerateChunk(chunk));
        }
        
        public bool ChunkIsLoaded(int x, int y) {
            if (!_chunks.TryGetValue(x, out var value) || !value.TryGetValue(y, out var chunk))
                return false;
            return chunk.IsReady;
        }
        
        public Chunk GetChunk(int x, int y) {
            if (!_chunks.TryGetValue(x, out var value)) return null;
            return value.TryGetValue(y, out var chunk) ? chunk : null;
        }

        public async void UpdateChunks() {
            var (chunkX, chunkY) = (AutomationGame.Game.Player.Position / Chunk.Size / Tile.Size).ToPoint();
            
            // Attempt to generate unloaded chunks in proximity of player
            var tasks = new List<Task>();
            for (var x = chunkX - 4; x <= chunkX + 4; x++) {
                for (var y = chunkY - 4; y <= chunkY + 4; y++) {
                    if (!ChunkIsLoaded(x, y)) {
                        tasks.Add(GenerateChunk(x, y));
                    }
                }
            }

            await Task.WhenAll(tasks);
        }
    }
}