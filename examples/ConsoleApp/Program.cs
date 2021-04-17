using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
// using CZGL.CodeAnalysis.Shared;
// using CZGL.Roslyn;

namespace ConsoleApp
{
    public class E : Model_泛型类4, IEnumerable<int>
    {
        public E() { }

        public IEnumerator<int> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
    public class Model_泛型1<T1, T2, T3>
    {

    }

    public class Model_泛型2<T1, T2, T3> : Model_泛型1<T1, T2, T3> where T1 : struct
    {

    }

    public class Model_泛型3 : Model_泛型1<int, double, int>
    {

    }


    public class Model_泛型类4 { }
    public class Model_泛型类5<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
        where T1 : struct
        where T2 : class
        where T3 : notnull, IEnumerable<int>
        where T4 : unmanaged
        where T5 : new()
        where T6 : Model_泛型类4, new()
        where T7 : IEnumerable<int>
        where T8 : T2
        // 组合条件
        where T9 : class, IEnumerable<int>, new()
        where T10 : Model_泛型类4, IEnumerable<int>, new()
    {
    }
    public enum En { }

    public interface Z1<in T>
    {

    }

    public interface Z2<out T>
    {

    }

    public interface Z3<T>
    {

    }
    public enum EEE
    {
        A = 0,
        B = 1,
        C = 2,
        D = 4,
        E = 8
    }

    public interface A<in T1, T2> { }

    public class Test
    {
        public ref readonly int A(ref int a)
        {
            return ref a;
        }
        public ref int B(ref int a)
        {
            return ref a;
        }


    }
    class Program
    {
        [Display]
        public readonly static int MyField = int.Parse("666");
        static void Main(string[] args)
        {
            var type1 = typeof(A<,>);
            var type2 = typeof(A<int,int>);
            var a = type1.GetGenericArguments();
            var b = type2.GetGenericArguments();
        }
    }
}
