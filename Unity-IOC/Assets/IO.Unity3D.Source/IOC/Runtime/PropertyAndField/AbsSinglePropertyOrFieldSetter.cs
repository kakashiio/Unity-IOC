using IO.Unity3D.Source.Reflection;

namespace IO.Unity3D.Source.IOC
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-08 16:56
    //******************************************
    public abstract class AbsSinglePropertyOrFieldSetter : IPropertyOrFieldSetter
    {
        public virtual string Name { get; protected set; }

        public void Set(IIOCContainer iocContainer, Instance instance)
        {
            IPropertyOrField propertyOrField = Reflections.GetPropertyOrField(instance.InstanceInfo.InstanceID.Type, Name);
            Set(iocContainer, instance, propertyOrField);
        }

        protected abstract void Set(IIOCContainer iocContainer, Instance instance, IPropertyOrField propertyOrField);
    }
}