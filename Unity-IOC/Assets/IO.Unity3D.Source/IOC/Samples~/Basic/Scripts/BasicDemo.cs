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
            var typeContainer = new TypeContainerCollection(new []
            {
                new TypeContainer(Assembly.GetExecutingAssembly()),
                new TypeContainer(typeof(IOCComponent).Assembly)
            });
            var iocContainer = new IOCContainer(typeContainer);
            
            GameObjectManager gameObjectManager = iocContainer.FindObjectOfType<GameObjectManager>();
            gameObjectManager.Instantiate("", null);
        }
    }
}