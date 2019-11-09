using GodSharp.MessageBus.NamePipeSample;

namespace GodSharp.MessageBus.NamePipeClientSample1
{
    class Program
    {
        static void Main(string[] args)
        {
            NamedPipeTester.RunAsClient(1);
        }
    }
}
