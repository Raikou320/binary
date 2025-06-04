using System;
using System.IO;
using System.Collections.Generic;

public class Program
{
    static Dictionary<string, string> stringVarsList = new Dictionary<string, string>();
    static Dictionary<string, int> intVarsList = new Dictionary<string, int>();
    public static void Main(string[] args)
    {
        if (args.Length != 1) return;
        string[] lines = File.ReadAllLines(args[0]);
        foreach (string line in lines)
        {
            Interpret(line);
        }
    }
    static void Interpret(string line)
    {
        if (line.StartsWith("print "))
        {
            string content = line.Substring(6);
            if (content.StartsWith("\"") && content.EndsWith("\""))
            {
                string[] parts = content.Split("\"", 3);
                Console.WriteLine(parts[1]);
                return;
            }
            if (intVarsList.ContainsKey(content))
            {
                Console.WriteLine(intVarsList[content]);
                return;
            }
            if (stringVarsList.ContainsKey(content))
            {
                Console.WriteLine(stringVarsList[content]);
                return;
            }
        }
        if (line.StartsWith("var "))
        {
            var content = line.Substring(4);
            string[] value = content.Split(" = ", 2);
            if (int.TryParse(value[1], out int intValue))
            {
                intVarsList.Add(value[0], intValue);
                return;
            }
            else if (value[1].StartsWith("\"") && value[1].EndsWith("\""))
            {
                stringVarsList.Add(value[0], value[1].Trim('"'));
                return;
            }
        }
        Console.WriteLine($"Unknown command, {line}");
    }
}