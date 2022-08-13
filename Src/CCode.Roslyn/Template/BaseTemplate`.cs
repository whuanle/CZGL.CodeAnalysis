using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CCode.Roslyn
{
    /// <summary>
    /// 基础模板
    /// </summary>
    public abstract class BaseTemplate<TBuilder> where TBuilder : BaseTemplate<TBuilder>
    {

        private IdentifierNameSyntax _name;

        /// <summary>
        /// 成员名称
        /// </summary>
        public string Name => _name.Identifier.ValueText;

        /// <summary>
        /// 
        /// </summary>
        protected BaseTemplate()
        {
            _name = SyntaxFactory.IdentifierName(CodeUtil.CreateRondomName("N_"));
        }


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

        /// <summary>
        /// 获取节点
        /// </summary>
        /// <returns></returns>
        public abstract SyntaxNode GetNode();

        /// <summary>
        /// 完整输出格式化代码
        /// <para>会对代码进行语法树分析，检查代码是否有问题。如果无问题，再格式化代码输出</para>
        /// </summary>
        /// <param name="context"></param>
        /// <returns>代码 <see cref="string"/></returns>
        public abstract string ToFormatCode(CodeContext? context);

    }
}
