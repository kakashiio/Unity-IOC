using System;

namespace IO.Unity3D.Source.IOC
{
    //******************************************
    // A class with IOCComponent attribute will
    // be managed by the IOCContainer.  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-03 21:40
    //******************************************
    [AttributeUsage(AttributeTargets.Class)]
    public class IOCComponent : Attribute
    {
    }
}