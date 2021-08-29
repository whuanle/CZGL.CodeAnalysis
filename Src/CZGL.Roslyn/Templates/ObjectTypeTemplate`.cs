using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZGL.Roslyn.Templates
{
    /// <summary>
    /// 类、结构体构建模板
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class ObjectTypeTemplate<TBuilder> : ObjectTemplate<TBuilder>
        where TBuilder : ObjectTypeTemplate<TBuilder>
    {
        /// <summary>
        /// 对象类型状态机
        /// </summary>
        protected internal readonly ObjectTypeState _typeState = new ObjectTypeState();

        /// <summary>
        /// 类的修饰符关键字，如 static，sealed；结构体修饰符只有readonly
        /// <para>关键字拼写错误，可能会导致代码有严重错误</para>
        /// </summary>
        /// <param name="keyword">static... </param>
        /// <returns></returns>
        public virtual TBuilder WithKeyword(string keyword = "")
        {
            _typeState.Keyword = keyword;
            return _TBuilder;
        }




        #region 构造函数

        /// <summary>
        /// 添加一个构造函数
        /// </summary>
        /// <returns></returns>
        public virtual CtorBuilder WithCtor()
        {
            string name = _base.Name;
            var builder = new CtorBuilder(name);
            _typeState.Ctors.Add(builder);
            return builder;
        }

        /// <summary>
        /// 添加一个构造函数
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public virtual TBuilder WithCtor(Action<CtorBuilder> builder)
        {
            CtorBuilder ctor = new CtorBuilder(_base.Name);
            builder.Invoke(ctor);
            _typeState.Ctors.Add(ctor);
            return _TBuilder;
        }


        /// <summary>
        /// 添加一个构造函数
        /// </summary>
        /// <param name="Code">构造函数代码</param>
        /// <returns></returns>
        public virtual TBuilder WithCtorFromCode(string Code)
        {
            _typeState.Ctors.Add(CtorBuilder.FromCode(Code));
            return _TBuilder;
        }


        #endregion

        #region 类的成员，字段、事件、委托、属性、方法

        #region 字段


        /// <summary>
        /// 添加一个字段
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <returns></returns>
        public virtual FieldBuilder WithField(string name)
        {
            FieldBuilder member = new FieldBuilder(name);
            _typeState.Fields.Add(member);
            return member;
        }

        /// <summary>
        /// 添加一个字段
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="builder">字段构建器</param>
        /// <returns></returns>
        public virtual TBuilder WithField(string name, Action<FieldBuilder> builder)
        {
            FieldBuilder member = new FieldBuilder(name);
            builder.Invoke(member);
            _typeState.Fields.Add(member);
            return _TBuilder;
        }

        /// <summary>
        /// 添加一个字段
        /// </summary>
        /// <param name="builder">字段构建器</param>
        /// <returns></returns>
        public virtual TBuilder WithField(FieldBuilder builder)
        {
            _typeState.Fields.Add(builder);
            return _TBuilder;
        }

        #endregion



        #region 委托、事件

        /// <summary>
        /// 统计一个委托
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public virtual TBuilder WithDelegate(Action<DelegateBuilder> builder)
        {
            DelegateBuilder member = new DelegateBuilder();
            builder.Invoke(member);
            _typeState.Delegates.Add(member);
            return _TBuilder;
        }

        /// <summary>
        /// 添加一个事件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public virtual TBuilder WithDelegate(Action<EventBuilder> builder)
        {
            EventBuilder member = new EventBuilder();
            builder.Invoke(member);
            _typeState.Events.Add(member);
            return _TBuilder;
        }

        #endregion

        #endregion
    }
}
