using System;
using System.Collections.Generic;
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
        private readonly static IOCContainerConfiguration EMPTY = new IOCContainerConfiguration();
        private List<Instance> _Instances = new List<Instance>();
        private HashSet<Instance> _InjectedObj = new HashSet<Instance>();
        private HashSet<IInstanceLifeCycle> _InstanceLifeCycles = new HashSet<IInstanceLifeCycle>();
        private HashSet<IContainerLifeCycle> _ContainerLifeCycles = new HashSet<IContainerLifeCycle>();
        
        private Dictionary<Type, Dictionary<string, Instance>> _FindCacheWithQualifier = new Dictionary<Type, Dictionary<string, Instance>>();

        internal IOCContainer(ITypeContainer typeContainer, IOCContainerConfiguration configuration = null)
        {
            configuration = configuration ?? EMPTY;
            
            var instanceInfos = new HashSet<InstanceInfo>();
            
            // Collect instance info from config
            foreach (var instanceInfo in configuration.InstanceInfos)
            {
                instanceInfos.Add(instanceInfo);
            }

            // Collect instance info from auto inject
            if (typeContainer != null)
            {
                var inheritedFromIOCComponent = Reflections.GetTypes(typeContainer, typeof(IOCComponent));
                var typesWithIOCComponent = Reflections.GetTypesWithAttributes(typeContainer, inheritedFromIOCComponent);
                foreach (var type in typesWithIOCComponent)
                {
                    instanceInfos.Add(InstanceInfo.Create(type));
                }                
            }

            // Instance
            foreach (var instanceInfo in instanceInfos)
            {
                Manage(_Instance(instanceInfo));
            }
            
            foreach (var instance in configuration.Instances)
            {
                Manage(instance);
            }
            
            // Trigger container's life cycle : OnContainerAware
            foreach (var containerLifeCycle in _ContainerLifeCycles)
            {
                containerLifeCycle.OnContainerAware(this);
            }
            
            // Inject all type's field or property
            foreach (var instance in _Instances)
            {
                var instanceLifeCycle = instance.Object as IInstanceLifeCycle;
                if (instanceLifeCycle != null)
                {
                    instanceLifeCycle.BeforePropertiesOrFieldsSet();
                }
                Inject(instance);
                
                if (instanceLifeCycle != null)
                {
                    instanceLifeCycle.AfterPropertiesOrFieldsSet();
                }
            }
            
            foreach (var instanceLifeCycle in _InstanceLifeCycles)
            {
                instanceLifeCycle.AfterAllInstanceInit();
            }
        }

        public void Manage(object obj)
        {
            var configInstanceInfo = new ConfigInstanceInfo(obj.GetType());
            var instance = new Instance(InstanceInfo.Create(configInstanceInfo), obj);
            Manage(instance);
        }

        public void Manage(Instance instance)
        {
            _Instances.Add(instance);
        }

        public object InstanceAndInject(InstanceInfo instanceInfo)
        {
            var instance = _Instance(instanceInfo);
            Inject(instance);
            return instance;
        }

        public T InstanceAndInject<T>(IReadOnlyList<ValueSetter> propertyAndFieldInfos, string qualifierName = Qualifier.DEFAULT)
        {
            return (T) InstanceAndInject(new InstanceInfo(new InstanceID(typeof(T), qualifierName), propertyAndFieldInfos));
        }

        public void Inject(Instance instance, bool recursive = false)
        {
            if (instance == null)
            {
                return;
            }

            var instanceObj = instance.Object;
            var type = instanceObj.GetType();
            if (type.IsPrimitive)
            {
                return;
            }

            if (recursive)
            {
                if (_InjectedObj.Contains(instance))
                {
                    return;
                }
                _InjectedObj.Add(instance);    
            }

            var propertyAndFieldInfos = instance.InstanceInfo.PropertyOrFieldInfos;
            foreach (var propertyAndFieldInfo in propertyAndFieldInfos)
            {
                propertyAndFieldInfo.Set(this, instance);
            }

            var instanceLifeCycle = instance as IInstanceLifeCycle;
            if (instanceLifeCycle != null)
            {
                instanceLifeCycle.AfterPropertiesOrFieldsSet();
            }
        }

        public object FindObjectOfType(Type type, string alias = Qualifier.DEFAULT)
        {
            Instance instance = _FindObjectOfType(type, alias);
            return instance != null ? instance.Object : null;
        }

        private Instance _FindObjectOfType(Type type, string alias)
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
                if (type.IsAssignableFrom(objType))
                {
                    if (instance.InstanceInfo.InstanceID.QualifierName.Equals(alias))
                    {
                        Dictionary<string, Instance> qualifier2Instance;
                        if (_FindCacheWithQualifier.ContainsKey(type))
                        {
                            qualifier2Instance = _FindCacheWithQualifier[type];
                        }
                        else
                        {
                            qualifier2Instance = new Dictionary<string, Instance>();
                            _FindCacheWithQualifier.Add(type, qualifier2Instance);
                        }
                        qualifier2Instance.Add(alias, instance);
                        return instance;
                    }
                }
            }

            return null;
        }

        public T FindObjectOfType<T>(string alias = Qualifier.DEFAULT) where T : class
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
                var type = instance.InstanceInfo.InstanceID.Type;
                var methods = Reflections.GetMethods(type, attribute);
                if (methods == null || methods.Count == 0)
                {
                    continue;
                }
                
                beanMethodses.Add(new InstanceMethods(instance.Object, methods));
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

        public void Destroy()
        {
            foreach (var containerLifeCycle in _ContainerLifeCycles)
            {
                containerLifeCycle.OnContainerDestroy(this);
            }
        }

        private Instance _Instance(InstanceInfo instanceInfo)
        {
            var obj = Activator.CreateInstance(instanceInfo.InstanceID.Type);
            
            var instanceLifeCycle = obj as IInstanceLifeCycle;
            if (instanceLifeCycle != null)
            {
                _InstanceLifeCycles.Add(instanceLifeCycle);
            }

            var containerLifeCycle = obj as IContainerLifeCycle;
            if (containerLifeCycle != null)
            {
                _ContainerLifeCycles.Add(containerLifeCycle);
            }
            return new Instance(instanceInfo, obj);
        }
        
        private List<T> _FindObjectsOfType<T>(Type type, Func<object, T> mapper) where T : class
        {
            List<T> list = new List<T>();
            foreach (Instance instance in _Instances)
            {
                var objType = instance.InstanceInfo.InstanceID.Type;
                if(type.IsAssignableFrom(objType))
                {
                    list.Add(mapper(instance.Object));
                }
            }
            return list;
        }
    }
}