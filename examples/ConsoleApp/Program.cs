﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
// using CZGL.CodeAnalysis.Shared;
// using CZGL.Roslyn;

namespace ConsoleApp
{
    public class E: Model_泛型类4, IEnumerable<int>
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
        where T6 : Model_泛型类4,new()
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
        A=0,
        B=1,
        C=2,
        D=4,
        E=8
    }

    class Program
    {
        [Display]
        public readonly static int MyField = int.Parse("666");
        static void Main(string[] args)
        {
            var z = EEE.B | EEE.C | EEE.D;
            Console.WriteLine(z^EEE.D);

            if(typeof(Program).GetMethod("A") is var method && method != null)
            {

            }
            //var obj = new Model_泛型类5<int, Program, E, En, Program, E, List<int>, Program, Program, E, int>();
            Type a = typeof(Model_泛型类5<int, Program, E, En, Program, E, List<int>, Program, E, E, int>).GetGenericTypeDefinition();

            var types = a.GetGenericArguments();

            foreach (var item in types)
            {
                Console.WriteLine(item.Name);
                Console.WriteLine("参数约束"+item.GenericParameterAttributes);
                var tmp = item.GetGenericParameterConstraints();
                foreach (var i in item.GetCustomAttributes())
                    Console.WriteLine(i.GetType().Name);
                foreach (var node in tmp)
                {
                    
                    Console.WriteLine(node.Name);
                }
                Console.WriteLine("--------------");
            }

            
            Console.ReadKey();
        }
    }
}
