using System.Collections.Generic;
using System.Reflection;
using IO.Unity3D.Source.Reflection;
using UnityEngine;

namespace IO.Unity3D.Source.IOC.Samples
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-04 19:59
    //******************************************
    public class BasicDemo : MonoBehaviour
    {
        private void Awake()
        {
            _TestContainerWithConfig(_CreateConfig(3, "ContainerA"));
            _TestContainerWithConfig(_CreateConfig(1, "ContainerB"));
        }

        private void _TestContainerWithConfig(IOCContainerConfiguration config)
        {
            var typeContainer = new TypeContainerCollection(new []
            {
                new TypeContainer(Assembly.GetExecutingAssembly()),
                new TypeContainer(typeof(IOCComponent).Assembly)
            });

            var iocContainer = new IOCContainerBuilder(typeContainer).SetConfiguration(config).Build();
            GameObjectManager gameObjectManager = iocContainer.FindObjectOfType<GameObjectManager>();
            gameObjectManager.Instantiate("", null);
        }

        private IOCContainerConfiguration _CreateConfig(float assetWaitSeconds, string logPrefix)
        {
            return new IOCContainerConfiguration(new List<InstanceInfo>
            {
                new InstanceInfo(typeof(LogManager), Qualifier.DEFAULT, null),
                new InstanceInfo(typeof(LogManager), AssetManager.LOG_INSTANCE_NAME, new List<FieldOrPropertyInfo>
                {
                    new FieldOrPropertyInfo("_Prefix", logPrefix)
                }),
                new InstanceInfo(typeof(AssetManager), Qualifier.DEFAULT, new List<FieldOrPropertyInfo>
                {
                    new FieldOrPropertyInfo("_WaitSeconds", assetWaitSeconds)
                })
            });
        }
    }
}