using CZGL.CodeAnalysis.Shared;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    public abstract class ClassTemplate<TBuilder> where TBuilder : ClassTemplate<TBuilder>
    {
        protected internal TBuilder _TBuilder;
        protected internal bool IsNested = false;

        protected internal readonly List<string> MemberAttrs = new List<string>();
        protected internal string Visibility = string.Empty;
        protected internal string Qualifier;
        protected internal string MemberName = "YourName";
        protected internal string BaseTypeName;
        protected internal List<string> BaseInterfaces = new List<string>();
        protected internal string Constraint;

        protected internal List<MemberDeclarationSyntax> Members = new List<MemberDeclarationSyntax>();

        protected internal List<ConstructorDeclarationSyntax> Ctors = new List<ConstructorDeclarationSyntax>();




        #region 特性注解

        /// <summary>
        /// 添加一些特性
        /// <para>会清除已存在的特性</para>
        /// </summary>
        /// <param name="attrs"></param>
        /// <returns></returns>
        public virtual TBuilder SetAttributeLists(string[] attrs = null)
        {
            MemberAttrs.Clear();
            MemberAttrs.AddRange(attrs);
            return _TBuilder;
        }



        /// <summary>
        /// 添加一个特性
        /// </summary>
        /// <param name="attr"></param>
        /// <returns></returns>
        public virtual TBuilder AddAttribute(string attr)
        {
            MemberAttrs.Add(attr);
            return _TBuilder;
        }

        #endregion

        #region 访问权限

        /// <summary>
        /// 访问权限
        /// </summary>
        /// <param name="visibilityType"></param>
        /// <returns></returns>
        public virtual TBuilder SetVisibility(ClassVisibilityType visibilityType = ClassVisibilityType.Internal)
        {
            Visibility = RoslynHelper.GetName(visibilityType);
            return _TBuilder;
        }

        /// <summary>
        /// 访问权限
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public virtual TBuilder SetVisibility(string str = null)
        {
            Visibility = str;
            return _TBuilder;
        }

        /// <summary>
        /// 定义当前类为嵌套类，并设置访问修饰符
        /// </summary>
        /// <param name="visibilityType"></param>
        /// <returns></returns>
        public virtual TBuilder SetNestedVisibility(MemberVisibilityType visibilityType)
        {
            Visibility = RoslynHelper.GetName(visibilityType);
            return _TBuilder;
        }

        #endregion

        #region 修饰符

        /// <summary>
        /// 修饰符
        /// </summary>
        /// <param name="qualifierType"></param>
        /// <returns></returns>
        public virtual TBuilder SetQualifier(ClassQualifierType qualifierType)
        {
            Qualifier = RoslynHelper.GetName(qualifierType);
            return _TBuilder;
        }

        /// <summary>
        /// 修饰符
        /// </summary>
        /// <param name="str">static... </param>
        /// <returns></returns>
        public virtual TBuilder SetQualifier(string str = "")
        {
            Qualifier = str;
            return _TBuilder;
        }

        #endregion

        #region 命名

        /// <summary>
        /// 设置名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual TBuilder SetName(string name)
        {
            MemberName = name;
            return _TBuilder;
        }

        /// <summary>
        /// 定义类的名称并设置泛型
        /// </summary>
        /// <param name="name">类的名称</param>
        /// <param name="genericParams">泛型参数列表,如果不为泛型类,则此为 null </param>
        /// <returns></returns>
        public virtual TBuilder SetClassName(string name, string[] genericParams = null)
        {
            MemberName = (string.Concat(name, "<", string.Join(",", genericParams), ">"));
            return _TBuilder;
        }

        #endregion

        #region 继承


        /// <summary>
        /// 继承基类
        /// </summary>
        /// <param name="baseName"></param>
        /// <returns></returns>
        public virtual TBuilder SetBaseType(string baseName)
        {
            BaseTypeName = baseName;
            return _TBuilder;
        }


        /// <summary>
        /// 继承接口
        /// </summary>
        /// <param name="baseName"></param>
        /// <returns></returns>
        public virtual TBuilder SetInterfaces(string[] names)
        {
            BaseInterfaces.AddRange(names);
            return _TBuilder;
        }

        #endregion


        #region 泛型约束

        public virtual TBuilder SetConstraint(string Code)
        {
            Constraint = Code;
            return _TBuilder;
        }

        public virtual TBuilder SetConstraint(Action<GenericBuilder> builder)
        {

            return _TBuilder;
        }

        #endregion


        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public virtual TBuilder AddCtor(string Code)
        {
            Ctors.Add(CtorBuilder.Build(Code));
            return _TBuilder;
        }


        public virtual TBuilder AddCtor(ConstructorDeclarationSyntax syntax)
        {
            Ctors.Add(syntax);
            return _TBuilder;
        }


        public virtual TBuilder AddCtor(Action<CtorBuilder> builder)
        {
            CtorBuilder ctor = new CtorBuilder();
            builder.Invoke(ctor);
            Ctors.Add(ctor.Build());
            return _TBuilder;
        }

        #endregion


        #region 类的成员，字段、事件、委托、属性、方法

        /// <summary>
        /// 统计字段、属性、方法、委托、事件等
        /// </summary>
        /// <returns></returns>
        public virtual TBuilder AddMember<TMember>(TMember member)where TMember: MemberDeclarationSyntax
        {
            Members.Add(member);
            return _TBuilder;
        }

        public virtual TBuilder AddMember(Action<FieldBuilder> builder) 
        {
            FieldBuilder member = new FieldBuilder();
            builder.Invoke(member);
            Members.Add(member.Build());
            return _TBuilder;
        }

        public virtual TBuilder AddMember(Action<PropertyBuilder> builder)
        {
            PropertyBuilder member = new PropertyBuilder();
            builder.Invoke(member);
            Members.Add(member.Build());
            return _TBuilder;
        }


        public virtual TBuilder AddMember(Action<MethodBuilder> builder)
        {
            MethodBuilder member = new MethodBuilder();
            builder.Invoke(member);
            Members.Add(member.Build());
            return _TBuilder;
        }

        public virtual TBuilder AddMember(Action<DelegateBuilder> builder)
        {
            DelegateBuilder member = new DelegateBuilder();
            builder.Invoke(member);
            Members.Add(member.Build());
            return _TBuilder;
        }


        public virtual TBuilder AddMember(Action<EventBuilder> builder)
        {
            EventBuilder member = new EventBuilder();
            builder.Invoke(member);
            Members.Add(member.Build());
            return _TBuilder;
        }


        #endregion
    }
}
