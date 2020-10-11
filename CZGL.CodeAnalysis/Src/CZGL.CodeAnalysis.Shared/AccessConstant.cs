using System;
using System.Collections.Generic;
using System.Text;

namespace CZGL.CodeAnalysis.Shared
{
    public class AccessConstant
    {
        public string Access { get; internal set; }

        public const string Public = "public";
        public const string PrivateProtected = "private protected";
        public const string Private = "private";
        public const string Internal = "internal";
        public const string Protected = "protected";
        public const string ProtectedInternal = "protected internal";
    }
}
