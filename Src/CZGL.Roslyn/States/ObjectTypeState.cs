using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZGL.Roslyn.States
{
    public class ObjectTypeState
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public HashSet<CtorBuilder> Ctors { get; } = new HashSet<CtorBuilder>();

        /// <summary>
        /// 字段
        /// </summary>
        public HashSet<FieldBuilder> Fields { get; } = new HashSet<FieldBuilder>();

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
