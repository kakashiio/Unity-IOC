using System;
using IO.Unity3D.Source.Reflection;

namespace IO.Unity3D.Source.IOC
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-06 21:42
    //******************************************
    public class IOCContainerBuilder
    {
        private ITypeContainer _TypeContainer;
        private IOCContainerConfiguration _Configuration;

        public IOCContainerBuilder(ITypeContainer typeContainer = null)
        {
            _TypeContainer = typeContainer;
        }

        public IOCContainerBuilder SetConfiguration(IOCContainerConfiguration configuration)
        {
            _Configuration = configuration;
            return this;
        }

        public IOCContainer Build()
        {
            return new IOCContainer(_TypeContainer, _Configuration);
        }
    }
}