using System.Linq;
using System.Collections.Generic;

namespace NamesCode.Generator.CodeBuilder
{
    public class StructWithReadonlyPropertiesCodeBuilder : CodeBuilder
    {
        public StructWithReadonlyPropertiesCodeBuilder(string headerComment) : base(headerComment) { }

        string thisTypeName;

        public void AddStuct(string structName)
        {
            base.AppendIndentLine(string.Format("public struct {0}", structName));
            IncreaseIndent();
            thisTypeName = structName;
        }

        public void AddMembers(IEnumerable<Member> members)
        {
            // member definitions
            var memberStrings = members.Select(p => string.Format("public readonly {0} {1};", p.TypeAlias, p.Name));
            foreach (var memberString in memberStrings)
                AppendIndentLine(memberString);

            if (memberStrings.Any())
                sb.AppendLine();

            // constructor arguments
            var arguments = string.Join(", ", members.Select(p => p.TypeAlias + " " + p.Name.ToLower()).ToArray());
            var constructor = string.Format("public {0}({1})", thisTypeName, arguments);
            AppendIndentLine(constructor);
            IncreaseIndent();

            // assignment
            var assignments = members.Select(p => string.Format("{0} = {1};", p.Name, p.Name.ToLower()));
            foreach (var assignment in assignments)
                AppendIndentLine(assignment);

            DecreaseIndent();
        }
    }
}