﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.Roslyn.States
{
    /// <summary>
    /// 命名空间的成员共有的属性
    /// </summary>
    public class MemberState
    {
        /// <summary>
        /// 可访问性
        /// </summary>
        public string Access { get; set; }

        // 委托事件，不具有属性修饰符，如 static

        /// <summary>
        /// 特性列表
        /// </summary>
        public HashSet<string> Atributes { get; set; } = new HashSet<string>();



    }
}
