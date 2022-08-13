using CZGL.Roslyn.States;
using CZGL.Roslyn.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn
{
    /// <summary>
    /// 基础模板
    /// </summary>
    public abstract class BaseTemplate<TBuilder> where TBuilder : BaseTemplate<TBuilder>
    {
        #region 属性

        /// <summary>
        /// 已使用字符串代码生成
        /// </summary>
        protected bool _useCode = false;

        /// <summary>
        /// 通过字符串代码生成
        /// </summary>
        public bool IsCode => _useCode;

        /// <summary>
        /// 字符串代码
        /// </summary>
        protected string? _code;

        /// <summary>
        /// 字符串代码
        /// </summary>
        public string? Code => _code;

        /// <summary>
        /// 成员名称
        /// </summary>
        protected string _name;

        /// <summary>
        /// 成员名称
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// 成员使用到的命名空间
        /// </summary>
        protected readonly HashSet<string> _namespaces = new HashSet<string>();

        /// <summary>
        /// 成员使用到的命名空间
        /// </summary>
        public IReadOnlyCollection<string> Namespaces => _namespaces;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        protected BaseTemplate()
        {
            _name = CodeUtil.CreateRondomName("N_");
        }


        /// <summary>
        /// 设置名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual TBuilder WithName(string name)
        {
            _name = name;
            return (TBuilder)this;
        }

        /// <summary>
        /// 随机生成一个名称
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        public virtual TBuilder WithRondomName(string prefix = "N")
        {
            _name = CodeUtil.CreateRondomName(prefix);
            return (TBuilder)this;
        }

        /// <summary>
        /// 引用一个命名空间
        /// <para>此处添加的命名空间将被统一收集，在构建代码时自动引用命名空间</para>
        /// </summary>
        /// <param name="namespaceName"></param>
        public virtual TBuilder WithNamespace(string namespaceName)
        {
            _namespaces.Add(namespaceName);
            return (TBuilder)this;
        }

        /// <summary>
        /// 通过代码直接生成
        /// </summary>
        /// <param name="code">字符串代码</param>
        /// <returns></returns>
        public virtual TBuilder WithFromCode(string code)
        {
            _useCode = true;
            _code = code;

            return (TBuilder)this;
        }

        /// <summary>
        /// 完整输出格式化代码
        /// <para>会对代码进行语法树分析，检查代码是否有问题。如果无问题，再格式化代码输出</para>
        /// </summary>
        /// <returns>代码 <see cref="string"/></returns>
        public abstract string ToFormatCode(CodeContext? codeContext);

    }
}
