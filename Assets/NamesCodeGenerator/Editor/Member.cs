using System;

namespace NamesCodeGenerator
{
    public struct Member
    {
        public readonly Type Type;
        public readonly string Name;

        public Member(Type type, string propertyNameAsCamelCase)
        {
            Type = type;
            Name = propertyNameAsCamelCase;
        }

        public string TypeAlias {
            get {
                if (Type == typeof(string))
                    return "string";
                if (Type == typeof(int))
                    return "int";
                return Type.FullName;
            }
        }

        public static readonly Member StringName = new Member(typeof(string), "Name");
        public static readonly Member IntIndex = new Member(typeof(int), "Index");
        public static readonly Member IntId = new Member(typeof(int), "Id");
    }
}
