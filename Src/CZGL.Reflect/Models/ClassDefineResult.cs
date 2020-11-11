using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Models
{
    public class ClassDefineResult : ClassDefine
    {
        /// <summary>
        /// 当前类的类型
        /// </summary>
        public Type ClassType { get; set; }

        /// <summary>
        /// 类的名称
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 类定义
        /// </summary>
        public string DefinitionString { get; set; }

        /// <summary>
        /// 是否密封类
        /// </summary>
        public bool IsSealed { get; set; }

        /// <summary>
        /// 是否静态类
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// 是否抽象类
        /// </summary>
        public bool IsAbstract { get; set; }

        /// <summary>
        /// 是否继承了接口或基类
        /// </summary>
        public bool IsInherit { get; set; }

        /// <summary>
        /// 继承的父类
        /// </summary>
        public Type BaseType { get; set; }

        /// <summary>
        /// 实现的接口
        /// </summary>
        public Type[] Interfaces { get; set; }

        /// <summary>
        /// 当前类型是否为泛型类型
        /// </summary>
        public bool IsGenericType { get; set; }

        /// <summary>
        /// 泛型类的泛型参数
        /// </summary>
        public GenericeParamterInfo[] GenericeParamters { get; set; }
    }
}
