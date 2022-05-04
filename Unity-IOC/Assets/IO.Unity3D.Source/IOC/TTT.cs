using System.Reflection;
using IO.Unity3D.Source.IOC;
using IO.Unity3D.Source.Reflection;
using UnityEngine;

public class TTT : MonoBehaviour
{
    void Start()
    {
        IOCContainer iocContainer = new IOCContainer(new TypeContainer(Assembly.GetExecutingAssembly()));
        var b = iocContainer.FindObjectOfType<B>();
        b.Test();
    }
}

[IOCComponent]
class A
{
    private string _Name = "Hello world!";

    public override string ToString()
    {
        return _Name;
    }
}

[IOCComponent]
class B
{
    [Autowired]
    private A _A;

    public void Test()
    {
        Debug.LogError(_A);
    }
}
