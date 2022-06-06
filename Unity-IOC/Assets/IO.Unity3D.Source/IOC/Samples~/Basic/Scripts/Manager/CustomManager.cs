using UnityEngine;

namespace IO.Unity3D.Source.IOC.Samples
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-06-07 00:38
    //******************************************
    public class CustomManager
    {
        [Autowired]
        private LogManager _LogManager;
        
        public string LogPrefix()
        {
            return $"{_LogManager}-";
        }
    }
}