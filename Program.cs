using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            var cb = new CodeBuilder("Person")
                .AddField("Name","string")
                .AddField("Age", "int");
            Console.WriteLine(cb);
            Console.ReadKey();
        }
    }
    public class MyField {
        public string FieldName { get; set; }
        public string FieldType { get; set; }

        public MyField(string fieldName, string fieldType)
        {
            FieldName = fieldName;
            FieldType = fieldType;
        }
        public override string ToString() {
            return $"public {FieldType} {FieldName};";
        }
    }
    public class MyClass {
        public string ClassName { get; set; }
        public List<MyField> Fields { get; set; } = new List<MyField>();
        public MyClass(string className)
        {
            ClassName = className;
        }
        public void AddField(string name, string type) {
            this.Fields.Add(new MyField(name, type));
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"public class {ClassName}");
            sb.AppendLine("{");
            foreach (var item in Fields)
            {
                sb.AppendLine($"  {item.ToString()}");
            }
            sb.AppendLine("}");
            return sb.ToString();
        }
    }

    public class CodeBuilder {
        private MyClass cl;
        public CodeBuilder(string className)
        {
            cl = new MyClass(className);
        }
        public CodeBuilder AddField(string name,string type) {
            cl.AddField(name, type);
            return this;
        }
        public override string ToString()
        {
            return cl.ToString();
        }
    }
}
