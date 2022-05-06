using System;

namespace IO.Unity3D.Source.IOC
{
    //******************************************
    // Identify unique instance
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-06 21:59
    //******************************************
    public class InstanceID
    {
        public Type Type;
        public string QualifierName;

        public InstanceID(Type type, string qualifierName = Qualifier.DEFAULT)
        {
            Type = type;
            QualifierName = qualifierName ?? Qualifier.DEFAULT;
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

            var beanID = obj as InstanceID;
            if (beanID == null)
            {
                return false;
            }
            
            return Type.Equals(beanID.Type) && QualifierName.Equals(beanID.QualifierName);
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode() ^ QualifierName.GetHashCode();
        }
    }
}