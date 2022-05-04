using System;

namespace IO.Unity3D.Source.IOC
{
    //******************************************
    // Qualifier used to specify the alias of the 
    // instance
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-03 22:00
    //******************************************
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property|AttributeTargets.Class)]
    public class Qualifier : Attribute
    {
        public string Name;

        public Qualifier(string name)
        {
            Name = name;
        }
    }
}