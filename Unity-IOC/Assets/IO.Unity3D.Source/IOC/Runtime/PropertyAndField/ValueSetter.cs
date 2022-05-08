using IO.Unity3D.Source.Reflection;

namespace IO.Unity3D.Source.IOC
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-06 21:49
    //******************************************
    public class ValueSetter : AbsSinglePropertyOrFieldSetter
    {
        public object Value;

        public ValueSetter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        protected override void Set(IIOCContainer iocContainer, Instance instance, IPropertyOrField propertyOrField)
        {
            propertyOrField.SetValue(instance.Object, Value);
        }
    }
}