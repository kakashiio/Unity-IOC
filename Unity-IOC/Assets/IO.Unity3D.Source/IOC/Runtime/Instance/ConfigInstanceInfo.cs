using System;
using System.Collections.Generic;
using IO.Unity3D.Source.Reflection;

namespace IO.Unity3D.Source.IOC
{
    //******************************************
    // InstanceInfo
    // Declare how to instance a object, includes:
    //   1. set value for field or property
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-06 21:48
    //******************************************
    public class ConfigInstanceInfo
    {
        private readonly static List<IPropertyOrFieldSetter> EMPTY = new List<IPropertyOrFieldSetter>(0);
        
        public readonly Type Type;
        public readonly string QualifierName;

        private List<IPropertyOrFieldSetter> _PropertyOrFieldInfos;
        public IReadOnlyList<IPropertyOrFieldSetter> PropertyOrFieldInfos => _PropertyOrFieldInfos;
        

        public ConfigInstanceInfo(Type type, string qualifierName, List<IPropertyOrFieldSetter> propertyOrFieldInfos, bool autoInjectMissingPropertyOrFieldInfo = true)
        {
            Type = type;
            QualifierName = qualifierName;
            _PropertyOrFieldInfos = propertyOrFieldInfos ?? EMPTY;
            
            if (autoInjectMissingPropertyOrFieldInfo)
            {
                _CollectAutoInjectPropertyOrFieldInfos();
            }
        }

        private void _CollectAutoInjectPropertyOrFieldInfos()
        {
            if (_PropertyOrFieldInfos == EMPTY)
            {
                _PropertyOrFieldInfos = new List<IPropertyOrFieldSetter>();
            }

            var propertiesAndFields = Reflections.GetPropertiesAndFields<Autowired>(Type);
            foreach (var propertiesAndField in propertiesAndFields)
            {
                var foundSetting = _PropertyOrFieldInfos.Find((p) =>
                {
                    var absSinglePropertyOrFieldSetter = p as AbsSinglePropertyOrFieldSetter;
                    if (absSinglePropertyOrFieldSetter == null)
                    {
                        throw new Exception($"AutoInjectMissingPropertyOrFieldInfo only support the `propertyOrFieldInfos` which contains all AbsSinglePropertyOrFieldSetter setter. Type={Type}");
                    }

                    return absSinglePropertyOrFieldSetter.Name.Equals(propertiesAndField.Name);
                });
                if (foundSetting != null)
                {
                    continue;
                }
                var fieldQualifier = propertiesAndField.GetCustomAttribute<Qualifier>();
                _PropertyOrFieldInfos.Add(new ReferenceSetter(propertiesAndField.Name, null, fieldQualifier == null ? Qualifier.DEFAULT : fieldQualifier.Name));
            }
        }

        public ConfigInstanceInfo(Type type, List<IPropertyOrFieldSetter> fieldOrPropertyInfos) : this(type, Qualifier.DEFAULT, fieldOrPropertyInfos)
        {
        }
        
        public ConfigInstanceInfo(Type type, string qualifierName) : this(type, qualifierName, null)
        {
        }
        
        public ConfigInstanceInfo(Type type) : this(type, Qualifier.DEFAULT, null)
        {
        }
    }
}