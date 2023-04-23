using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCode.Roslyn.Builder
{
    public class ClassBuilder : BaseTemplate<ClassBuilder>
    {
        private readonly List<BaseTypeSyntax> __bases = new List<BaseTypeSyntax>();
        private readonly List<MemberDeclarationSyntax> _members = new List<MemberDeclarationSyntax>();
        private readonly List<TypeParameterSyntax> _parameters = new List<TypeParameterSyntax>();
        private readonly List<TypeParameterConstraintClauseSyntax> _clauses = new List<TypeParameterConstraintClauseSyntax>();


        public ClassBuilder()
        {
        }

        public ClassBuilder WithBase(BaseTypeSyntax baseType)
        {
            __bases.Add(baseType);
            return this;
        }

        public ClassBuilder WithBases(params BaseTypeSyntax[] baseTypes)
        {
            __bases.AddRange(baseTypes);
            return this;
        }

        public ClassBuilder WithToken(ClassKeyword classKeyword)
        {
            SyntaxKind kind;
            switch (classKeyword)
            {
                case ClassKeyword.Static: kind = SyntaxKind.StaticKeyword; break;
                case ClassKeyword.Abstract: kind = SyntaxKind.AbstractKeyword; break;
                case ClassKeyword.Sealed: kind = SyntaxKind.SealedKeyword; break;
                default: return this;
            }
            var token = SyntaxFactory.Token(kind);
            _modifiers.Add(token);
            return this;
        }
        public ClassBuilder WithMember<T>(T value) where T : MemberDeclarationSyntax
        {
            _members.Add(value);
            return this;
        }

        public ClassBuilder WithMembers<T>(params T[] values) where T : MemberDeclarationSyntax
        {
            _members.AddRange(values);
            return this;
        }

        public ClassBuilder WWithTypeParameter(TypeParameterSyntax typeParameter, TypeParameterConstraintClauseSyntax? clauseSyntax)
        {
            _parameters.Add(typeParameter);
            if (clauseSyntax != null) _clauses.Add(clauseSyntax);
            return this;
        }

        public ClassBuilder WWithTypeParameters(
            IReadOnlyDictionary<TypeParameterSyntax, IEnumerable<TypeParameterConstraintClauseSyntax>> parameters)
        {
            foreach (var item in parameters)
            {
                _parameters.Add(item.Key);
                foreach (var clause in item.Value)
                {
                    _clauses.Add(clause);
                }
            }

            return this;
        }

        public override SyntaxNode GetNode()
        {
            var attributeLists = SyntaxFactory.SingletonList(SyntaxFactory.AttributeList(SyntaxFactory.SeparatedList(_attributes)));
            var declaration = SyntaxFactory.ClassDeclaration(
                 attributeLists: attributeLists,
                 modifiers: base._modifiers,
                 identifier: base._name.Identifier,
                 typeParameterList: SyntaxFactory.TypeParameterList(SyntaxFactory.SeparatedList(_parameters)),
                 baseList: null,
                 constraintClauses: SyntaxFactory.List(_clauses),
                 members: SyntaxFactory.List(_members)
                 );
            return declaration;
        }

        public override string ToFormatCode(CodeContext? context)
        {
            throw new NotImplementedException();
        }

        public static ClassDeclarationSyntax ToSyntax(string code)
        {
            var syntaxNodes = CSharpSyntaxTree.ParseText(code).GetRoot().DescendantNodes();
            var declarations = syntaxNodes
                .OfType<ClassDeclarationSyntax>()
                .FirstOrDefault();
            return declarations;
        }
    }
}
