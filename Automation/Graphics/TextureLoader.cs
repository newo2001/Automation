using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

using static Automation.Graphics.TextureName;

namespace Automation.Graphics {
    public static class TextureLoader {
        public static void LoadTextures() {
            var game = AutomationGame.Game;
            var registry = game.TextureRegistry;

            var textures = new Dictionary<TextureName, string>() {
                { TextureSand, "Graphics/Textures/Tiles/tile_sand" },
                { TextureWater, "Graphics/Textures/Tiles/tile_water" },
                { TextureVoid, "Graphics/Textures/Tiles/tile_void" },
                { Cursor, "Graphics/Textures/Interface/Hud/cursor" },
            };

            foreach (var (name, texture) in textures) {
                registry.Register(name, game.Content.Load<Texture2D>(texture));
            }
        }
    }
}