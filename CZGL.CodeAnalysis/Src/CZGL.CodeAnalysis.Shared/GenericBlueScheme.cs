using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{

    /// <summary>
    /// 当约束为 blue 时，参数约束的信息
    /// </summary>
    public class GenericBlueScheme
    {
        /// <summary>
        /// T:U 约束，例如 T3:T1,T2
        /// </summary>
        /// <param name="TUNames"></param>
        public GenericBlueScheme(string[] TUNames)
        {
            if (TUNames is null || TUNames.Length == 0)
                throw new ArgumentNullException($"{TUNames} 不能为空或数组长度为 0 ！");
            Constraints = GenericConstraintsType.TU;
            TU = TUNames;
        }

        /// <summary>
        /// 接口约束
        /// </summary>
        /// <param name="interfaces">要求实现的接口列表</param>
        public GenericBlueScheme(Type[] interfaces)
        {
            if (interfaces is null || interfaces.Length == 0)
                throw new ArgumentNullException($"{interfaces} 不能为空或数组长度为 0 ！");
            Constraints = GenericConstraintsType.Interface;
            InterfaceType = interfaces;
        }



        /// <summary>
        /// 泛型参数约束
        /// </summary>
        public GenericConstraintsType Constraints { get; private set; }

        /// <summary>
        /// T:U 约束
        /// </summary>
        public string[] TU { get; private set; }

        /// <summary>
        /// 约束实现接口
        /// </summary>
        public Type[] InterfaceType { get; private set; }
    }
}
