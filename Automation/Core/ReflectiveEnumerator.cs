using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Automation.Core {
    public static class ReflectiveEnumerator {
        public static IEnumerable<T> GetSubclassesOf<T>(object[] constructorArgs) where T : class {
            return Assembly.GetAssembly(typeof(T))
                ?.GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(T)))
                .Select(x => (T) Activator.CreateInstance(x, constructorArgs));
        }

        public static IEnumerable<T> GetSubclassesOf<T>() where T : class => GetSubclassesOf<T>(new object[] { });
    }
}