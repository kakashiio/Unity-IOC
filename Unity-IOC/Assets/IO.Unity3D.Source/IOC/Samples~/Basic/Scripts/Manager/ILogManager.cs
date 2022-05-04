namespace IO.Unity3D.Source.IOC.Samples
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-04 22:05
    //******************************************
    public interface ILogManager
    {
        public void Log(LogLevel level, string templte, params object[] args);
    }

    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Exception,
        Error
    }
}