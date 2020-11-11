//using CZGL.CodeAnalysis.Shared;
//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CSharp;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace CZGL.Roslyn
//{
//    public static class RoslynHelper
//    {

//        #region Token
//        public static SyntaxToken GetToken(MemberAccess visibilityType)
//        {
//            return SyntaxFactory.Token(GetKind(visibilityType));
//        }


//        #endregion


//        #region string Token

//        public static SyntaxToken GetVisibilityToken(string str)
//        {
//            return SyntaxFactory.Token(GetVisibilityKind(str));
//        }

//        public static SyntaxToken[] GetQualifierToken(string sstr)
//        {
//            var kinds = GetQualifierKind(str);
//            return kinds == null ? null : kinds.Select(kind => SyntaxFactory.Token(kind)).ToArray();
//        }


//        #endregion

//        #region kind
//        public static SyntaxKind GetKind(MemberAccess visibilityType)
//        {
//            switch (visibilityType)
//            {
//                case MemberAccess.Internal: return SyntaxKind.InternalKeyword;
//                case MemberAccess.Public: return SyntaxKind.PublicKeyword;
//                case MemberAccess.Protected: return SyntaxKind.ProtectedKeyword;
//                case MemberAccess.Private: return SyntaxKind.PrivateKeyword;
//                case MemberAccess.ProtectedInternal: return SyntaxKind.AssemblyKeyword;
//                default:
//                    return SyntaxKind.InternalKeyword;
//            }
//        }

//        public static SyntaxKind GetVisibilityKind(string visibilityType)
//        {
//            switch (visibilityType)
//            {
//                case "internal": return SyntaxKind.InternalKeyword;
//                case "public": return SyntaxKind.PublicKeyword;
//                case "protected": return SyntaxKind.ProtectedKeyword;
//                case "private": return SyntaxKind.PrivateKeyword;
//                case "internal protected":
//                case "protected internal": return SyntaxKind.AssemblyKeyword;
//                default:
//                    return SyntaxKind.InternalKeyword;
//            }
//        }


//        #endregion


//        #region kind str
//        public static SyntaxKind[] GetKind(MemberQualifierType qualifierType)
//        {
//            switch (qualifierType)
//            {
//                case MemberQualifierType.Const: return new SyntaxKind[] { SyntaxKind.ConstKeyword };
//                case MemberQualifierType.Readonly: return new SyntaxKind[] { SyntaxKind.ReadOnlyKeyword };
//                case MemberQualifierType.Static: return new SyntaxKind[] { SyntaxKind.StaticKeyword };
//                case MemberQualifierType.Static | MemberQualifierType.Readonly: return new SyntaxKind[] { SyntaxKind.StaticKeyword, SyntaxKind.ReadOnlyKeyword };
//                default:
//                    return null;
//            }
//        }

//        public static SyntaxKind[] GetQualifierKind(string str)
//        {
//            switch (str)
//            {
//                case "const": return new SyntaxKind[] { SyntaxKind.ConstKeyword };
//                case "readonly": return new SyntaxKind[] { SyntaxKind.ReadOnlyKeyword };
//                case "static": return new SyntaxKind[] { SyntaxKind.StaticKeyword };
//                case "static readonly":
//                case "readonly staic": return new SyntaxKind[] { SyntaxKind.StaticKeyword, SyntaxKind.ReadOnlyKeyword };
//                default:
//                    return null;
//            }
//        }

//        #endregion
//    }
//}
