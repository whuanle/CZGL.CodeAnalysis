using System;

namespace CZGL.CodeAnalysis.Shared
{

    /// <summary>
    /// 泛型参数以及此参数的约束，请注意构造函数的使用
    /// <para>为了避免滥用，请先了解：<inheritdoc>https://www.cnblogs.com/whuanle/p/12252754.html#113-泛型的参数名称和泛型限定</inheritdoc>/></para>
    /// </summary>
    [Obsolete]
    public class GenericScheme
    {

        private void NameIsNullOrEmpty(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name 不能为 null ！");
        }
        /// <summary>
        /// 不设置任何约束
        /// </summary>
        /// <param name="name">参数名称</param>
        public GenericScheme(string name)
        {
            NameIsNullOrEmpty(name);
            Name = name;
            TypeId = 0;
        }


        /// <summary>
        /// 唯一性约束，泛型参数约束为 struct 或 umanaged 其中一个
        /// <para>Red</para>
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="type"></param>
        public void AddStructOrUmanaged(string name, GenericConstraintsType red)
        {
            NameIsNullOrEmpty(name);

            if (!(red == GenericConstraintsType.Struct || red == GenericConstraintsType.Unmanaged))
                throw new ArgumentException($"{nameof(red)} 约束条件错误！");

            Name = name;
            TypeId = 1;
            Red = red;
        }


        /// <summary>
        /// 设置泛型约束
        /// <para>一个 yellow + 多(零)个blue 组成</para>
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="yellow">设置 class、notnull、<基类> 其中一种约束，不允许多个同时使用！</param>
        /// <param name="baseClass">继承的基类类型，如果 yellow 参数为 GenericConstraintsType.BaseClass，才需要设置此参数</param>
        /// <param name="interfaceSchemes"> <接口> 约束 </param>
        /// <param name="interfaceSchemes">  T;U 约束 </param>
        public GenericScheme(string name, GenericConstraintsType yellow, Type baseClass = null, GenericBlueScheme interfaceSchemes = null, GenericBlueScheme TUSchemes = null)
        {
            NameIsNullOrEmpty(name);

            switch (yellow)
            {
                case GenericConstraintsType.Class: Yellow = yellow; break;
                case GenericConstraintsType.Notnull: Yellow = yellow; break;
                case GenericConstraintsType.BaseClass:
                    Yellow = yellow; 
                    if (baseClass != null)
                        break;
                    else throw new ArgumentNullException($"{nameof(yellow)} 为 GenericConstraintsType.BaseClass ，但 {nameof(baseClass)} 参数为空！");
                default: throw new ArgumentException($"{nameof(yellow)} 约束条件错误！");
            }

            Name = name;
            TypeId = 2;
            if (interfaceSchemes != null)
            {
                InterfaceType = interfaceSchemes.InterfaceType;
                TypeId = 4;
            }
            if (TUSchemes != null)
            {
                TU = TUSchemes.TU;
                TypeId = 4;
            }

        }

        /// <summary>
        /// 设置泛型约束
        /// <para>一个或多个blue+orange</para>
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="interfaceSchemes"> <接口> 约束 </param>
        /// <param name="interfaceSchemes">  T;U 约束 </param>
        /// <param name="orange">new 约束，如果不设置此参数，只为 blue 约束</param>
        public GenericScheme(string name, GenericBlueScheme interfaceSchemes = null, GenericBlueScheme TUSchemes = null, bool isNew = false)
        {
            NameIsNullOrEmpty(name);

            if (interfaceSchemes==null&&TUSchemes==null)
                throw new ArgumentException($"{nameof(interfaceSchemes)} 和 {nameof(TUSchemes)} 同时为空！");


            Name = name;

            if (interfaceSchemes != null)
            {
                InterfaceType = interfaceSchemes.InterfaceType;
            }
            if (TUSchemes != null)
            {
                TU = TUSchemes.TU;
            }

            TypeId = isNew ? 7 : 3;
        }

        /// <summary>
        /// 设置泛型约束
        /// <para>一个 yellow 和 一个 orange 组成</para>
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="yellow">设置 class、notnull、<基类> 其中一种约束，不允许多个同时使用！</param>
        /// <param name="orange">new 约束</param>
        /// <param name="baseClass">继承的基类类型，如果 yellow 参数为 GenericConstraintsType.BaseClass，才需要设置此参数</param>
        public GenericScheme(string name, GenericConstraintsType yellow, GenericConstraintsType orange, Type baseClass = null)
        {
            NameIsNullOrEmpty(name);

            switch (yellow)
            {
                case GenericConstraintsType.Class: break;
                case GenericConstraintsType.Notnull: break;
                case GenericConstraintsType.BaseClass:
                    if (baseClass != null)
                    {
                        BaseType = baseClass; break;
                    }
                    else throw new ArgumentNullException($"{nameof(yellow)} 为 GenericConstraintsType.BaseClass ，但 {nameof(baseClass)} 参数为空！");
                default: throw new ArgumentException($"{nameof(yellow)} 约束条件错误！");
            }

            if (orange != GenericConstraintsType.New)
                throw new ArgumentException($"{nameof(orange)} 约束条件错误！");

            Name = name;
            TypeId = 5;
        }


        /// <summary>
        /// 设置泛型约束
        /// <para>yellow+blues+orange，blue 可为空,orange 可空</para>
        /// </summary>
        /// <param name="isNewConstraint">是否 new 约束</param>
        /// <param name="yellow">设置 class、notnull、<基类> 其中一种约束，不允许多个同时使用！</param>
        /// <param name="blues"><接口>或者 T;U，可以多个同时使用，使用数组形式存储</param>
        public GenericScheme(string name, GenericConstraintsType yellow, bool isNewConstraint, Type baseClass = null, GenericBlueScheme interfaceSchemes = null, GenericBlueScheme TUSchemes = null):this(name, yellow, baseClass, interfaceSchemes, TUSchemes)
        {
            if (isNewConstraint)
            {
                TypeId = TypeId == 2 ? 5 : 6;
            }
        }


        /// <summary>
        /// 一个参数是如何约束的标记
        /// <para>0:无约束，1:red；2:yellow，3:blues，4:yellow+blues，5:yellow+orange，6:yellow+blues+orange，7:blues+orange</para>
        /// </summary>
        public int TypeId { get; private set; }

        public GenericConstraintsType Red { get; private set; }
        public GenericConstraintsType Yellow { get; private set; }

        /// <summary>
        /// 泛型参数的名称
        /// </summary>
        public string Name { get; private set; }


        /// <summary>
        /// T:U 约束
        /// </summary>
        public string[] TU { get; private set; }

        /// <summary>
        /// 约束继承父类型
        /// </summary>
        public Type BaseType { get; private set; }


        /// <summary>
        /// 约束实现接口
        /// </summary>
        public Type[] InterfaceType { get; private set; }

    }
}
