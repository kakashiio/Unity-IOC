using System;
using System.Collections.Generic;
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
        public readonly HashSet<InstanceInfo> InstanceInfos = new HashSet<InstanceInfo>();

        public IOCContainerConfiguration(List<ConfigInstanceInfo> configInstanceInfos = null)
        {
            if (configInstanceInfos == null || configInstanceInfos.Count == 0)
            {
                return;    
            }
            
            foreach (ConfigInstanceInfo configInstanceInfo in configInstanceInfos)
            {
                AddConfigInstanceInfo(configInstanceInfo);
            }
        }

        public IOCContainerConfiguration AddConfigInstanceInfo<T>()
        {
            return AddConfigInstanceInfo(new ConfigInstanceInfo(typeof(T)));
        }
        
        public IOCContainerConfiguration AddConfigInstanceInfo<T>(string qualifier)
        {
            return AddConfigInstanceInfo(new ConfigInstanceInfo(typeof(T), qualifier));
        }
        
        public IOCContainerConfiguration AddConfigInstanceInfo<T>(string qualifier, List<IPropertyOrFieldSetter> fieldOrPropertyInfos)
        {
            return AddConfigInstanceInfo(new ConfigInstanceInfo(typeof(T), qualifier, fieldOrPropertyInfos));
        }
        public IOCContainerConfiguration AddConfigInstanceInfo<T>(string qualifier, params IPropertyOrFieldSetter[] fieldOrPropertyInfos)
        {
            List<IPropertyOrFieldSetter> list = null;
            if (fieldOrPropertyInfos != null && fieldOrPropertyInfos.Length > 0)
            {
                list = new List<IPropertyOrFieldSetter>(fieldOrPropertyInfos);
            }
            return AddConfigInstanceInfo(new ConfigInstanceInfo(typeof(T), qualifier, list));
        }
        
        public IOCContainerConfiguration AddConfigInstanceInfo<T>(List<IPropertyOrFieldSetter> fieldOrPropertyInfos)
        {
            return AddConfigInstanceInfo(new ConfigInstanceInfo(typeof(T), fieldOrPropertyInfos));
        }
        public IOCContainerConfiguration AddConfigInstanceInfo<T>(params IPropertyOrFieldSetter[] fieldOrPropertyInfos)
        {
            return AddConfigInstanceInfo<T>(Qualifier.DEFAULT, fieldOrPropertyInfos);
        }

        public IOCContainerConfiguration AddConfigInstanceInfo(ConfigInstanceInfo configInstanceInfo)
        {
            var instanceInfo = InstanceInfo.Create(configInstanceInfo);
            if (InstanceInfos.Contains(instanceInfo))
            {
                Debug.LogError($"Found duplicate InstanceInfo {instanceInfo.InstanceID}");
                return this;
            }
            InstanceInfos.Add(instanceInfo);
            return this;
        }
    }
}