using System;
using System.Collections.Generic;

namespace IO.Unity3D.Source.IOC
{
    //******************************************
    // Defined the ability of an IOC Container
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-03 21:13
    //******************************************
    public interface IIOCContainer
    {
        /// <summary>
        /// Create instance and inject all the fields and properties by the specified `type`
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object InstanceAndInject(Type type);

        /// <summary>
        /// Create instance and inject all the fields and properties by the specified generic type `T`
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T InstanceAndInject<T>();

        /// <summary>
        /// Inject all the fields and properties by the specified `obj`.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="recursive">
        /// If recursive == true, its fields will be recursive injected
        /// </param>
        void Inject(object obj, bool recursive = false);

        /// <summary>
        /// Find the object by the specified `type` and `alias`
        /// </summary>
        /// <param name="type"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        object FindObjectOfType(Type type, string alias = null);
        
        /// <summary>
        /// Find the object by the specified generic type `T` and `alias`
        /// </summary>
        /// <param name="alias"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T FindObjectOfType<T>(string alias = null) where T : class;
        
        /// <summary>
        /// Find all objects by the specified `type`
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        List<object> FindObjectsOfType(Type type);
        
        /// <summary>
        /// Find all objects by the specified generic type `T`
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        List<T> FindObjectsOfType<T>() where T : class;
        
        /// <summary>
        /// Find all objects managered in the container
        /// </summary>
        /// <returns></returns>
        List<object> GetAllObjects();
        
        /// <summary>
        /// Find all methods which with the attribute `A` in the container
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <returns></returns>
        List<BeanMethods> FindMethods<A>() where A : Attribute;
        
        /// <summary>
        /// Find all methods which with the attribute `attribute` in the container
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        List<BeanMethods> FindMethods(Type attribute);

        /// <summary>
        /// Find methods which with the attribute `attribute` in the `obj`
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="attribute"></param>
        /// <param name="beanMethodList"></param>
        /// <returns></returns>
        BeanMethods FindMethods(Object obj, Type attribute);
    }
}