using System.Collections.Generic;

namespace Automation.World.Layers {
    public interface IWorldLayer<T> : IEnumerable<T> {
        public T GetTile(int x, int y);
        public void SetTile(int x, int y, T tile);
    }
}