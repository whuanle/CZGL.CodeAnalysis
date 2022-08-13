namespace CZGL.Roslyn.Templates
{
    /// <summary>
    /// 委托构建器模板
    /// </summary>
    /// <typeparam name="TBuilder"></typeparam>
    public abstract class DelegateTemplate<TBuilder> : FuncTemplate<TBuilder>
        where TBuilder : DelegateTemplate<TBuilder>
    {
    }
}
