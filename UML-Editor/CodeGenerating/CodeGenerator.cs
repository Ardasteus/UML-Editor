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
        public string Output { get; set; }
        CodeCompileUnit TargetUnit;
        CodeTypeDeclaration TargetClass;
        ClassDiagramNode ClassNode;
        public CodeGenerator(string output, ClassDiagramNode class_node)
        {
            Output = output;
            TargetUnit = new CodeCompileUnit();
            TargetClass = new CodeTypeDeclaration(class_node.NameTextBox.Text);
            ClassNode = class_node;
        }

        public bool GenerateClass()
        {
           // try
            //{
                CodeNamespace Namespaces = new CodeNamespace("Uml_Editor_Generated");
                Namespaces.Imports.Add(new CodeNamespaceImport("System"));
                TargetClass.IsClass = true;
                TargetClass.TypeAttributes = TypeAttributes.Public;
                Namespaces.Types.Add(TargetClass);
                TargetUnit.Namespaces.Add(Namespaces);
                foreach (PropertyNode item in ClassNode.Properties)
                {
                    AddProperty(item);
                }
                foreach (MethodNode item in ClassNode.Methods)
                {
                    AddMethod(item);
                }
            GenerateConstructor();
            GenerateCode();
                return true;
            //}
            //catch (Exception)
            //{
            //    throw;
            //    return false;
            //}
        }
        private void AddProperty(PropertyNode propertyNode)
        {
            CodeMemberProperty newProperty = new CodeMemberProperty();
            newProperty.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            newProperty.Name = propertyNode.Name;
            newProperty.HasGet = true;
            newProperty.HasSet = true;
            newProperty.Type = new CodeTypeReference(propertyNode.Type);
            CodeMemberField field = new CodeMemberField()
            {
                Name = propertyNode.Name.ToLower(),
                Type = new CodeTypeReference(propertyNode.Type),
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
            newMethod.Name = methodNode.NameTextBox.Text;
            if(methodNode.TypeTextBox.Text == "void")
                newMethod.ReturnType = new CodeTypeReference(   );
            else
                newMethod.ReturnType = new CodeTypeReference(new CodeTypeParameter(methodNode.TypeTextBox.Text));
            List<string> param_types = methodNode.ArgumentsTextBox.Text.Split(',').Select(x => x.Split(' ')[0]).ToList();
            List<string> param_names = methodNode.ArgumentsTextBox.Text.Split(',').Select(x => x.Split(' ')[1]).ToList();
            for (int i = 0; i < param_names.Count; i++)
            {
                newMethod.Parameters.Add(new CodeParameterDeclarationExpression(param_types[i], param_names[i]));
                TargetClass.Members.Add(newMethod);
            }
        }
        private void GenerateConstructor()
        {
            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            foreach (var item in TargetClass.Members)
            {
                if(item is CodeMemberProperty prop)
                {
                    constructor.Parameters.Add(new CodeParameterDeclarationExpression(
                        prop.Type, "_" + prop.Name.ToLower()));
                    CodeFieldReferenceExpression fieldReference =
                        new CodeFieldReferenceExpression(
                            new CodeThisReferenceExpression(), prop.Name);
                    constructor.Statements.Add(new CodeAssignStatement(fieldReference,
                        new CodeArgumentReferenceExpression("_" + prop.Name)));
                }
            }
            TargetClass.Members.Add(constructor);
        }
        private void GenerateCode()
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            FileInfo outputfile = new FileInfo(Output);
            options.BracingStyle = "C";
            using (StreamWriter sourceWriter = new StreamWriter(outputfile.FullName))
            {
                provider.GenerateCodeFromCompileUnit(
                    TargetUnit, sourceWriter, options);
                sourceWriter.Close();
            }
            
        }
    }
}
