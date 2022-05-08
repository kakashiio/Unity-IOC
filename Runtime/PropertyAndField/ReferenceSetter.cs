using System;
using IO.Unity3D.Source.Reflection;

namespace IO.Unity3D.Source.IOC
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-08 15:44
    //******************************************
    public class ReferenceSetter : AbsSinglePropertyOrFieldSetter
    {
        private Type _Type;
        private string _Qualifier;

        public ReferenceSetter(string name, Type type = null, string qualifier = Qualifier.DEFAULT)
        {
            _Type = type;
            _Qualifier = qualifier ?? Qualifier.DEFAULT;
            Name = name;
        }
        
        public ReferenceSetter(string name, string qualifier) : this(name, null, qualifier)
        {
        }

        protected override void Set(IIOCContainer iocContainer, Instance instance, IPropertyOrField propertyOrField)
        {
            Set(iocContainer, instance, propertyOrField, _Qualifier, _Type);
        }

        public static void Set(IIOCContainer iocContainer, Instance instance, IPropertyOrField propertyOrField, string qualifier = Qualifier.DEFAULT, Type type = null)
        {
            var qualifierName = qualifier == null ? Qualifier.DEFAULT : qualifier;
            object value = iocContainer.FindObjectOfType(type ?? propertyOrField.GetFieldOrPropertyType(), qualifierName);
            propertyOrField.SetValue(instance.Object, value);
        }
    }
}