using System.Collections.Generic;
using System.Reflection;
using IO.Unity3D.Source.Reflection;
using UnityEngine;

namespace IO.Unity3D.Source.IOC
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-08 16:52
    //******************************************
    public class MixSetter : IPropertyOrFieldSetter
    {
        private readonly static List<AbsSinglePropertyOrFieldSetter> EMPTY = new List<AbsSinglePropertyOrFieldSetter>();
        
        private List<AbsSinglePropertyOrFieldSetter> _PropertyAndFieldSetters;
        private HashSet<string> _SetPropertyAndFieldNames = new HashSet<string>();
        
        public MixSetter(List<AbsSinglePropertyOrFieldSetter> propertyAndFieldSetters)
        {
            _PropertyAndFieldSetters = propertyAndFieldSetters ?? EMPTY;
            foreach (var propertyAndFieldSetter in _PropertyAndFieldSetters)
            {
                _SetPropertyAndFieldNames.Add(propertyAndFieldSetter.Name);
            }
        }

        public void Set(IIOCContainer iocContainer, Instance instance)
        {
            var type = instance.Object.GetType();

            foreach (var propertyAndFieldSetter in _PropertyAndFieldSetters)
            {
                propertyAndFieldSetter.Set(iocContainer, instance);
            }
            
            var propertiesAndFields = Reflections.GetPropertiesAndFields<Autowired>(type);
            foreach (var propertiesAndField in propertiesAndFields)
            {
                if (_SetPropertyAndFieldNames.Contains(propertiesAndField.Name))
                {
                    continue;
                }

                var qualifier = propertiesAndField.GetCustomAttribute<Qualifier>();
                ReferenceSetter.Set(iocContainer, instance, propertiesAndField, qualifier == null ? Qualifier.DEFAULT : qualifier.Name);
            }
        }
    }
}