using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace IO.Unity3D.Source.IOC
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-06 21:47
    //******************************************
    public class IOCContainerConfiguration
    {
        public readonly Dictionary<InstanceID, InstanceInfo> BeanInfos;

        public IOCContainerConfiguration(List<InstanceInfo> beanInfos)
        {
            if (beanInfos == null || beanInfos.Count == 0)
            {
                return;    
            }
            
            BeanInfos = new Dictionary<InstanceID, InstanceInfo>();
            foreach (InstanceInfo beanInfo in beanInfos)
            {
                var beanID = new InstanceID(beanInfo.Type, beanInfo.QualifierName);
                if (BeanInfos.ContainsKey(beanID))
                {
                    Debug.LogError($"Found duplicate BeanInfo Type={beanID.Type} Qualifier={beanID.QualifierName}");
                    continue;
                }
                BeanInfos.Add(beanID, beanInfo);
            }
        }

        public bool ContainsInstanceConfig(Type type)
        {
            var qualifier = type.GetCustomAttributes(typeof(Qualifier)) as Qualifier;
            var instanceID = new InstanceID(type, qualifier == null ? Qualifier.DEFAULT : qualifier.Name);
            return BeanInfos.ContainsKey(instanceID);
        }
    }
}