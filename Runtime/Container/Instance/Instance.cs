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
        public readonly string QualifierName;
        public readonly object Object;

        public Instance(string qualifierName, object o)
        {
            QualifierName = qualifierName ?? Qualifier.DEFAULT;
            Object = o;
        }
    }
}