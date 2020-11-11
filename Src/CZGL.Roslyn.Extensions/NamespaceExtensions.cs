using CZGL.Roslyn.Templates;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn.Extensions
{
    /// <summary>
    /// 创建命名空间的拓展方法 <see cref="NamespaceBuilder"/>
    /// </summary>
    public static class NamespaceExtensions
    {

        /// <summary>
        /// <b>自动</b>添加 using 命名空间
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static NamespaceBuilder WithAutoUsing(this NamespaceBuilder builder)
        {
            builder.WithUsing(DependencyContext.Default.CompileLibraries.Select(x => x.Name).ToArray());
            return builder;
        }
    }
}
