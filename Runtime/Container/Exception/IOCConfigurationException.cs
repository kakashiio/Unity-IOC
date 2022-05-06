using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace IO.Unity3D.Source.IOC
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-07 00:15
    //******************************************
    public class IOCConfigurationException : Exception
    {
        public IOCConfigurationException()
        {
        }

        protected IOCConfigurationException([NotNull] SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public IOCConfigurationException(string message) : base(message)
        {
        }

        public IOCConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}