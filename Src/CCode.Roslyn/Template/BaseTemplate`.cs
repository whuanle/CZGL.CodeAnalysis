using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace CCode.Roslyn
{
    /// <summary>
    /// 基础模板
    /// </summary>
    public abstract class BaseTemplate
    {
        #region 抽象

        /// <summary>
        /// 获取节点
        /// </summary>
        /// <returns></returns>
        public abstract SyntaxNode GetNode();

        /// <summary>
        /// 完整输出格式化代码
        /// <para>会对代码进行语法树分析，检查代码是否有问题。如果无问题，再格式化代码输出</para>
        /// </summary>
        /// <param name="codeContext"></param>
        /// <returns>代码 <see cref="string"/></returns>
        public abstract string ToFormatCode(CodeContext? codeContext);
    }

    /// <summary>
    /// 基础模板
    /// </summary>
    public abstract class BaseTemplate<TBuilder> : BaseTemplate where TBuilder : BaseTemplate<TBuilder>
    {
        #region 初始化

        /// <summary>
        /// 成员名称
        /// </summary>
        protected IdentifierNameSyntax _name = default!;

        /// <summary>
        /// 特性语法树列表
        /// </summary>
        protected readonly List<AttributeSyntax> _attributes = new List<AttributeSyntax>();

        /// <summary>
        /// token 列表
        /// </summary>
        protected SyntaxTokenList _modifiers = new SyntaxTokenList();

        /// <summary>
        /// 成员名称
        /// </summary>
        public string Name => _name.Identifier.ValueText;

        /// <summary>
        /// 
        /// </summary>
        protected BaseTemplate()
        {
            _name = BuildName();
        }

        /// <summary>
        /// 构建元素名称
        /// </summary>
        /// <returns></returns>
        protected virtual IdentifierNameSyntax BuildName()
        {
            return SyntaxFactory.IdentifierName(CodeUtil.CreateRondomName("N_"));
        }

        #endregion


        /// <summary>
        /// 获取当前已定义代码的特性列表
        /// </summary>
        /// <returns></returns>
        public virtual AttributeListSyntax GetAttributeSytax()
        {
            return SyntaxFactory.AttributeList(SyntaxFactory.SeparatedList(_attributes));
        }


        #region 名称

        /// <summary>
        /// 设置标识符
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual TBuilder WithName(string name)
        {
            _name = SyntaxFactory.IdentifierName(name);
            return (TBuilder)this;
        }

        /// <summary>
        /// 随机生成一个标识符
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        public virtual TBuilder WithRondomName(string prefix = "N")
        {
            WithName(CodeUtil.CreateRondomName(prefix));
            return (TBuilder)this;
        }

        #endregion

        public TBuilder WithToken(MemberAccess memberAccess)
        {
            SyntaxKind kind;
            switch (memberAccess)
            {
                case MemberAccess.Public: kind = SyntaxKind.PublicKeyword; break;
                case MemberAccess.Protected: kind = SyntaxKind.ProtectedKeyword; break;
                case MemberAccess.Internal: kind = SyntaxKind.InternalKeyword; break;
                case MemberAccess.Private: kind = SyntaxKind.PrivateKeyword; break;
                case MemberAccess.PrivateProtected: kind = SyntaxKind.PrivateKeyword; break;
                case MemberAccess.ProtectedInternal: kind = SyntaxKind.ProtectedKeyword; break;
                default: return (TBuilder)this;
            }
            var token = SyntaxFactory.Token(kind);
            _modifiers.Add(token);
            return (TBuilder)this;
        }

        public override string ToFormatCode(CodeContext? codeContext)
        {
            if (codeContext == null) return GetNode().ToFullString();
            return GetNode().NormalizeWhitespace(
                indentation: codeContext.Indentation,
                eol: codeContext.Eol,
                elasticTrivia: codeContext.ElasticTrivia).ToFullString();
        }
    }
}