using System;
using UnityEngine;

namespace IO.Unity3D.Source.IOC.Samples
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-04 20:22
    //******************************************
    [IOCComponent]
    public class GameObjectManager : IInstanceLifeCycle
    {
        [Autowired]
        private AssetManager _AssetManager;
        [Autowired]
        private ILogManager _LogManager;

        public void Instantiate(string assetPath, Action<GameObject> onLoaded)
        {
            _AssetManager.LoadAsync(assetPath, (GameObject prefab) =>
            {
                if (prefab == null)
                {
                    _LogManager.Log(LogLevel.Debug, "Failed to instantiate {0}", assetPath);
                    return;
                }
                var go = GameObject.Instantiate(prefab);
                onLoaded?.Invoke(go);
            });
        }

        public void BeforePropertiesOrFieldsSet()
        {
        }

        public void AfterPropertiesOrFieldsSet()
        {
        }

        public void AfterAllInstanceInit()
        {
            Instantiate("", null);
        }
    }
}