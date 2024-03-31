using System;

namespace Danqzq.UnityGo
{
    internal static class GoParser
    {
        internal static void Parse(string goFile)
        {
            var contents = System.IO.File.ReadAllText(goFile);
            var config = UnityEngine.Resources.Load<GoConvertConfig>("GoConvertConfig");
            var hasNamespace = !string.IsNullOrEmpty(config.@namespace);
            var output = hasNamespace ? "namespace " + config.@namespace + "\n{\n" : "";
            var lines = contents.Split('\n');
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (!line.StartsWith("type"))
                    continue;
                string type;
                (type, i) = CreateType(ref lines, i, hasNamespace ? "    " : "");
                output += type;
            }
            if (hasNamespace) 
                output += "}\n";
            
            if (config.outputFile != null) 
                System.IO.File.WriteAllText(config.outputFile, output);
        }

        private static (string, int) CreateType(ref string[] lines, int lineStart, string tabs)
        {
            var words = lines[lineStart].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var typeName = words[1];

            var result = tabs + "[System.Serializable]\n" + tabs + "public class ";
            result += typeName + " \n" + tabs + "{\n";
            
            int i;
            for (i = lineStart + 1; lines[i].Trim() != "}"; i++)
            {
                var line = lines[i];
                var fieldLine = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (fieldLine.Length < 2)
                    continue;
                var fieldName = fieldLine[0];
                var fieldType = fieldLine[1];
                if (fieldType == "interface{}") 
                    fieldType = "object";
                if (fieldLine.Length > 2 && fieldLine[2].Contains("json:")) 
                    fieldName = fieldLine[2].Split("json:\"")[1].Split("\"")[0];
                if (fieldType.StartsWith("[]")) 
                    fieldType = fieldType.Substring(2) + "[]";
                result += tabs + $"    public {fieldType} {fieldName};\n";
            }
            
            result += tabs + "}\n\n";
            return (result, i);
        }
    }
}