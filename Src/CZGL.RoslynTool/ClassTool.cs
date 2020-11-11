//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace CZGL.RoslynTool
//{
//    public class ClassTool
//    {
//        public static ClassDeclarationSyntax Build(string Code, string[] attrs = null)
//        {
//            ClassDeclarationSyntax memberDeclaration;
//            memberDeclaration = CSharpSyntaxTree.ParseText(Code)
//                .GetRoot()
//                .DescendantNodes()
//                .OfType<ClassDeclarationSyntax>()
//                .Single();

//            if (attrs != null)
//                memberDeclaration = memberDeclaration
//                    .WithAttributeLists(AttributeBuilder.CreateAttributeList(attrs));

//            return memberDeclaration;
//        }

//    }
//}
