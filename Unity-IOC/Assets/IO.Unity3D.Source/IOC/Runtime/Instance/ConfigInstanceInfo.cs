using System;
using System.Collections.Generic;

namespace IO.Unity3D.Source.IOC
{
    //******************************************
    // InstanceInfo
    // Declare how to instance a object, includes:
    //   1. set value for field or property
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-06 21:48
    //******************************************
    public class ConfigInstanceInfo
    {
        private readonly static List<IPropertyOrFieldSetter> EMPTY = new List<IPropertyOrFieldSetter>(0);
        
        public readonly Type Type;
        public readonly string QualifierName;
        public readonly IReadOnlyList<IPropertyOrFieldSetter> FieldOrPropertyInfos;

        public ConfigInstanceInfo(Type type, string qualifierName, List<IPropertyOrFieldSetter> fieldOrPropertyInfos)
        {
            Type = type;
            QualifierName = qualifierName;
            FieldOrPropertyInfos = fieldOrPropertyInfos ?? EMPTY;
        }
    }
}