using CognitiveServiceProxy;
using CognitiveServiceProxy.Models;
using HappyMeterConsoleTest;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;

namespace EndavaHappiness
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            //container.RegisterType<IDescriptorManager, DescriptorManagerMongoDb>();
            container.LoadConfiguration();

            var program = container.Resolve<MainProgram>();
            program.Run();
            
        }

    }
}
