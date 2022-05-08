namespace IO.Unity3D.Source.IOC
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-06 23:36
    //******************************************
    public class Instance
    {
        public readonly InstanceInfo InstanceInfo;
        public readonly object Object;

        public Instance(InstanceInfo instanceInfo, object o)
        {
            InstanceInfo = instanceInfo;
            Object = o;
        }
    }
}