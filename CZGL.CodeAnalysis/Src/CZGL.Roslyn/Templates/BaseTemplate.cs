using CZGL.Roslyn.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.Templates
{
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
        internal virtual void WithRondomName()
        {
            _base.Name = "N" + Guid.NewGuid().ToString("N");
        }

        #endregion


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
