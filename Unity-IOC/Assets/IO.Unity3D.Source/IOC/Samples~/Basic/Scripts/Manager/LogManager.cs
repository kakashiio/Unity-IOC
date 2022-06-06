using System;
using UnityEngine;

namespace IO.Unity3D.Source.IOC.Samples
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-04 20:10
    //******************************************
    [IOCComponent]
    public class LogManager : ILogManager
    {
        private string _Prefix;
        private LogLevel _LogLevel = LogLevel.Debug;

        [Autowired]
        private CustomManager _CustomManager;
        
        public void Log(LogLevel level, string templte, params object[] args)
        {
            if (level < _LogLevel)
            {
                return;
            }
            string msg = args == null || args.Length == 0 ? templte : string.Format(templte, args);
            msg = $"{_CustomManager.LogPrefix()} {_Prefix} [{level}] Frame={Time.frameCount} Time={Time.time} -- {msg}";
            switch (level)
            {
                case LogLevel.Debug:
                case LogLevel.Info:
                    Debug.Log(msg);
                    break;
                case LogLevel.Warning:
                    Debug.LogWarning(msg);
                    break;
                case LogLevel.Exception:
                    Debug.LogException(new Exception(msg));
                    break;
                case LogLevel.Error:
                    Debug.LogError(msg);
                    break;
            }
        }
    }
}