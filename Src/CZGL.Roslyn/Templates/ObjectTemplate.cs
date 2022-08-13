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
    /// 基础类型模板，接口、类、结构体
    /// <para>定义能够被左式的类型，如 XXX x = ... ...</para>
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class ObjectTemplate<TBuilder> : MemberTemplate<TBuilder>
        where TBuilder : ObjectTemplate<TBuilder>
    {
        /// <summary>
        /// 属性
        /// </summary>
        public HashSet<PropertyBuilder> Propertys { get; } = new HashSet<PropertyBuilder>();

        /// <summary>
        /// 方法
        /// </summary>
        public HashSet<MethodBuilder> Methods { get; } = new HashSet<MethodBuilder>();


        /// <summary>
        /// 设置访问修饰符(Access Modifiers)
        /// </summary>
        /// <param name="access">标记</param>
        /// <returns></returns>
        public TBuilder WithAccess(NamespaceAccess access = NamespaceAccess.Internal)
        {
            _access = EnumCache.View<NamespaceAccess>(access);
            return (TBuilder)this;
        }

        #region 属性

        /// <summary>
        /// 添加一个属性
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <returns></returns>
        public virtual PropertyBuilder WithProperty(string name)
        {
            PropertyBuilder member = new PropertyBuilder(name);
            _objectState.Propertys.Add(member);
            return member;
        }

        /// <summary>
        /// 添加一个属性
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <param name="builder">属性构建器</param>
        /// <returns></returns>
        public virtual TBuilder WithProperty(string name, Action<PropertyBuilder> builder)
        {
            PropertyBuilder member = new PropertyBuilder(name);
            builder.Invoke(member);
            _objectState.Propertys.Add(member);
            return _TBuilder;
        }

        /// <summary>
        /// 添加一个属性
        /// </summary>
        /// <param name="builder">字段构建器</param>
        /// <returns></returns>
        public virtual TBuilder WithProperty(PropertyBuilder builder)
        {
            _objectState.Propertys.Add(builder);
            return _TBuilder;
        }


        #endregion


        /// <summary>
        /// 继承接口
        /// </summary>
        /// <param name="interfaceName"></param>
        /// <returns></returns>
        public virtual TBuilder WithInterface(string interfaceName)
        {
            _objectState.Interfaces.Add(interfaceName);
            return _TBuilder;
        }

        /// <summary>
        /// 继承接口
        /// </summary>
        /// <param name="interfaceNames"></param>
        /// <returns></returns>
        public virtual TBuilder WithInterfaces(params string[] interfaceNames)
        {
            _ = interfaceNames.Execute(item => _objectState.Interfaces.Add(item));
            return _TBuilder;
        }

        #region 方法

        /// <summary>
        /// 添加一个方法
        /// </summary>
        /// <param name="name"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public virtual TBuilder WithMethod(string name, Action<MethodBuilder> builder)
        {
            MethodBuilder member = new MethodBuilder(name);
            builder.Invoke(member);
            _objectState.Methods.Add(member);
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
            _objectState.Methods.Add(member);
            return _TBuilder;
        }


        #endregion

    }
}
