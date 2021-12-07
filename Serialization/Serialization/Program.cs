using System;
using System.IO;
namespace Serialization
{
    class Program
    {
        static void Main(string[] args)
        {
            var type = Console.ReadLine();
            switch(type.ToLower())
            {
                case "xml":
                    var xmlView = new XMLView(Console.ReadLine());
                    Console.WriteLine(xmlView.output);
                    break;
                case "json":
                    var jsonView = new JsonView(Console.ReadLine());
                    Console.WriteLine(jsonView.output);
                    break;
            }

        }
    }
}
