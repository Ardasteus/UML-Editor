using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UML_Editor.Nodes;
using System.Reflection;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace UML_Editor.CodeGenerating
{
    public class CodeGenerator
    {
        public DirectoryInfo OutputDirectory { get; set; }
        CodeCompileUnit TargetUnit;
        CodeTypeDeclaration TargetClass;
        ClassDiagramNode ClassNode;
        public CodeGenerator(DirectoryInfo directory, ClassDiagramNode class_node)
        {
            OutputDirectory = directory;
            TargetUnit = new CodeCompileUnit();
            TargetClass = new CodeTypeDeclaration(class_node.NameTextBox.Name);
            ClassNode = class_node;
        }

        public bool GenerateClass()
        {
            CodeNamespace Namespaces = new CodeNamespace("Uml_Editor_Generated");
            Namespaces.Imports.Add(new CodeNamespaceImport("System"));
            TargetClass.IsClass = true;
            TargetClass.TypeAttributes = TypeAttributes.Public;
            Namespaces.Types.Add(TargetClass);
            TargetUnit.Namespaces.Add(Namespaces);
            return true;
        }
        private void AddProperty(PropertyNode propertyNode)
        {
            CodeMemberProperty newProperty = new CodeMemberProperty();
            newProperty.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            newProperty.Name = propertyNode.PropertyName;
            newProperty.HasGet = true;
            newProperty.HasSet = true;
            newProperty.Type = new CodeTypeReference(propertyNode.PropertyType);
            CodeMemberField field = new CodeMemberField()
            {
                Name = propertyNode.PropertyName.ToLower(),
                Type = new CodeTypeReference(propertyNode.PropertyType),
                Attributes = MemberAttributes.Private
            };
            newProperty.GetStatements.Add(new CodeMethodReturnStatement(
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(), field.Name)));
            newProperty.SetStatements.Add(
                new CodeAssignStatement(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(), field.Name),
                            new CodePropertySetValueReferenceExpression()));
            TargetClass.Members.Add(newProperty);
        }
        private void AddMethod(MethodNode methodNode)
        {
            CodeMemberMethod newMethod = new CodeMemberMethod();
            newMethod.Attributes = MemberAttributes.Public;
            newMethod.Name = methodNode.NameTextBox.Name;
            newMethod.ReturnType = new CodeTypeReference(methodNode.TypeTextBox.Name);
            newMethod.Parameters.Add(new CodeParameterDeclarationExpression)
        }
    }
}
