using System;
using System.Collections.Generic;
using System.Reflection;
using IO.Unity3D.Source.Reflection;

namespace IO.Unity3D.Source.IOC
{
    //******************************************
    //  Wrapper for config or auto inject mechanism
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-08 14:00
    //******************************************
    public class InstanceInfo
    {
        public readonly InstanceID InstanceID;
        public readonly IReadOnlyList<IPropertyOrFieldSetter> PropertyOrFieldInfos;

        public InstanceInfo(InstanceID instanceID, IReadOnlyList<IPropertyOrFieldSetter> propertyOrFieldInfos)
        {
            InstanceID = instanceID;
            PropertyOrFieldInfos = propertyOrFieldInfos;
        }

        public static InstanceInfo Create(ConfigInstanceInfo configInstanceInfo)
        {
            var instanceID = new InstanceID(configInstanceInfo.Type, configInstanceInfo.QualifierName);
            var instanceInfo = new InstanceInfo(instanceID, configInstanceInfo.PropertyOrFieldInfos);
            return instanceInfo;
        }

        public static InstanceInfo Create(Type type)
        {
            var qualifier = type.GetCustomAttribute(typeof(Qualifier)) as Qualifier;
            var instanceID = new InstanceID(type, qualifier == null ? Qualifier.DEFAULT : qualifier.Name);
            
            List<IPropertyOrFieldSetter> propertyAndFieldSetters = new List<IPropertyOrFieldSetter>();
            
            var propertiesOrFields = Reflections.GetPropertiesAndFields<Autowired>(type);
            foreach (var propertiesOrField in propertiesOrFields)
            {
                var fieldQualifier = propertiesOrField.GetCustomAttribute<Qualifier>();
                propertyAndFieldSetters.Add(new ReferenceSetter(propertiesOrField.Name, null, fieldQualifier == null ? Qualifier.DEFAULT : fieldQualifier.Name));
            }
            
            var instanceInfo = new InstanceInfo(instanceID, propertyAndFieldSetters);
            return instanceInfo;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj == this)
            {
                return true;
            }

            var instanceInfo = obj as InstanceInfo;
            if (instanceInfo == null)
            {
                return false;
            }

            return InstanceID.Equals(instanceInfo.InstanceID);
        }

        public override int GetHashCode()
        {
            return InstanceID.GetHashCode();
        }
    }
}