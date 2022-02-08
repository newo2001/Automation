using System;

namespace Automation.World {
    public enum ChunkState {
        Unloaded = 0,
        Generating = 1,
        Generated = 2,
        Populating = 3,
        Populated = 4
    }
}