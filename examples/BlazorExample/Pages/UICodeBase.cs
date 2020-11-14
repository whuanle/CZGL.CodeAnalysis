using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorExample.Pages
{
    public class UICodeBase: ComponentBase
    {
        [Parameter]
        public string Language { get; set; } = "language-html";

        /// <summary>
        /// 自定义组件元素属性
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> UIAttributes { get; set; }

        /// <summary>
        /// 内嵌子元素
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
