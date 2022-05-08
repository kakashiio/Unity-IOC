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
        private static readonly HashSet<InstanceInfo> EMPTY = new HashSet<InstanceInfo>();
        
        public readonly HashSet<InstanceInfo> InstanceInfos = EMPTY;

        public IOCContainerConfiguration(List<ConfigInstanceInfo> configInstanceInfos = null)
        {
            if (configInstanceInfos == null || configInstanceInfos.Count == 0)
            {
                return;    
            }
            
            InstanceInfos = new HashSet<InstanceInfo>();
            foreach (ConfigInstanceInfo configInstanceInfo in configInstanceInfos)
            {
                var instanceInfo = InstanceInfo.Create(configInstanceInfo);
                if (InstanceInfos.Contains(instanceInfo))
                {
                    Debug.LogError($"Found duplicate InstanceInfo {instanceInfo.InstanceID}");
                    continue;
                }
                InstanceInfos.Add(instanceInfo);
            }
        }
    }
}