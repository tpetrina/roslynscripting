using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.Scripting.CSharp;
using System;

namespace simplerepl
{
    class Program
    {
        static void Main(string[] args)
        {
            Script<object> script = CSharpScript.Create(string.Empty);

            while (true)
            {
                Console.Write("> ");
                var code = Console.ReadLine();

                var newScript = script.ContinueWith(code);

                ScriptState<object> result = null;
                try
                {
                    result = newScript.RunAsync().Result;
                    script = newScript;
                }
                catch (Exception ex)
                {
                    var color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(ex.ToString());
                    Console.ForegroundColor = color;
                }

                if (result != null)
                    Console.WriteLine(result.ReturnValue);
            }
        }
    }
}
