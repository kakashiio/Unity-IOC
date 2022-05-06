using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace IO.Unity3D.Source.IOC.Samples
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-04 20:00
    //******************************************
    [IOCComponent]
    public class AssetManager
    {
        public const string LOG_INSTANCE_NAME = "Logger4Asset";
        
        private float _WaitSeconds;
        
        [Autowired] 
        private CoroutineManager _CoroutineManager;
        [Qualifier(LOG_INSTANCE_NAME)]
        [Autowired] 
        private ILogManager _LogManager;

        public void LoadAsync<T>(string assetPath, Action<T> onLoaded) where T : Object
        {
            _CoroutineManager.StartCoroutine(_LoadAsync(assetPath, onLoaded));
        }

        private IEnumerator _LoadAsync<T>(string assetPath, Action<T> onLoaded) where T : Object
        {
            _LogManager.Log(LogLevel.Debug, "Loading {0}", assetPath);
            // Your load code here
            // Now just wait for some seconds for demo
            yield return new WaitForSeconds(_WaitSeconds);
            T loadedAsset = default(T);
            _LogManager.Log(LogLevel.Debug, "Loaded {0} asset={1}", assetPath, loadedAsset);
            onLoaded?.Invoke(loadedAsset);
        }
    }
}