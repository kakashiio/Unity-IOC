namespace IO.Unity3D.Source.IOC
{
    //******************************************
    // If a instance implement this interface: 
    //   1. The `void OnContainerAware(IIOCContainer iocContainer)`
    //      will be invoked after instance and before set properties
    //      or fields.
    //
    //   2. The `void OnContainerDestroy(IIOCContainer iocContainer)`
    //      will be invoked before the container destroy.
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-08 14:27
    //******************************************
    public interface IContainerLifeCycle
    {
        void OnContainerAware(IIOCContainer iocContainer);

        void OnContainerDestroy(IIOCContainer iocContainer);
    }
}