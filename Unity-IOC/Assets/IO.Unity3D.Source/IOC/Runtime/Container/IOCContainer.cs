using System;
using System.Collections.Generic;
using System.Reflection;
using IO.Unity3D.Source.Reflection;

namespace IO.Unity3D.Source.IOC
{
    //******************************************
    //  Implementation of IIOCContainer
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-03 21:11
    //******************************************
    public class IOCContainer : IIOCContainer
    {
        private ITypeContainer _TypeContainer;
        private List<Instance> _Instances = new List<Instance>();
        private HashSet<object> _InjectedObj = new HashSet<object>();
        
        private Dictionary<Type, Dictionary<string, object>> _FindCacheWithQualifier = new Dictionary<Type, Dictionary<string, object>>();

        internal IOCContainer(ITypeContainer typeContainer, IOCContainerConfiguration configuration = null)
        {
            _TypeContainer = typeContainer;
            _ProcessConfiguration(configuration);

            var inheritedFromIOCComponent = Reflections.GetTypes(_TypeContainer, typeof(IOCComponent));
            var typesWithIOCComponent = Reflections.GetTypesWithAttributes(_TypeContainer, inheritedFromIOCComponent);
            foreach (var type in typesWithIOCComponent)
            {
                if (configuration!=null && configuration.ContainsInstanceConfig(type))
                {
                    continue;
                }

                var instance = _Instance(type);
                var qualifier = type.GetCustomAttribute(typeof(Qualifier)) as Qualifier;
                _Instances.Add(new Instance(qualifier == null ? Qualifier.DEFAULT : qualifier.Name, instance));
            }
            
            // Inject all type's field or property
            foreach (var instance in _Instances)
            {
                Inject(instance.Object);
            }
        }

        private void _ProcessConfiguration(IOCContainerConfiguration configuration)
        {
            if (configuration == null)
            {
                return;
            }

            if (configuration.BeanInfos != null)
            {
                foreach (var beanInfo in configuration.BeanInfos)
                {
                    var type = beanInfo.Key.Type;
                    var instance = _Instance(type);
                    foreach (var fieldOrPropertyInfo in beanInfo.Value.FieldOrPropertyInfos)
                    {
                        var propertyOrField = Reflections.GetPropertyOrField(type, fieldOrPropertyInfo.Name);
                        if (propertyOrField == null)
                        {
                            throw new IOCConfigurationException("Can not found property or field `" + fieldOrPropertyInfo.Name + "` in " + type);
                        }
                        propertyOrField.SetValue(instance, fieldOrPropertyInfo.Value);
                    }
                    _Instances.Add(new Instance(beanInfo.Key.QualifierName, instance));
                }
            }
        }

        public object InstanceAndInject(Type type)
        {
            var instance = _Instance(type);
            Inject(instance);
            return instance;
        }

        public T InstanceAndInject<T>()
        {
            return (T) InstanceAndInject(typeof(T));
        }

        public void Inject(object obj, bool recursive = false)
        {
            if (obj == null)
            {
                return;
            }

            if (obj.GetType().IsPrimitive)
            {
                return;
            }

            if (recursive)
            {
                if (_InjectedObj.Contains(obj))
                {
                    return;
                }
                _InjectedObj.Add(obj);    
            }

            var propertiesOrFields = Reflections.GetPropertiesAndFields<Autowired>(obj);

            foreach (var propertyOrField in propertiesOrFields)
            {
                var qualifier = propertyOrField.GetCustomAttribute<Qualifier>();
                var fieldValue = FindObjectOfType(propertyOrField.GetFieldOrPropertyType(), qualifier != null ? qualifier.Name : null);
                propertyOrField.SetValue(obj, fieldValue);
                
                if (recursive)
                {
                    Inject(fieldValue, true);
                }
            }
        }

        public object FindObjectOfType(Type type, string alias = null)
        {
            alias = alias ?? Qualifier.DEFAULT;
            
            if (_FindCacheWithQualifier.ContainsKey(type))
            {
                var qualifier2Instance = _FindCacheWithQualifier[type];
                if (qualifier2Instance.ContainsKey(alias))
                {
                    return qualifier2Instance[alias];
                }
            }

            foreach (Instance instance in _Instances)
            {
                object obj = instance.Object;
                var objType = obj.GetType();
                if(type.IsAssignableFrom(objType))
                {
                    if (instance.QualifierName.Equals(alias))
                    {
                        Dictionary<string, object> qualifier2Instance = null;
                        if (_FindCacheWithQualifier.ContainsKey(type))
                        {
                            qualifier2Instance = _FindCacheWithQualifier[type];
                        }
                        else
                        {
                            qualifier2Instance = new Dictionary<string, object>();
                            _FindCacheWithQualifier.Add(type, qualifier2Instance);
                        }
                        qualifier2Instance.Add(alias, obj);
                        return obj;    
                    }
                }
            }
            
            return null;
        }

        public T FindObjectOfType<T>(string alias = null) where T : class
        {
            return FindObjectOfType(typeof(T), alias) as T;
        }

        public List<object> FindObjectsOfType(Type type)
        {
            return _FindObjectsOfType(typeof(object), o => o);
        }

        public List<T> FindObjectsOfType<T>() where T : class
        {
            return _FindObjectsOfType(typeof(T), o => o as T);
        }

        public List<Instance> GetAllInstances()
        {
            return _Instances;
        }

        public List<InstanceMethods> FindMethods<A>() where A : Attribute
        {
            return FindMethods(typeof(A));
        }

        public List<InstanceMethods> FindMethods(Type attribute)
        {
            List<InstanceMethods> beanMethodses = new List<InstanceMethods>();

            foreach (var instance in _Instances)
            {
                var type = instance.GetType();
                var methods = Reflections.GetMethods(type, attribute);
                if (methods == null || methods.Count == 0)
                {
                    continue;
                }
                
                beanMethodses.Add(new InstanceMethods(instance, methods));
            }

            return beanMethodses;
        }

        public InstanceMethods FindMethods(object obj, Type attribute)
        {
            var type = obj.GetType();
            var methods = Reflections.GetMethods(type, attribute);
            if (methods == null || methods.Count == 0)
            {
                return null;
            }
            return new InstanceMethods(obj, methods);
        }

        private object _Instance(Type type)
        {
            return Activator.CreateInstance(type);
        }
        
        private List<T> _FindObjectsOfType<T>(Type type, Func<object, T> mapper) where T : class
        {
            List<T> list = new List<T>();
            foreach (object instance in _Instances)
            {
                var objType = instance.GetType();
                if(type.IsAssignableFrom(objType))
                {
                    list.Add(mapper(instance));
                }
            }
            return list;
        }
    }
}