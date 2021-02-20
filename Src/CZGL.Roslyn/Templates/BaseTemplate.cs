using CZGL.Roslyn.States;
using CZGL.Roslyn.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    /// <summary>
    /// 基础模板，所有模板都必须继承此抽象类
    /// </summary>
    public abstract class BaseTemplate
    {
        /// <summary>
        /// 所有结构共有的属性，如名称
        /// </summary>
        protected internal readonly BaseState _base = new BaseState();


        #region 名称

        /// <summary>
        /// 设置名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal void WithName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            _base.Name = name;
        }

        /// <summary>
        /// 随机生成一个名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal virtual void WithRondomName(string prefix = "N")
        {
            _base.Name = CodeUtil.CreateRondomName(prefix);
        }

        #endregion

        /// <summary>
        /// 获取此构造器设的命名空间表
        /// </summary>
        internal IEnumerable<string> Namespaces => _base.Namespaces;

        /// <summary>
        /// 命名空间名称
        /// <para>此处添加的命名空间将被统一收集，在构建代码时自动引用命名空间</para>
        /// </summary>
        /// <param name="namespaceName"></param>
        public virtual void WithNamespace(string namespaceName)
        {
            if (string.IsNullOrWhiteSpace(namespaceName))
                return;

            _base.Namespaces.Add(namespaceName);
        }


        /// <summary>
        /// 完整输出代码
        /// <para>不会对代码进行检查，直接输出当前已经定义的代码</para>
        /// </summary>
        /// <returns>代码 <see cref="string"/></returns>
        public abstract string ToFullCode();

        /// <summary>
        /// 完整输出格式化代码
        /// <para>会对代码进行语法树分析，检查代码是否有问题。如果无问题，再格式化代码输出</para>
        /// </summary>
        /// <returns>代码 <see cref="string"/></returns>
        public abstract string ToFormatCode();
    }
}
