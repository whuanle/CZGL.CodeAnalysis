using CZGL.CodeAnalysis.Shared;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CZGL.Roslyn
{
    public static class RoslynHelper
    {

        #region Token

        public static string GetName<T>(T t) where T : Enum
        {
            Type type = t.GetType();
            FieldInfo field = type.GetField(Enum.GetName(type, t));
            if (field == null) return null;
            return GetDisplayNameValue(field.GetCustomAttributesData());
        }

        /// <summary>
        /// 获取 [Display] 特性的属性 Name 的值
        /// </summary>
        /// <param name="attrs"></param>
        /// <returns></returns>
        private static string GetDisplayNameValue(IList<CustomAttributeData> attrs)
        {
            if (attrs == null || attrs.Count == 0)
                return null;
            var argument = attrs.FirstOrDefault(x => x.AttributeType.Name == nameof(MemberDefineNameAttribute)).NamedArguments;
            return argument.FirstOrDefault(x => x.MemberName == nameof(MemberDefineNameAttribute.Name)).TypedValue.Value.ToString();
        }

        #endregion


        public static SyntaxList<AttributeListSyntax> BuildAttributeListSyntax(params string[] attrs)
        {
            List<AttributeListSyntax> list = new List<AttributeListSyntax>();
            foreach (var item in attrs)
            {
                list.Add(SyntaxFactory.AttributeList(
                SyntaxFactory.SingletonSeparatedList<AttributeSyntax>(SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(item)))));
            }
            return SyntaxFactory.List<AttributeListSyntax>(list.ToArray());
        }
    }
}
