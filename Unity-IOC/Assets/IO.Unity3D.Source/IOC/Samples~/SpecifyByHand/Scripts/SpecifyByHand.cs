using IO.Unity3D.Source.IOC;
using UnityEngine;

public class SpecifyByHand : MonoBehaviour
{
    public const string WORD_SPECIAL_INSTANCE = nameof(WORD_SPECIAL_INSTANCE);
    void Start()
    {
        IOCContainerConfiguration config = new IOCContainerConfiguration()
            .AddConfigInstanceInfo<You>()
            .AddConfigInstanceInfo<Word>()
            .AddConfigInstanceInfo<Word>(WORD_SPECIAL_INSTANCE, new ValueSetter("_Msg", "Message"));
        new IOCContainerBuilder().SetConfiguration(config).Build();
    }

    class You : IInstanceLifeCycle
    {
        [Autowired]
        private Word _Word;
        
        [Autowired]
        [Qualifier(WORD_SPECIAL_INSTANCE)]
        private Word _Word2;

        public void Say()
        {
            Debug.LogError($"Say {_Word.GetMsg()}");
            Debug.LogError($"Say {_Word2.GetMsg()}");
        }

        public void BeforePropertiesOrFieldsSet()
        {
        }

        public void AfterPropertiesOrFieldsSet()
        {
        }

        public void AfterAllInstanceInit()
        {
            Say();
        }
    }

    class Word
    {
        private string _Msg = "Hello";

        public string GetMsg()
        {
            return _Msg;
        }
    }
}
