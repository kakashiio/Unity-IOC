using System;

namespace IO.Unity3D.Source.IOC
{
    //******************************************
    // Field or property with Autowired will be
    // injected automatically by the IOC Container
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-03 21:58
    //******************************************
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property)]
    public class Autowired : Attribute
    {
    }
}