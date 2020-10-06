using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn.States;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    /// <summary>
    /// 类构建器
    /// </summary>
    /// <typeparam name="TBuilder"><see cref="ClassBuilder"/></typeparam>
    public abstract class ClassTemplate<TBuilder> : MemberTemplate<TBuilder> where TBuilder : ClassTemplate<TBuilder>
    {
        protected internal readonly ClassState _class = new ClassState();

        #region 修饰符

        /// <summary>
        /// 类的修饰符关键字，如 static，sealed
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public virtual TBuilder WithKeyword(ClassKeyword keyword)
        {
            _class.Keyword = RoslynHelper.GetName(keyword);
            return _TBuilder;
        }

        /// <summary>
        /// 类的修饰符关键字，如 static，sealed
        /// <para>关键字拼写错误，可能会导致代码有严重错误</para>
        /// </summary>
        /// <param name="keyword">static... </param>
        /// <returns></returns>
        public virtual TBuilder WithKeyword(string keyword = "")
        {
            _class.Keyword = keyword;
            return _TBuilder;
        }

        #endregion

        #region 名称

        /// <summary>
        /// 设置名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public new virtual TBuilder WithName(string name)
        {
            base.WithName(name);
            return _TBuilder;
        }

        /// <summary>
        /// 随机生成一个名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public new virtual TBuilder WithRondomName()
        {
            base.WithRondomName();
            return _TBuilder;
        }

        #endregion


        #region 继承


        /// <summary>
        /// 继承基类
        /// </summary>
        /// <param name="baseName">基类名称</param>
        /// <returns></returns>
        public virtual TBuilder WithBaseClass(string baseName)
        {
            _class.BaseClass = baseName;
            return _TBuilder;
        }

        /// <summary>
        /// 继承接口
        /// </summary>
        /// <param name="interfaceName"></param>
        /// <returns></returns>
        public virtual TBuilder WithInterface(string interfaceName)
        {
            _class.Interfaces.Add(interfaceName);
            return _TBuilder;
        }

        /// <summary>
        /// 继承接口
        /// </summary>
        /// <param name="interfaceNames"></param>
        /// <returns></returns>
        public virtual TBuilder WithInterfaces(params string[] interfaceNames)
        {
            _ = interfaceNames.SelectMany(item => { _class.Interfaces.Add(item); ; return item; });
            return _TBuilder;
        }

        #endregion


        #region 泛型约束


        /// <summary>
        /// 为此类构建泛型
        /// </summary>
        /// <param name="builder">泛型构建器</param>
        /// <returns></returns>
        public virtual TBuilder WithGeneric(Action<GenericTemplate<GenericBuilder>> builder)
        {
            GenericBuilder generic = new GenericBuilder();
            builder.Invoke(generic);
            _class.GenericParams = generic;
            return _TBuilder;
        }

        /// <summary>
        /// 为此类构建泛型
        /// </summary>
        /// <param name="builder">构建器</param>
        /// <returns></returns>
        public virtual TBuilder WithGeneric(GenericBuilder builder)
        {
            _class.GenericParams = builder;
            return _TBuilder;
        }

        /// <summary>
        /// 为此类构建泛型
        /// </summary>
        /// <param name="paramList">泛型参数</param>
        /// <param name="constraintList">泛型参数约束</param>
        /// <returns></returns>
        public virtual TBuilder WithGeneric(string paramList, string constraintList)
        {
            _class.GenericParams = GenericBuilder.WithFromCode(paramList, constraintList);
            return _TBuilder;
        }

        /// <summary>
        /// 为此类构建泛型
        /// </summary>
        /// <param name="paramList">泛型参数</param>
        /// <param name="constraintList">泛型参数约束</param>
        /// <param name="builder">构建器</param>
        /// <returns></returns>
        public virtual TBuilder WithGeneric(string paramList, string constraintList, out GenericBuilder builder)
        {
            var generic = GenericBuilder.WithFromCode(paramList, constraintList);
            _class.GenericParams = generic;
            builder = generic;
            return _TBuilder;
        }


        #endregion


        #region 构造函数

        /// <summary>
        /// 添加一个构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual CtorBuilder WithCtor()
        {
            string name = _base.Name;
            var builder = new CtorBuilder(name);
            _class.Ctors.Add(builder);
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
            _class.Ctors.Add(ctor);
            return _TBuilder;
        }


        /// <summary>
        /// 添加一个构造函数
        /// </summary>
        /// <param name="Code">构造函数代码</param>
        /// <returns></returns>
        public virtual TBuilder WithCtorFromCode(string Code)
        {
            _class.Ctors.Add(CtorBuilder.FromCode(Code));
            return _TBuilder;
        }


        #endregion

        /// <summary>
        /// 获得构建器
        /// </summary>
        /// <returns></returns>
        public TBuilder GetBuilder()
        {
            return _TBuilder;
        }


        #region 类的成员，字段、事件、委托、属性、方法

        #region 字段
        
        /// <summary>
        /// 添加一个字段
        /// </summary>
        /// <returns></returns>
        public virtual FieldBuilder WithField()
        {
            FieldBuilder member = new FieldBuilder();
            _class.Fields.Add(member);
            return member;
        }

        /// <summary>
        /// 添加一个字段
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <returns></returns>
        public virtual FieldBuilder WithField(string name)
        {
            FieldBuilder member = new FieldBuilder(name);
            _class.Fields.Add(member);
            return member;
        }

        /// <summary>
        /// 添加一个字段
        /// </summary>
        /// <param name="builder">字段构建器</param>
        /// <returns></returns>
        public virtual TBuilder WithField(Action<FieldBuilder> builder)
        {
            FieldBuilder member = new FieldBuilder();
            builder.Invoke(member);
            _class.Fields.Add(member);
            return _TBuilder;
        }

        /// <summary>
        /// 添加一个字段
        /// </summary>
        /// <param name="builder">字段构建器</param>
        /// <returns></returns>
        public virtual TBuilder WithField(FieldBuilder builder)
        {
            _class.Fields.Add(builder);
            return _TBuilder;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 添加一个属性
        /// </summary>
        /// <returns></returns>
        public virtual PropertyBuilder WithProperty()
        {
            PropertyBuilder member = new PropertyBuilder();
            _class.Propertys.Add(member);
            return member;
        }

        /// <summary>
        /// 添加一个属性
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <returns></returns>
        public virtual PropertyBuilder WithProperty(string name)
        {
            PropertyBuilder member = new PropertyBuilder(name);
            _class.Propertys.Add(member);
            return member;
        }

        /// <summary>
        /// 添加一个属性
        /// </summary>
        /// <param name="builder">属性构建器</param>
        /// <returns></returns>
        public virtual TBuilder WithProperty(Action<PropertyBuilder> builder)
        {
            PropertyBuilder member = new PropertyBuilder();
            builder.Invoke(member);
            _class.Propertys.Add(member);
            return _TBuilder;
        }

        /// <summary>
        /// 添加一个属性
        /// </summary>
        /// <param name="builder">字段构建器</param>
        /// <returns></returns>
        public virtual TBuilder WithProperty(PropertyBuilder builder)
        {
            _class.Propertys.Add(builder);
            return _TBuilder;
        }


        #endregion

        #region 方法

        /// <summary>
        /// 添加一个方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public virtual TBuilder WithMethod(string name,Action<MethodBuilder> builder)
        {
            MethodBuilder member = new MethodBuilder(name);
            builder.Invoke(member);
            _class.Methods.Add(member);
            return _TBuilder;
        }

        /// <summary>
        /// 添加一个方法
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public virtual TBuilder WithMethod(Action<MethodBuilder> builder)
        {
            MethodBuilder member = new MethodBuilder();
            builder.Invoke(member);
            _class.Methods.Add(member);
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
            _class.Delegates.Add(member);
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
            _class.Events.Add(member);
            return _TBuilder;
        }

        #endregion

        #endregion
    }
}
