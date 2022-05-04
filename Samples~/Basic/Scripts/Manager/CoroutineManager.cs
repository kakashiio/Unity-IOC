using System.Collections;
using UnityEngine;

namespace IO.Unity3D.Source.IOC.Samples
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-04 20:03
    //******************************************
    [IOCComponent]
    public class CoroutineManager
    {
        private CoroutineRunner _CoroutineRunner;
        
        public CoroutineManager()
        {
            var go = new GameObject("CoroutineRunner");
            _CoroutineRunner = go.AddComponent<CoroutineRunner>();
            GameObject.DontDestroyOnLoad(go);
        }

        public void StartCoroutine(IEnumerator enumerator)
        {
            _CoroutineRunner.StartCoroutine(enumerator);
        }
    }

    public class CoroutineRunner : MonoBehaviour
    {
    }
}