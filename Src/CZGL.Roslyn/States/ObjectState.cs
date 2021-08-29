﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZGL.Roslyn.States
{
    /// <summary>
    /// 类型状态机
    /// </summary>
    /// <remarks>适合类、结构体等</remarks>
    public class ObjectState
    {

        /// <summary>
        /// 接口列表
        /// </summary>
        public HashSet<string> Interfaces { get; } = new HashSet<string>();

        /// <summary>
        /// 属性
        /// </summary>
        public HashSet<PropertyBuilder> Propertys { get; } = new HashSet<PropertyBuilder>();

        /// <summary>
        /// 方法
        /// </summary>
        public HashSet<MethodBuilder> Methods { get; } = new HashSet<MethodBuilder>();
    }
}
