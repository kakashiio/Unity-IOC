namespace IO.Unity3D.Source.IOC
{
    //******************************************
    // If an instance impplement this interface,
    // methods defined in the interface will be invoke
    // in their time.
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-08 14:22
    //******************************************
    public interface IInstanceLifeCycle
    {
        /// <summary>
        /// Invoked after this object is created and before the properties & fields are injected.
        /// </summary>
        void AfterInstance();
        
        /// <summary>
        /// Invoked after this object's properties & fields are injected. 
        /// </summary>
        void AfterPropertiesOrFieldsSet();

        /// <summary>
        /// Invoked after all objects' properties & fields are injected.
        /// </summary>
        void AfterAllInstanceSet();
    }
}