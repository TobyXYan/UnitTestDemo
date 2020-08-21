using Caliburn.Micro;
using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Instrumentation;
using System.Reflection;
using UnitTestDemo;
using UnitTestDemo.Common;
using UnitTestDemo.ViewModels;
using UnitTestLibrary;
using UnitTestLibrary.ViewModels;

namespace UnitTest
{
   [TestFixture]
    public class ShellViewModelTest
    {
         #region fields
        private ShellViewModel _shellVm;
        #endregion

        [SetUp]
        public void Init()
        {
            var bootstrapper = new RunOptBootstrapper();
            _shellVm = new ShellViewModel();
        }
          
        [TearDown]
        public void CleanUp()
        {
            _shellVm = null;
        }

        [Category("ShellViewModel Test")]
        [Test]
        public void  OnCalculate_CalculateAge_Incorrect()
        {
            //Test Setup / MockData
            _shellVm.Age = 22;

            //Test Methods Execution
            _shellVm.OnCalculate();


            //Test Results Verification
            Assert.AreEqual(_shellVm.Age, 45);

        }
    }

    
    class RunOptBootstrapper
    {
        protected readonly SimpleContainer Container = new SimpleContainer();

        public RunOptBootstrapper()
        {
            Configure();
            LoadExtensions();//Configure TestOrderSequence in SE extension
            UnitIoC.GetInstance     = GetInstance;
            UnitIoC.GetAllInstance  = GetAllInstance;
            UnitIoC.BuildUp         = BuildUp;
        }

        protected virtual void Configure()
        {
            Container.Singleton<IEventAggregator, EventAggregator>();
        }

        protected virtual object GetInstance(Type service, string key)
        {
            if (service == typeof(IEventAggregator))
            {
                return A.Fake<IEventAggregator>();
            }

            if (service == typeof(ICalculateService))
            {
               var calService = A.Fake<ICalculateService>();
                int val = 0;
                A.CallTo(()=>calService.DoMathWork(val)).
                    WithAnyArguments().
                    AssignsOutAndRefParameters(24);
                return calService;
            }

            if (service == typeof(BaseCommentViewModel))
            {
                var viewModel = A.Fake<BaseCommentViewModel>();
                return viewModel;
            }
            throw new InvalidOperationException("Could not locate any instance.");
        }

        protected virtual IEnumerable<object> GetAllInstance(Type service)
        {
            return Container.GetAllInstances(service);
        }

        protected virtual void BuildUp(object instance)
        {
            Container.BuildUp(instance);
        }

        private void LoadExtensions()
        {
            do
            {
                var dirPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string extensionAssemblyName = "UnitTestExtension.dll";
                var assembly = Assembly.LoadFile(Path.Combine(dirPath, extensionAssemblyName));
                if (!AssemblySource.Instance.Contains(assembly))
                    AssemblySource.Instance.Add(assembly);
                var allTypes = assembly.GetTypes();
                foreach (var type in allTypes)
                {
                    var interfaces = type.GetInterfaces();
                    if (typeof(IConfigureService).IsAssignableFrom(type))
                    {
                        IConfigureService extensionConfigService = (IConfigureService)Activator.CreateInstance(type);
                        if (extensionConfigService == null)
                        {
                            break;
                        }
                        extensionConfigService.Configure(Container);
                    }
                }
            } while (false);
        }
    }
}
