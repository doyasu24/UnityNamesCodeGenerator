#pragma warning disable XS0001

using System.Text;
using System;
using System.Linq;
using System.Collections.Generic;

namespace NamesCodeGenerator
{
    public struct Property
    {
        public readonly string Type;
        public readonly string Name;
        Property(string primitiveTypeName, string propertyNameAsCamelCase)
        {
            Type = primitiveTypeName;
            Name = propertyNameAsCamelCase;
        }

        public static readonly Property StringName = new Property("string", "Name");
        public static readonly Property IntIndex = new Property("int", "Index");
        public static readonly Property IntId = new Property("int", "Id");
    }

    public static class CodeGenerator
    {
        const string headerComment = "// Generated code by NamesCodeGenerator";
        const string indent = "    ";

        public static string GenerateConstClass(string typeName, string[] namesAsCamelCase)
        {
            // // Generated code by NamesCodeGenerator
            // public static class typeName
            // {
            //     public const string name = "Name";
            //     ...
            // }
            var parameters = namesAsCamelCase
                .Select(name => "public const string " + ConvertToVariableName(name) + " = " + "\"" + name + "\";");
            return GenerateType(true, true, typeName, parameters);
        }

        static string GenerateStructProperty(string typeName, string name, NameWithNumber arguments)
        {
            return "public static readonly " + typeName + " " + ConvertToVariableName(name) + " = new " + typeName + "(\"" + arguments.Name + "\", " + arguments.Number + ");";
        }

        static string GenerateStructProperty(string typeName, string name, string argument)
        {
            return "public static readonly " + typeName + " " + ConvertToVariableName(name) + " = new " + typeName + "(\"" + argument + "\");";
        }

        public static string GenerateParentStaticClass(string parentClassName, string typeName, string[] names)
        {
            // // Generated code by NamesCodeGenerator
            // public static class ParentClass
            // {
            //     public static readonly ChildStruct A = new ChildStruct("A");
            //     public static readonly ChildStruct B = new ChildStruct("B");
            //     ...
            //     public static readonly ChildStructs[] = { A, B, ... };
            // }
            var parameters = names.Select(n => GenerateStructProperty(typeName, n, n)).ToList();
            if(parameters.Count != 0)
                parameters.Add("");
            var variables = string.Join(", ", names.Select(ConvertToVariableName).ToArray());
            parameters.Add(indent + "public static readonly " + typeName + "[] Names = { " + variables + " };");

            return GenerateType(true, true, parentClassName, parameters);
        }

        public static string GenerateParentStaticClass(string parentClassName, string typeName, NameWithNumber[] nameWithNumbers)
        {
            // // Generated code by NamesCodeGenerator
            // public static class ParentClass
            // {
            //     public static readonly ChildStruct A = new ChildStruct("A", 1);
            //     public static readonly ChildStruct B = new ChildStruct("B", 2);
            //     ...
            //     public static readonly ChildStructs[] = { A, B, ... };
            // }
            var parameters = nameWithNumbers.Select(n => GenerateStructProperty(typeName, n.Name, n)).ToList();
            if (parameters.Count != 0)
                parameters.Add("");
            var variables = string.Join(", ", nameWithNumbers.Select(n => n.Name).Select(ConvertToVariableName).ToArray());
            parameters.Add("public static readonly " + typeName + "[] Names = { " + variables + " };");
            return GenerateType(true, true, parentClassName, parameters);
        }

        /// <summary>
        /// Generates the struct.
        /// </summary>
        /// <returns>The struct.</returns>
        /// <param name="typeName">Type name.</param>
        /// <param name="properties">Properties.</param>
        public static string GenerateStruct(string typeName, params Property[] properties)
        {
            // // Generated code by NamesCodeGenerator
            // public struct TypeName
            // {
            //     public readonly string Name;
            //     ...
            // 
            //     public TypeName(string name, ...)
            //     {
            //         Name = name;
            //         ...
            //     }
            // }

            // property definitions
            var parameters = properties.Select(p => "public readonly " + p.Type + " " + p.Name + ";").ToList();
            parameters.Add("");

            // constructor arguments
            parameters.Add("public " + typeName + "(" + string.Join(", ", properties.Select(p => p.Type + " " + p.Name.ToLower()).ToArray()) + ")");

            // assignment
            parameters.Add("{");
            parameters = parameters.Concat(properties.Select(p => indent + p.Name + " = " + p.Name.ToLower() + ";")).ToList();
            parameters.Add("}");
            return GenerateType(false, false, typeName, parameters);
        }

        static string GenerateType(bool isClass, bool isStatic, string typeName, IEnumerable<string> parameters)
        {
            if (!isClass && isStatic)
                throw new ArgumentException();

            var sb = new StringBuilder();
            sb.AppendLine(headerComment);
            sb.Append("public ");
            if (isStatic)
                sb.Append("static ");
            if (isClass)
                sb.Append("class ");
            else
                sb.Append("struct ");

            sb.Append(typeName);
            sb.AppendLine();
            sb.AppendLine("{");
            foreach (var parameter in parameters)
            {
                if(!string.IsNullOrEmpty(parameter))
                    sb.Append(indent);
                sb.AppendLine(parameter);
            }
            sb.AppendLine("}");
            return sb.ToString();
        }

        public static string AddNamespace(string namespaceName, string code)
        {
            // namespace namespaceName
            // {
            //     ...
            // }
            var lines = code.Split('\n');
            var sb = new StringBuilder(lines.Length + 3);
            sb.AppendLine("namespace " + namespaceName);
            sb.AppendLine("{");
            foreach (var line in lines)
            {
                if (!string.IsNullOrEmpty(line))
                    sb.Append(indent);
                sb.AppendLine(line);
            }
            sb.AppendLine("}");
            return sb.ToString();
        }

        static string ConvertToVariableName(string name)
        {
            return new String(name.Where(c => !invalidCharsInVariableName.Contains(c)).ToArray());
        }

        static readonly char[] invalidCharsInVariableName = new[]
        {
            ' ',
            '!',
            '\"',
            '#',
            '$',
            '%',
            '&',
            '\'',
            '(',
            ')',
            '-',
            '=',
            '^',
            '~',
            '¥',
            '|',
            '[',
            '{',
            '@',
            '`',
            ']',
            '}',
            ':',
            '*',
            ';',
            '+',
            '/',
            '?',
            '.',
            '>',
            ',',
            '<'
        };
    }
}
#pragma warning restore XS0001
