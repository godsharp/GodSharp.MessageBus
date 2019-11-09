using GodSharp.MessageBus.NamePipeSample;
using System;

namespace GodSharp.MessageBus.NamePipeServerSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello GodSharp.MessageBus.NamePipeServerSample!");

            NamedPipeTester.RunAsServer();
        }
    }
}
