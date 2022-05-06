namespace IO.Unity3D.Source.IOC
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-06 21:49
    //******************************************
    public class FieldOrPropertyInfo
    {
        public string Name;
        public object Value;

        public FieldOrPropertyInfo(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}