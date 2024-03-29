﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CZGL.Reflect
{
    /// <summary>
    /// AttributeDefine 扩展
    /// </summary>
    [CLSCompliant(true)]
    public static class AttributeDefineExtension
    {
        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="define"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string View(this AttributeDefine define)
        {
            if (define.ConstructorArguments.IsNullOrEmpty() && define.NamedArguments.IsNullOrEmpty())
                return $"[{define.Name}]";

            var comma = define.ConstructorArguments.IsHasValue() && define.NamedArguments.IsHasValue() ? ", " : "";
            string? constroctors = null;
            string? propertyParams = null;

            if (define.ConstructorArguments.IsHasValue())
            {
                constroctors = string.Join(", ", define.ConstructorArguments!.Select(c => c.ToString()));
            }
            if (define.NamedArguments.IsHasValue())
            {
                propertyParams = string.Join(", ", define.NamedArguments!.Select(c => $"{c.Key.Name} = {c.Value.ToString()}"));
            }

            return $"[{define.Name}({constroctors}{comma}{propertyParams})]";
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="defines"></param>
        /// <returns></returns>
        public static string View(this IEnumerable<AttributeDefine> defines)
        {
            return string.Join(Environment.NewLine, defines.Select(d => View(d)));
        }
    }
}
