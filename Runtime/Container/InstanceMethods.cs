using System.Collections.Generic;
using System.Reflection;

namespace IO.Unity3D.Source.IOC
{
    //******************************************
    // Methods of instance managed in the IOC Container
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-03 21:18
    //******************************************
    public class InstanceMethods
    {
        private List<MethodInfo> _Methods;

        public object Instance;

        public List<MethodInfo> Methods { get { return _Methods; } }
        
        public InstanceMethods(object obj, List<MethodInfo> methods)
        {
            Instance = obj;
            _Methods = new List<MethodInfo>(methods);
        }
    }
}