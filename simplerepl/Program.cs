using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.Scripting.CSharp;
using System;

namespace simplerepl
{
    class Program
    {
        static void Main(string[] args)
        {
            Script<object> script = CSharpScript.Create("");

            while (true)
            {
                Console.Write("> ");
                var code = Console.ReadLine();

                script = script.WithPrevious(script)
                    .WithCode(code);
                var result = script.RunAsync().ReturnValue;

                Console.WriteLine(result.Result);
            }
        }
    }
}
