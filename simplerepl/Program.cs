using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.Scripting.CSharp;
using System;

namespace simplerepl
{
    class Program
    {
        private static CSharpParseOptions options;

        static void Main(string[] args)
        {
            options = new CSharpParseOptions(LanguageVersion.CSharp6,
                DocumentationMode.Parse,
                SourceCodeKind.Interactive,
                null);

            Script<object> script = CSharpScript.Create(string.Empty);

            while (true)
            {
                Console.Write("> ");
                var code = Console.ReadLine();

                var newScript = script.ContinueWith(code);

                ScriptState<object> result = null;
                try
                {
                    bool isComplete = IsCompleteSubmission(code);

                    if (!isComplete)
                    {
                        Console.WriteLine("Incomplete submission");
                    }
                    else
                    {
                        result = newScript.RunAsync().Result;
                        script = newScript;
                    }
                }
                catch (Exception ex)
                {
                    var error = ex.ToString();
                    WriteError(error);
                }

                if (result != null && result.ReturnValue != null)
                    Console.WriteLine(result.ReturnValue);
            }
        }

        private static void WriteError(string error)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(error);
            Console.ForegroundColor = color;
        }

        private static bool IsCompleteSubmission(string code)
        {
            var syntaxTree = SyntaxFactory.ParseSyntaxTree(code, options: options);
            var isComplete = SyntaxFactory.IsCompleteSubmission(syntaxTree);
            return isComplete;
        }
    }
}
