using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Linq
{
    internal static class Extensons
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsNullOrEmpty<T>(this IEnumerable<T>? source)
        {
            if (source == null) return true;
            if (source.Count() <= 0) return true;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsHasValue<T>(this IEnumerable<T>? source)
        {
            if (source == null) return false;
            if (source.Count() <= 0) return false;
            return true;
        }
    }
}
