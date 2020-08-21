using System;
using System.Collections.Generic;

namespace UnitTestDemo
{
    ///Refer to three major functions of Simple Container///
    //public void BuildUp(object instance);
    //public IEnumerable<object> GetAllInstances(Type service);
    //public object GetInstance(Type service, string key);
    public class UnitIoC
    {
        public static Action<object> BuildUp =(obj)=>{ throw new NotImplementedException("No implementation for UnitIoC"); };
        public static Func<Type, IEnumerable<object>> GetAllInstance = (service) => { throw new NotImplementedException("No implementation for UnitIoC"); };
        public static Func<Type, string, object> GetInstance = (service, key) => { throw new NotImplementedException("No implementation for UnitIoC"); };
        public static T Get<T>(string key = null)
        {
            return (T)GetInstance(typeof(T), key);
        }
    }
}
