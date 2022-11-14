using System;

namespace Zx.Core
{
    public abstract class Singleton<T> where T : class, new()
    {
        private static readonly Lazy<T> lazy = new(() => new T());
        public static T Instance => lazy.Value;
        protected abstract void Initialize();
    }
}