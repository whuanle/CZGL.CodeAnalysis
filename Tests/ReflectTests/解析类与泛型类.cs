using CZGL.Reflect;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace ReflectTests
{
    public class Model_����1<T1, T2, T3>
    {

    }

    public class Model_����2<T1, T2, T3> : Model_����1<T1, T2, T3> where T1 : struct
    {

    }

    public class Model_����3 : Model_����1<int, double, int>
    {

    }


    public class Model_������4 { }
    public class Model_������5<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
        where T1 : struct
        where T2 : class
        where T3 : notnull
        where T4 : unmanaged
        where T5 : new()
        where T6 : Model_������4
        where T7 : IEnumerable<int>
        where T8 : T2
        // �������
        where T9 : class, new()
        where T10 : Model_������4, IEnumerable<int>, new()
    {
    }

    public interface IModel1�����ඨ��
    {

    }
    public class Model1_5 : Model_����1<int, double, int>, IEnumerable<object>, IModel1�����ඨ��
    {
        public IEnumerator<object> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class ����������
    {
        ITestOutputHelper _tempOutput;
        public ����������(ITestOutputHelper tempOutput)
        {
            _tempOutput = tempOutput;
        }
        [Fact]
        public void ������_���Ͳ����б�_δ�����������()
        {
            Type type = typeof(Model_����1<,,>);
            Assert.Equal("<T1, T2, T3>", "<" + string.Join(", ", GenericeAnalysis.GetGenriceParams(type)) + ">");
        }

        [Fact]
        public void ������_���Ͳ����б�_�Ѷ����������()
        {
            Type type = typeof(Model_����1<int, double, int>);
            var array = type.GetGenriceParams();    // GenericeAnalysis.GetGenriceParams(type);
            Assert.Equal("<int, double, int>", "<" + string.Join(", ", array) + ">");
        }

        [Fact]
        public void ������_���͸������()
        {
            Model_����2<int, double, int> model_ = new Model_����2<int, double, int>();
            Type type = model_.GetType();
            Assert.Equal("<int, double, int>", "<" + string.Join(", ", GenericeAnalysis.GetGenriceParams(type)) + ">");
            //Assert.Equal("<System.Int32, System.Double, System.Int32>", GenericeAnalysis.Analysis(type.BaseType, true));
        }

        [Fact]
        public void ����Ƿ���_���෺���Ѷ������()
        {
            Type type = new Model_����3().GetType();
            Assert.Equal("<>", "<" + string.Join(",", GenericeAnalysis.GetGenriceParams(type)) + ">");
            //Assert.Equal("<System.Int32, System.Double, System.Int32>", "<"+string.Join(",", GenericeAnalysis.GetGenriceParams(type))+">");
        }

        [Fact]
        public void ��Ƕ�׷��ͷ��Ͷ���()
        {
            Type type = typeof(Model_����1<int, List<int>, Dictionary<List<int>, Dictionary<int, List<int>>>>);
            string output = GenericeAnalysis.GetGenriceName(type);
            _tempOutput.WriteLine(output);
            Assert.Equal("Model_����1<int, List<int>, Dictionary<List<int>, Dictionary<int, List<int>>>>", output);
        }

        [Fact]
        public void �����ӵķ���Լ��()
        {
            Type type = typeof(Model_������5<,,,,,,,,,,>);
            _tempOutput.WriteLine(GenericeAnalysis.GetGetConstrainCode(type, true));
            Assert.Equal(@"where T1 : struct 
where T2 : class 
where T3 : notnull 
where T4 : struct 
where T5 : new() 
where T6 : Model_������4 
where T7 : IEnumerable<int> 
where T8 : T2 
where T9 : class,new() 
where T10 : Model_������4,IEnumerable<int>,new() 
", GenericeAnalysis.GetGetConstrainCode(type, true));
        }
    }
}
