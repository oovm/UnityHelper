using System;

namespace Zx.Config
{
    public interface ICloneable<out T>
    {
        public T Clone();
    }
}