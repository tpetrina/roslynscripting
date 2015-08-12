using Microsoft.CodeAnalysis.Scripting.CSharp;
using System;

namespace simplescript
{
    class Program
    {
        static void Main(string[] args)
        {
            var script = CSharpScript.Create("2+2");
            var result = script.RunAsync();

            result.ReturnValue
                .ContinueWith(t =>
                {
                    Console.WriteLine(result.ReturnValue.Result);
                });

            Console.ReadKey();
        }
    }
}
