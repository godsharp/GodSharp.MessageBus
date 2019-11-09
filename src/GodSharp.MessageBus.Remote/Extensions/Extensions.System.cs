using System.Collections.Concurrent;

namespace GodSharp.Bus.Messages
{
    public static partial class Extensions
    {
        public static TValue AddOrUpdate<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dict, TKey key, TValue value) => dict.AddOrUpdate(key, value, (k, v) => value);
    }
}