using Microsoft.Xna.Framework;

namespace Automation.Core.Math {
    public static class VectorExtensions {
        public static Vector2 Abs(this Vector2 v) => new Vector2(System.Math.Abs(v.X), System.Math.Abs(v.Y));
    }
}