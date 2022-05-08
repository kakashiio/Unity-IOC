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
    public interface IPropertyOrFieldSetter
    {
        void Set(IIOCContainer iocContainer, Instance instance);
    }
}