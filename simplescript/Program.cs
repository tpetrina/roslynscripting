using Microsoft.CodeAnalysis.Scripting.CSharp;
using System;

namespace simplescript
{
    class Program
    {
        static void Main(string[] args)
        {
            var script = CSharpScript.Create("\"Hello World\"");
            var result = script.RunAsync().ReturnValue;
            Console.WriteLine(result.Result);

            // async version

            //result.ReturnValue
            //    .ContinueWith(t =>
            //    {
            //        Console.WriteLine(result.ReturnValue.Result);
            //    });

            Console.ReadKey();
        }
    }
}
