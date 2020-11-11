using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.States
{
    public class ClassState
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 继承基类
        /// </summary>
        public string BaseClass { get; set; }

        /// <summary>
        /// 接口列表
        /// </summary>
        public HashSet<string> Interfaces { get; } = new HashSet<string>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public HashSet<CtorBuilder> Ctors { get; } = new HashSet<CtorBuilder>();

        /// <summary>
        /// 字段
        /// </summary>
        public HashSet<FieldBuilder> Fields { get; } = new HashSet<FieldBuilder>();

        /// <summary>
        /// 属性
        /// </summary>
        public HashSet<PropertyBuilder> Propertys { get; } = new HashSet<PropertyBuilder>();

        /// <summary>
        /// 方法
        /// </summary>
        public HashSet<MethodBuilder> Methods { get; } = new HashSet<MethodBuilder>();

        /// <summary>
        /// 委托
        /// </summary>
        public HashSet<DelegateBuilder> Delegates { get; } = new HashSet<DelegateBuilder>();

        /// <summary>
        /// 事件
        /// </summary>
        public HashSet<EventBuilder> Events { get; } = new HashSet<EventBuilder>();

        /// <summary>
        /// 嵌套类、嵌套接口等
        /// </summary>
        public HashSet<object> Others { get; } = new HashSet<object>();
    }
}
