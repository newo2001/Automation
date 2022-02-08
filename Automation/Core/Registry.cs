using System.Collections.Generic;

namespace Automation.Core {
    public class Registry<TKey, TValue> {
        private readonly Dictionary<TKey, TValue> _registry = new Dictionary<TKey, TValue>();

        public void Register(TKey key, TValue value) => _registry[key] = value;
        public TValue Get(TKey key) => _registry[key];
    }
}