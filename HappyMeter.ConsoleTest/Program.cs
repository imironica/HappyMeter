using HappyMeterConsoleTest;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace EndavaHappiness
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            container.LoadConfiguration();
            var program = container.Resolve<MainProgram>();
            program.Run();
        }

    }
}
