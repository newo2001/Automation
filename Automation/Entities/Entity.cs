using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Automation.Entities {
    public abstract class Entity {
        public abstract Vector2 Position { get; set; }
        public abstract Texture2D Texture { get; }
    }
}