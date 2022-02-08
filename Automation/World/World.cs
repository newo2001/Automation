using System;
using System.Collections.Generic;
using Automation.Entities;
using Automation.Graphics;
using Automation.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Automation.World {
    public class World {
        public readonly long Seed = new Random().Next();
        private readonly SpriteBatch _spriteBatch;
        private readonly ChunkManager _chunkManager;
        private readonly List<Entity> _entities;

        public World() {
            _chunkManager = new ChunkManager();
            _spriteBatch = new SpriteBatch(AutomationGame.Game.GraphicsDevice);
            _entities = new List<Entity>();
        }

        public Chunk GetChunk(int x, int y) => _chunkManager.GetChunk(x, y);
        public Chunk GetChunkAt(int x, int y) => GetChunk(x / Chunk.Size, y / Chunk.Size);

        public Tile GetBackgroundAt(int x, int y) {
            var chunk = GetChunkAt(x, y);
            if (chunk == null)
                throw new Exception("Attempted to retrieve tile from an unloaded chunk!");
            
            return chunk.GetBackgroundType(x % Chunk.Size, y % Chunk.Size).Tile();
        }

        public void SetBackgroundTypeAt(int x, int y, TileType type) {
            var chunk = GetChunkAt(x, y);
            if (chunk == null)
                throw new Exception("Attempted to set tile in an unloaded chunk!");

            chunk.SetBackgroundType(x % Chunk.Size, y % Chunk.Size, type);
        }

        public void SpawnEntity(Entity entity) => _entities.Add(entity);

        public void ForceChunkUpdate() => _chunkManager.UpdateChunks();

        public void Draw(Camera camera) {
            _spriteBatch.Begin(transformMatrix: camera.ViewMatrix);
            
            // Draw Tiles
            var (cornerX, cornerY) = (camera.PlayerPosition / new Vector2(Chunk.Size * Tile.Size)).ToPoint();
            for (var x = cornerX - 1; x <= cornerX + 1; x++) {
                for (var y = cornerY - 1; y <= cornerY + 1; y++) {
                    GetChunk(x, y)?.Draw(_spriteBatch);
                }
            }
            
            foreach (var entity in _entities) {
                _spriteBatch.Draw(entity.Texture, new Rectangle(entity.Position.ToPoint(), new Point(32)), Color.White);
            }
            
            _spriteBatch.End();
        }
    }
}