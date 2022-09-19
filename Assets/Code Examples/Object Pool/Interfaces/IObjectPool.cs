//----------------------------------------
// 	            Object Pool
// Copyright © 2021 'OOO EBONI INTERAKTIV'
//---------------------------------------

using UnityEngine;

public interface IObjectPool<ObjectType>
{
    /// <summary>
    /// Obtain object from pool.
    /// </summary>
    /// <param name="objName">Name of the obtained object.</param>
    ObjectType Obtain(string objName);

    /// <summary>
    /// Obtain object from pool as child of specific parent.
    /// </summary>
    /// <param name="objName">Name of the obtained object.</param>
    /// /// <param name="parent">Reference to specific object parent.</param>
    ObjectType Obtain(string objName, Transform parent);

    /// <summary>
    /// Return object to pool for reuse.
    /// </summary>
    void Release(ObjectType obj, float? releaseDelay = null);

    /// <summary>
    /// Delete all objects in the pool and clear it.
    /// </summary>
    void Clear();
}