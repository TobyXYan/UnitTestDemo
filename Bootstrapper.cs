using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Xml.Serialization;
using UnitTestDemo.Common;
using UnitTestDemo.ViewModels;
using UnitTestLibrary;

namespace UnitTestDemo
{
    public class Bootstrapper:BootstrapperBase
    {
        private readonly SimpleContainer Container;
        public Bootstrapper():base(true)
        {
            Initialize();
            Container              = new SimpleContainer();
            Configure();
            if(!LoadExtensions())
            {
                return;
            }
            UnitIoC.BuildUp        = BuildUp;
            UnitIoC.GetInstance    = GetInstance;
            UnitIoC.GetAllInstance = GetAllInstances;
        }

        protected new void Configure()
        {
            Container.Singleton<IEventAggregator, EventAggregator>();
            Container.Singleton<IWindowManager, WindowManager>();
            Container.Singleton<ShellViewModel>();
            Container.PerRequest<ICalculateService,CalculateService>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return Container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return Container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            Container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        private bool LoadExtensions()
        {
            bool bRet = true;
            do
            {
                var dirPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var strExtAssembly = "UnitTestExtension.dll";
                var assembly = Assembly.LoadFile(Path.Combine(dirPath, strExtAssembly));
                if (!AssemblySource.Instance.Contains(assembly))
                    AssemblySource.Instance.Add(assembly);
                var allTypes = assembly.GetTypes();
                foreach (var type in allTypes)
                {
                    var interfaces = type.GetInterfaces();
                    if ( typeof(IConfigureService).IsAssignableFrom(type))
                    {
                        IConfigureService extensionConfigService = (IConfigureService)Activator.CreateInstance(type);
                        if (extensionConfigService == null)
                        {
                            bRet = false;
                            break;
                        }
                        extensionConfigService.Configure(Container);
                    }
                }
            } while (false);

            return bRet;
        }
    }
}
