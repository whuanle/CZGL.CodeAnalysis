using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CZGL.Roslyn
{
    /// <summary>
    /// 成员特性构建器
    /// </summary>
    internal sealed class AttributeBuilder : AttrbuteTemplate
    {
        internal AttributeBuilder(string name)
        {
            _name = name;
        }
    }
}
