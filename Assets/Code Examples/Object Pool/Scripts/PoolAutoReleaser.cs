//----------------------------------------
// 	            Object Pool
// Copyright © 2021 'OOO EBONI INTERAKTIV'
//---------------------------------------

using UnityEngine;
using System;

public class PoolAutoReleaser : MonoBehaviour
{
    public enum Trigger
    {
        OnTriggerEnter,
        OnTriggerExit,
        OnCollisionEnter,
        OnCollisionExit,
        OnDisable,
        Custom
    }

    [SerializeField]
    private Trigger _trigger = Trigger.Custom;
    [SerializeField]
    private string[] _collisionTags = new string[] { "Untagged" };

    private ObjectPool pool;
    private bool released;

    /// <summary>
    /// Invoke this method to construct auto-return object to pool.
    /// </summary>
    /// <param name="objectPool">Reference to object pool.</param>
    public void Construct(ObjectPool objectPool)
    {
        pool = objectPool != null ? objectPool : throw new ArgumentNullException(nameof(objectPool));
    }

    private void OnEnable()
    {
        released = false;
    }

    private void OnDisable()
    {
        if (_trigger != Trigger.OnDisable)
            return;

        Release();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_trigger != Trigger.OnTriggerEnter)
            return;

        if (_collisionTags != null && IsValidTag(other.tag))
            return;

        Release();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_trigger != Trigger.OnTriggerEnter)
            return;

        if (_collisionTags != null && IsValidTag(other.tag))
            return;

        Release();
    }

    private void OnTriggerExit(Collider other)
    {
        if (_trigger != Trigger.OnTriggerExit)
            return;

        if (_collisionTags != null && IsValidTag(other.tag))
            return;

        Release();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_trigger != Trigger.OnTriggerExit)
            return;

        if (_collisionTags != null && IsValidTag(other.tag))
            return;

        Release();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_trigger != Trigger.OnCollisionEnter)
            return;

        if (_collisionTags != null && IsValidTag(other.gameObject.tag))
            return;

        Release();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_trigger != Trigger.OnCollisionEnter)
            return;

        if (_collisionTags != null && IsValidTag(other.gameObject.tag))
            return;

        Release();
    }

    private void OnCollisionExit(Collision other)
    {
        if (_trigger != Trigger.OnCollisionExit)
            return;

        if (_collisionTags != null && IsValidTag(other.gameObject.tag))
            return;

        Release();
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (_trigger != Trigger.OnCollisionExit)
            return;

        if (_collisionTags != null &&  IsValidTag(other.gameObject.tag))
            return;

        Release();
    }

    private bool IsValidTag(string tag)
    {
        bool result = false;

        foreach (string collisionTag in _collisionTags)
        {
            if (tag != collisionTag)
                continue;

            result = true;

            break;
        }

        return result;
    }

    /// <summary>
    /// Invoke this method to release object to pool.
    /// </summary>
    public void Release()
    {
        if (pool == null)
            throw new MissingReferenceException(nameof(pool));

        if (released)
            return;

        released = true;

        pool.Release(gameObject);
    }
}
