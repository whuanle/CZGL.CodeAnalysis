namespace CZGL.Roslyn.States
{
    /// <summary>
    /// 成员基础结构表示
    /// </summary>
    public class BaseState
    {
        /// <summary>
        /// 成员名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否使用了字符串代码生成
        /// </summary>
        public bool UseCode { get; set; } = false;

#nullable disable

        /// <summary>
        /// 字符串代码
        /// </summary>
        public string Code { get; set; } = null;
#nullable disable
    }
}
