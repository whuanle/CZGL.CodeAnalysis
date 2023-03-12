using CCode;
using CCode.Reflect;
using System.Reflection;
using Xunit;

namespace CCode.Reflect.Tests
{
	public struct SamplePoint { }
	public abstract class _KeywordAnalyObject
	{
		public abstract int P_C { get; set; }
		public abstract int P_E { get; set; }
		public int P_F { get; set; }

		private static readonly SamplePoint s_origin = new SamplePoint();
		public virtual ref readonly SamplePoint P_J => ref s_origin;

		public virtual void M_C() { }
		public virtual void M_E() { }
		public virtual ref readonly SamplePoint M_J() => ref s_origin;
	}
	public class KeywordAnalyObject : _KeywordAnalyObject
	{
		public int F_A;
		public const int F_B = 1;
		public static int F_C;
		public readonly int F_D;
		public static readonly int F_E;
		public volatile int F_F;
		public volatile static int F_G;

		public int P_A { get; set; }
		public static int P_B { get; set; }
		public override int P_C { get; set; }
		public virtual int P_D { get; set; }
		public sealed override int P_E { get; set; }
		public new int P_F { get; set; }

		private static readonly SamplePoint s_origin = new SamplePoint();

		public ref readonly SamplePoint P_G => ref s_origin;
		public static ref readonly SamplePoint P_H => ref s_origin;
		public virtual ref readonly SamplePoint P_I => ref s_origin;
		public override ref readonly SamplePoint P_J => ref s_origin;

		public void M_A() { }
		public static void M_B() { }
		public override void M_C() { }
		public virtual void M_D() { }
		public sealed override void M_E() { }
		public new void M_F() { }
		public ref readonly SamplePoint M_G() => ref s_origin;
		public static ref readonly SamplePoint M_H() => ref s_origin;
		public virtual ref readonly SamplePoint M_I() => ref s_origin;
		public override ref readonly SamplePoint M_J() => ref s_origin;
	}

	public class KeywordAnalysisTests
	{
		[Fact]
		public void GetFieldKeyword()
		{
			var a = typeof(KeywordAnalyObject).GetField("F_A");
			var b = typeof(KeywordAnalyObject).GetField("F_B");
			var c = typeof(KeywordAnalyObject).GetField("F_C");
			var d = typeof(KeywordAnalyObject).GetField("F_D");
			var e = typeof(KeywordAnalyObject).GetField("F_E");
			var f = typeof(KeywordAnalyObject).GetField("F_F");
			var g = typeof(KeywordAnalyObject).GetField("F_G");

			Assert.Equal(FieldKeyword.Default, KeywordAnalysis.GetKeyword(a!));
			Assert.Equal(FieldKeyword.Const, KeywordAnalysis.GetKeyword(b!));
			Assert.Equal(FieldKeyword.Static, KeywordAnalysis.GetKeyword(c!));
			Assert.Equal(FieldKeyword.Readonly, KeywordAnalysis.GetKeyword(d!));
			Assert.Equal(FieldKeyword.StaticReadonly, KeywordAnalysis.GetKeyword(e!));
			Assert.Equal(FieldKeyword.Volatile, KeywordAnalysis.GetKeyword(f!));
			Assert.Equal(FieldKeyword.VolatileStatic, KeywordAnalysis.GetKeyword(g!));

			Assert.Equal(EnumCache.View(FieldKeyword.Default), KeywordAnalysis.View(a!));
			Assert.Equal(EnumCache.View(FieldKeyword.Const), KeywordAnalysis.View(b!));
			Assert.Equal(EnumCache.View(FieldKeyword.Static), KeywordAnalysis.View(c!));
			Assert.Equal(EnumCache.View(FieldKeyword.Readonly), KeywordAnalysis.View(d!));
			Assert.Equal(EnumCache.View(FieldKeyword.StaticReadonly), KeywordAnalysis.View(e!));
			Assert.Equal(EnumCache.View(FieldKeyword.Volatile), KeywordAnalysis.View(f!));
			Assert.Equal(EnumCache.View(FieldKeyword.VolatileStatic), KeywordAnalysis.View(g!));
		}

		[Fact]
		public void GetPropertydKeyword()
		{
			var a = typeof(KeywordAnalyObject).GetProperty("P_A");
			var b = typeof(KeywordAnalyObject).GetProperty("P_B");
			var _c = typeof(_KeywordAnalyObject).GetProperty("P_C");
			var c = typeof(KeywordAnalyObject).GetProperty("P_C");
			var d = typeof(KeywordAnalyObject).GetProperty("P_D");
			var e = typeof(KeywordAnalyObject).GetProperty("P_E");
			var f = typeof(KeywordAnalyObject).GetProperty("P_F");
			var g = typeof(KeywordAnalyObject).GetProperty("P_G");
			var h = typeof(KeywordAnalyObject).GetProperty("P_H");
			var i = typeof(KeywordAnalyObject).GetProperty("P_I");
			var j = typeof(KeywordAnalyObject).GetProperty("P_J");

			Assert.Equal(PropertyKeyword.Default, KeywordAnalysis.GetKeyword(a!));
			Assert.Equal(PropertyKeyword.Static, KeywordAnalysis.GetKeyword(b!));
			Assert.Equal(PropertyKeyword.Abstract, KeywordAnalysis.GetKeyword(_c!));
			Assert.Equal(PropertyKeyword.Override, KeywordAnalysis.GetKeyword(c!));
			Assert.Equal(PropertyKeyword.Virtual, KeywordAnalysis.GetKeyword(d!));
			Assert.Equal(PropertyKeyword.SealedOverride, KeywordAnalysis.GetKeyword(e!));

			Assert.Equal(PropertyKeyword.Default, KeywordAnalysis.GetKeyword(g!));
			Assert.Equal(PropertyKeyword.Static, KeywordAnalysis.GetKeyword(h!));
			Assert.Equal(PropertyKeyword.Virtual, KeywordAnalysis.GetKeyword(i!));
			Assert.Equal(PropertyKeyword.Override, KeywordAnalysis.GetKeyword(j!));
		}

		[Fact]
		public void GetMethodKeyword()
		{
			var a = typeof(KeywordAnalyObject).GetMethod("M_A");
			var b = typeof(KeywordAnalyObject).GetMethod("M_B");
			var c = typeof(KeywordAnalyObject).GetMethod("M_C");
			var d = typeof(KeywordAnalyObject).GetMethod("M_D");
			var e = typeof(KeywordAnalyObject).GetMethod("M_E");
			var f = typeof(KeywordAnalyObject).GetMethod("M_F");
			var g = typeof(KeywordAnalyObject).GetMethod("M_G");
			var h = typeof(KeywordAnalyObject).GetMethod("M_H");
			var i = typeof(KeywordAnalyObject).GetMethod("M_I");
			var j = typeof(KeywordAnalyObject).GetMethod("M_J");

			Assert.Equal(MethodKeyword.Default, KeywordAnalysis.GetKeyword(a!));
			Assert.Equal(MethodKeyword.Static, KeywordAnalysis.GetKeyword(b!));
			Assert.Equal(MethodKeyword.Override, KeywordAnalysis.GetKeyword(c!));
			Assert.Equal(MethodKeyword.Virtual, KeywordAnalysis.GetKeyword(d!));
			Assert.Equal(MethodKeyword.SealedOverride, KeywordAnalysis.GetKeyword(e!));

			Assert.Equal(MethodKeyword.Default, KeywordAnalysis.GetKeyword(g!));
			Assert.Equal(MethodKeyword.Static, KeywordAnalysis.GetKeyword(h!));
			Assert.Equal(MethodKeyword.Virtual, KeywordAnalysis.GetKeyword(i!));
			Assert.Equal(MethodKeyword.Override, KeywordAnalysis.GetKeyword(j!));
		}
	}
}
