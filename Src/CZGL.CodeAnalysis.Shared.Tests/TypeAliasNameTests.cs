using System;
using Xunit;

namespace CZGL.CodeAnalysis.Shared.Tests
{
    public class TypeAliasNameTests
    {
        [Fact]
        public void TypeEquals()
        {

            Assert.Equal(typeof(Object), typeof(object));

            Assert.Equal(TypeCode.Object, Type.GetTypeCode(typeof(Object)));
            Assert.Equal(TypeCode.Boolean, Type.GetTypeCode(typeof(Boolean)));
            Assert.Equal(TypeCode.Char, Type.GetTypeCode(typeof(Char)));
            Assert.Equal(TypeCode.SByte, Type.GetTypeCode(typeof(SByte)));
            Assert.Equal(TypeCode.Byte, Type.GetTypeCode(typeof(Byte)));
            Assert.Equal(TypeCode.Int16, Type.GetTypeCode(typeof(Int16)));
            Assert.Equal(TypeCode.UInt16, Type.GetTypeCode(typeof(UInt16)));
            Assert.Equal(TypeCode.Int32, Type.GetTypeCode(typeof(Int32)));
            Assert.Equal(TypeCode.UInt32, Type.GetTypeCode(typeof(UInt32)));
            Assert.Equal(TypeCode.Int64, Type.GetTypeCode(typeof(Int64)));
            Assert.Equal(TypeCode.UInt64, Type.GetTypeCode(typeof(UInt64)));
            Assert.Equal(TypeCode.Single, Type.GetTypeCode(typeof(Single)));
            Assert.Equal(TypeCode.Double, Type.GetTypeCode(typeof(Double)));
            Assert.Equal(TypeCode.Decimal, Type.GetTypeCode(typeof(Decimal)));
            Assert.Equal(TypeCode.String, Type.GetTypeCode(typeof(String)));

            Assert.Equal(TypeCode.Object, Type.GetTypeCode(typeof(object)));
            Assert.Equal(TypeCode.Boolean, Type.GetTypeCode(typeof(bool)));
            Assert.Equal(TypeCode.Char, Type.GetTypeCode(typeof(char)));
            Assert.Equal(TypeCode.SByte, Type.GetTypeCode(typeof(sbyte)));
            Assert.Equal(TypeCode.Byte, Type.GetTypeCode(typeof(byte)));
            Assert.Equal(TypeCode.Int16, Type.GetTypeCode(typeof(short)));
            Assert.Equal(TypeCode.UInt16, Type.GetTypeCode(typeof(ushort)));
            Assert.Equal(TypeCode.Int32, Type.GetTypeCode(typeof(int)));
            Assert.Equal(TypeCode.UInt32, Type.GetTypeCode(typeof(uint)));
            Assert.Equal(TypeCode.Int64, Type.GetTypeCode(typeof(long)));
            Assert.Equal(TypeCode.UInt64, Type.GetTypeCode(typeof(ulong)));
            Assert.Equal(TypeCode.Single, Type.GetTypeCode(typeof(float)));
            Assert.Equal(TypeCode.Double, Type.GetTypeCode(typeof(double)));
            Assert.Equal(TypeCode.Decimal, Type.GetTypeCode(typeof(decimal)));
            Assert.Equal(TypeCode.String, Type.GetTypeCode(typeof(string)));

            Assert.Equal(TypeCode.Object, Type.GetTypeCode(typeof(bool?)));
        }

        [Fact]
        public void GetName()
        {
            Assert.Equal("object", TypeAliasName.GetName(typeof(Object)));
            Assert.Equal("bool", TypeAliasName.GetName(typeof(Boolean)));
            Assert.Equal("char", TypeAliasName.GetName(typeof(Char)));
            Assert.Equal("sbyte", TypeAliasName.GetName(typeof(SByte)));
            Assert.Equal("byte", TypeAliasName.GetName(typeof(Byte)));
            Assert.Equal("short", TypeAliasName.GetName(typeof(Int16)));
            Assert.Equal("ushort", TypeAliasName.GetName(typeof(UInt16)));
            Assert.Equal("int", TypeAliasName.GetName(typeof(Int32)));
            Assert.Equal("uint", TypeAliasName.GetName(typeof(UInt32)));
            Assert.Equal("long", TypeAliasName.GetName(typeof(Int64)));
            Assert.Equal("ulong", TypeAliasName.GetName(typeof(UInt64)));
            Assert.Equal("float", TypeAliasName.GetName(typeof(Single)));
            Assert.Equal("double", TypeAliasName.GetName(typeof(Double)));
            Assert.Equal("decimal", TypeAliasName.GetName(typeof(Decimal)));
            Assert.Equal("string", TypeAliasName.GetName(typeof(String)));
        }
    }
}
