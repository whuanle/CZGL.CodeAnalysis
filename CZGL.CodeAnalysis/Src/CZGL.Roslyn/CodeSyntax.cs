using CZGL.Roslyn.Templates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn
{
    /// <summary>
    /// 代码工具
    /// </summary>
    public static class CodeSyntax
    {

        #region 命名空间
        /// <summary>
        /// 创建一个命名空间
        /// </summary>
        /// <returns></returns>
        public static NamespaceBuilder CreateNamespace()
        {
            return new NamespaceBuilder();
        }


        /// <summary>
        /// 创建一个命名空间
        /// </summary>
        /// <param name="namespaceName">命名空间名称</param>
        /// <returns></returns>
        public static NamespaceBuilder CreateNamespace(string namespaceName)
        {
            return new NamespaceBuilder(namespaceName);
        }

        public static NamespaceBuilder NamespaceTransform()
        {
            return null;
        }

        #endregion
    }
}
