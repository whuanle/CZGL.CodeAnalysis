using CZGL.CodeAnalysis.Shared;
using CZGL.Roslyn.Templates;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CZGL.Roslyn.Templates
{
    public abstract class GenericTemplate<TBuilder> where TBuilder : GenericTemplate<TBuilder>
    {
        protected internal Dictionary<string, GenericConstarintInfo> Constarints = new Dictionary<string, GenericConstarintInfo>();

        protected internal TBuilder _TBuilder;

        /// <summary>
        /// 添加一个泛型参数并设置约束
        /// <para>建议使用此API对泛型约束进行定义！</para>
        /// </summary>
        /// <param name="parmter">参数名称</param>
        /// <param name="scheme">泛型类别</param>
        /// <returns></returns>
        //public virtual TBuilder AddConstarint(GenericScheme scheme)
        //{
        //    if (scheme == null)
        //        throw new ArgumentNullException(nameof(scheme), "参数不能为 null !");

        //    switch (scheme.TypeId)
        //    {
        //        case 0: Add(scheme.Name); break;
        //        case 1: AddRed(scheme); break;
        //        case 2: AddYellow(scheme); break;
        //        case 3: AddBlue(scheme); break;
        //        case 4: AddYellow(scheme); AddBlue(scheme); break;
        //        case 5: AddYellow(scheme); AddOrange(scheme); break;
        //        case 6: AddYellow(scheme); AddBlue(scheme); AddOrange(scheme); break;
        //        case 7: AddBlue(scheme); AddOrange(scheme); break;
        //        default:
        //            throw new Exception();
        //    }

        //    return _TBuilder;
        //}


        #region 8 s种型约束

        /// <summary>
        /// 处理 Red 
        /// </summary>
        /// <returns></returns>
        private void AddRed(GenericScheme scheme)
        {
            if (scheme.Red == GenericConstraintsType.Struct)
                AddStruct(scheme.Name);
            else
                AddUnmanaged(scheme.Name);
        }


        private void AddYellow(GenericScheme scheme)
        {
            switch (scheme.Yellow)
            {
                case GenericConstraintsType.Class: AddClass(scheme.Name); break;
                case GenericConstraintsType.Notnull: AddNotNull(scheme.Name); break;
                case GenericConstraintsType.BaseClass: AddBaseClass(scheme.Name, scheme.BaseType.Name); break;
            }
        }

        private void AddBlue(GenericScheme scheme)
        {
            if (scheme.InterfaceType != null)
                AddInterface(scheme.Name, scheme.InterfaceType.Select(x => x.Name).ToArray());
            if (scheme.TU != null)
                AddTU(scheme.Name, scheme.TU.Select(x => x).ToArray());
        }

        private void AddOrange(GenericScheme scheme)
        {
            AddNewNull(scheme.Name);
        }

        /// <summary>
        /// 添加 struct 约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <returns></returns>
        public virtual TBuilder AddStruct(string paramterName)
        {
            var info = Search(paramterName);

            info.Name = paramterName;
            info.Constraints.Add("struct");

            return _TBuilder;
        }

        /// <summary>
        /// 添加 unmanaged 约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <returns></returns>
        private void AddUnmanaged(string paramterName)
        {
            var info = Search(paramterName);
            info.Name = paramterName;
            info.Constraints.Add("unmanaged");
        }

        /// <summary>
        /// 添加 Class 约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <returns></returns>
        private void AddClass(string paramterName)
        {
            var info = Search(paramterName);
            info.Name = paramterName;

            info.Constraints.Add("class");
        }


        /// <summary>
        /// 添加 notnull 约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <returns></returns>
        private void AddNotNull(string paramterName)
        {
            var info = Search(paramterName);
            info.Name = paramterName;

            info.Constraints.Add("notnull");
        }

        /// <summary>
        /// 添加 <基类> 约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <param name="baseTypeName"></param>
        /// <returns></returns>
        private void AddBaseClass(string paramterName, string baseTypeName)
        {
            var info = Search(paramterName);
            info.Name = paramterName;

            info.Constraints.Add("baseTypeName");
        }

        /// <summary>
        /// 添加接口约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <param name="interfaces"></param>
        /// <returns></returns>
        private void AddInterface(string paramterName, string[] interfaces)
        {
            var info = Search(paramterName);
            info.Name = paramterName;

            for (int i = 0; i < interfaces.Length; i++)
            {
                info.Constraints.Add(interfaces[i]);
            }
        }

        /// <summary>
        /// 添加 T:U 约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <param name="UName"></param>
        /// <returns></returns>
        private void AddTU(string paramterName, string[] UName)
        {
             AddInterface(paramterName, UName);
        }

        /// <summary>
        /// 添加 new() 约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <param name="UName"></param>
        /// <returns></returns>
        private void AddNewNull(string paramterName)
        {
            var info = Search(paramterName);
            info.Name = paramterName;
            info.Constraints.Add("new()");
        }

        #endregion

        #region 参数管理

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="paramterName"></param>
        /// <returns></returns>
        private GenericConstarintInfo Search(string paramterName)
        {
            GenericConstarintInfo info = new GenericConstarintInfo()
            {
                Name = paramterName
            };

            if (!Constarints.ContainsKey(paramterName))
            {
                Constarints.Add(paramterName, info);
            }
            else info = Constarints[paramterName];
            return info;
        }

        /// <summary>
        /// 添加一个参数
        /// </summary>
        /// <param name="paramterName">参数名称</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        private void Add(string paramterName)
        {
            var info = new GenericConstarintInfo()
            {
                Name = paramterName
            };

            if (!Constarints.ContainsKey(paramterName))
            {
                Constarints.Add(paramterName, info);
            }
            Constarints[paramterName] = info;
        }

        /// <summary>
        /// 移除一个泛型参数约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <returns></returns>
        public virtual TBuilder RemoveParam(string paramterName)
        {
             Remove(paramterName);
            return _TBuilder;
        }

        /// <summary>
        /// 移除一个泛型参数约束
        /// </summary>
        /// <param name="paramterName">参数名称</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        private void Remove(string paramterName)
        {
            Constarints.Remove(paramterName);
        }
        #endregion


        #region 添加约束

        /// <summary>
        /// 添加 struct 约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <returns></returns>
        public virtual TBuilder AddStructConstarint(string paramterName)
        {
            var info = Search(paramterName);

            info.Name = paramterName;
            info.Constraints.Add("struct");

            return _TBuilder;
        }

        /// <summary>
        /// 添加 unmanaged 约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <returns></returns>
        public virtual TBuilder AddUnmanagedConstarint(string paramterName)
        {
            var info = Search(paramterName);
            info.Name = paramterName;
            info.Constraints.Add("unmanaged");

            return _TBuilder;
        }

        /// <summary>
        /// 添加 Class 约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <returns></returns>
        public virtual TBuilder AddClassConstarint(string paramterName)
        {
            var info = Search(paramterName);
            info.Name = paramterName;
            info.Constraints.Add("class");

            return _TBuilder;
        }


        /// <summary>
        /// 添加 notnull 约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <returns></returns>
        public virtual TBuilder AddNotNullConstarint(string paramterName)
        {
            var info = Search(paramterName);
            info.Name = paramterName;

            info.Constraints.Add("notnull");

            return _TBuilder;
        }

        /// <summary>
        /// 添加 <基类> 约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <param name="baseTypeName"></param>
        /// <returns></returns>
        public virtual TBuilder AddBaseClassConstarint(string paramterName, string baseTypeName)
        {
            var info = Search(paramterName);
            info.Name = paramterName;

            info.Constraints.Add(baseTypeName);

            return _TBuilder;
        }

        /// <summary>
        /// 添加接口约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <param name="interfaces"></param>
        /// <returns></returns>
        public virtual TBuilder AddInterfaceConstarint(string paramterName, string[] interfaces)
        {
            var info = Search(paramterName);
            info.Name = paramterName;

            for (int i = 0; i < interfaces.Length; i++)
            {
                info.Constraints.Add(interfaces[i]);
            }


            return _TBuilder;
        }

        /// <summary>
        /// 添加 T:U 约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <param name="UName"></param>
        /// <returns></returns>
        public virtual TBuilder AddTUConstarint(string paramterName, string[] UName)
        {
             AddInterface(paramterName, UName);
            return _TBuilder;
        }

        /// <summary>
        /// 添加 T:U 约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <param name="UName"></param>
        /// <returns></returns>
        public virtual TBuilder AddTUConstarint(string paramterName, string UName)
        {
            AddInterface(paramterName, new string[] { UName });
            return _TBuilder;
        }

        /// <summary>
        /// 添加 new() 约束
        /// </summary>
        /// <param name="paramterName"></param>
        /// <param name="UName"></param>
        /// <returns></returns>
        public virtual TBuilder AddNewNullConstarint(string paramterName)
        {
            var info = Search(paramterName);
            info.Name = paramterName;

            info.Constraints.Add("new()");

            return _TBuilder;
        }

        #endregion


        /// <summary>
        /// 获得格式化代码
        /// </summary>
        /// <returns></returns>
        public abstract string FullCode();

        protected internal class GenericConstarintInfo
        {
            /// <summary>
            /// 泛型参数名称
            /// <para>class Test<T1>，则参数名称为 T1 </para>
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 泛型约束
            /// </summary>
            public List<string> Constraints { get; set; } = new List<string>();
        }
    }
}
