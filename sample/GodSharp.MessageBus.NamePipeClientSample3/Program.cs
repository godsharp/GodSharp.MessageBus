using GodSharp.MessageBus.NamePipeSample;

namespace GodSharp.MessageBus.NamePipeClientSample3
{
    class Program
    {
        static void Main(string[] args)
        {
            NamedPipeTester.RunAsClient(3);
        }
    }
}
