//----------------------------------------
// 	            Object Pool
// Copyright © 2021 'OOO EBONI INTERAKTIV'
//---------------------------------------

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AbonyInt.EventSystem;

public class ObjectPool : MonoBehaviour, IObjectPool<GameObject>
{
    private readonly List<GameObject> pool = new List<GameObject>();
    private readonly Dictionary<string, GameObject> map = new Dictionary<string, GameObject>();
    private readonly int preCreationLimit = 10;

    [SerializeField]
    private bool _autoEnable = false;
    [SerializeField]
    private PoolLoadTypes _loadType = PoolLoadTypes.Prefabs;
    [SerializeField]
    private GameObject[] _prefabs = null;
    [SerializeField]
    private string[] _folderPaths = null;
    // Optional / Advanced
    [SerializeField]
    private GlobalEvent _onObtain = null;
    [SerializeField]
    private GlobalEvent _onRelease = null;
    [SerializeField]
    private ParentTypes _parentType = ParentTypes.Pool;
    [SerializeField]
    private Transform _parent = null;
    [SerializeField]
    private bool _usePreCreation = false;
    [SerializeField]
    private int _preCreationCount = 0;

    /// <summary>
    /// Count of ready to use objects in the pool.
    /// </summary>
    public int Count => pool.Count;
    /// <summary>
    /// Helpful event to handle obtain new object from pool.
    /// </summary>
    public event EventHandler<object> OnObtain;
    /// <summary>
    /// Helpful event to handle release object to pool.
    /// </summary>
    public event EventHandler<object> OnRelease;
    /// <summary>
    /// Helpful event to handle objects created by pre-createion process.
    /// </summary>
    public event EventHandler<GameObject[]> OnPreCreate;

    private List<GameObject> objectsToRelease = new List<GameObject>();
    private Coroutine preCreation = null;

    private void Awake()
    {
        if (_parentType == ParentTypes.Pool)
            _parent = transform;

        if (_loadType == PoolLoadTypes.Folders)
            LoadFromFolders();
        else
            LoadPrefabs();

        if (!_usePreCreation)
            return;

        preCreation = StartCoroutine(PreCreation());
    }

    private void OnDisable()
    {
        CancelInvoke();

        objectsToRelease.ForEach(x => pool.Add(x));
        objectsToRelease.Clear();

        if (preCreation == null)
            return;

        StopCoroutine(preCreation);
        preCreation = null;
    }

    private void LoadFromFolders()
    {
        for (int i = 0; i < _folderPaths.Length; i++)
        {
            LoadFromFolder(_folderPaths[i]);
        }
    }

    private void LoadFromFolder(string path)
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>(path);

        foreach (GameObject prefab in prefabs)
        {
            LoadPrefab(prefab);
        }
    }

    private void LoadPrefabs()
    {
        for (int i = 0; i < _prefabs.Length; i++)
        {
            LoadPrefab(_prefabs[i]);
        }
    }

    private void LoadPrefab(GameObject prefab)
    {
        if (map.ContainsKey(prefab.name))
            throw new InvalidOperationException($"There is more then one '{prefab.name}' prefab in the folder.");

        map.Add(prefab.name, prefab);
    }

    /// <summary>
    /// Clear all objects in the pool.
    /// </summary>
    public void Clear()
    {
        pool.ForEach(x => Destroy(x));
        pool.Clear();
    }

    private IEnumerator PreCreation()
    {
        int counter = 0, prefabsCount = 0, i = 0;
        GameObject obj = null;
        GameObject[] preCreatedObjects = null;

        foreach (string key in map.Keys)
        {
            counter = 0;

            while (counter < _preCreationCount)
            {
                prefabsCount = preCreationLimit;

                if (counter + prefabsCount > _preCreationCount)
                    prefabsCount = (_preCreationCount - counter);

                preCreatedObjects = new GameObject[prefabsCount];

                for (i = 0; i < prefabsCount; i++)
                {
                    obj = Create(map[key], _parent);
                    
                    pool.Add(obj);

                    preCreatedObjects[i] = obj;
                }

                counter += prefabsCount;

                OnPreCreate?.Invoke(this, preCreatedObjects);

                yield return new WaitForEndOfFrame();
            }
        }

        counter = 0;
        prefabsCount = 0;
        i = 0;

        preCreation = null;

        yield return new WaitForEndOfFrame();

        GC.Collect();
    }

    private GameObject Create(GameObject prefab, Transform parent)
    {
        GameObject obj = Instantiate(prefab);
        obj.transform.parent = parent;

        PoolAutoReleaser autoReleaser = obj.GetComponent<PoolAutoReleaser>();

        if (autoReleaser != null)
            autoReleaser.Construct(this);

        if (_autoEnable)
            obj.SetActive(true);

        return obj;
    }

    /// <summary>
    /// Obtain object from pool as child of specific parent.
    /// </summary>
    /// <param name="objName">Name of the obtained object.</param>
    /// <param name="parent">Reference to specific object parent.</param>
    public GameObject Obtain(string objName, Transform parent)
    {
        string key = GetKey(objName);

        if (!map.ContainsKey(key))
            throw new InvalidOperationException($"Pool does not support objects with name '{key}'.");

        GameObject obj = pool.Find(x => x != null && x.name.Contains(key));

        if (obj == null)
        {
            obj = Create(map[key], parent);
        }
        else
        {
            if (obj.transform.parent != parent)
                obj.transform.parent = parent;

            if (_autoEnable && !obj.activeInHierarchy)
                obj.SetActive(true);
        }

        pool.Remove(obj);

        OnObtain?.Invoke(this, obj);
        _onObtain?.Invoke(this, obj);

        return obj;
    }

    /// <summary>
    /// Obtain object from pool.
    /// </summary>
    /// <param name="objName">Name of the obtained object.</param>
    public GameObject Obtain(string objName)
    {
        return Obtain(objName, _parent);
    }

    /// <summary>
    /// Obtain object from pool by type.
    /// </summary>
    public T Obtain<T>() where T : MonoBehaviour
    {
        GameObject prefab = map.Values.ToList().Find(x => x.GetComponent<T>());

        if (prefab == null)
            throw new InvalidOperationException($"Pool does not support type '{typeof(T)}'.");

        return Obtain(prefab.name, _parent).GetComponent<T>();
    }

    /// <summary>
    /// Obtain object from pool by type as child of specific parent.
    /// </summary>
    /// <param name="parent">Reference to specific object's parent.</param>
    public T Obtain<T>(Transform parent) where T : MonoBehaviour
    {
        GameObject prefab = map.Values.ToList().Find(x => x is T);

        if (prefab == null)
            throw new InvalidOperationException($"Pool does not support type {typeof(T)}.");

        return Obtain(prefab.name, parent).GetComponent<T>();
    }

    /// <summary>
    /// Obtain object from pool by type and name.
    /// </summary>
    /// <param name="objName">Name of the obtained object.</param>
    public T Obtain<T>(string objName) where T : MonoBehaviour
    {
        GameObject go = Obtain(objName, _parent);
        T comp = go.GetComponent<T>();

        if (comp == null)
            throw new InvalidOperationException($"Pool does not contains objects with name '{objName}' and type '{typeof(T)}'.");

        return comp;
    }

    /// <summary>
    /// Obtain object from pool by type and name as child of specific parent.
    /// </summary>
    /// <param name="objName">Name of the obtained object.</param>
    /// <param name="parent">Reference to specific object's parent.</param>
    public T Obtain<T>(string objName, Transform parent) where T : MonoBehaviour
    {
        GameObject go = Obtain(objName, parent);
        T comp = go.GetComponent<T>();

        if (comp == null)
            throw new InvalidOperationException($"Pool does not contains objects with name '{objName}' and type '{typeof(T)}'.");

        return comp;
    }

    /// <summary>
    /// Return object to pool for reuse.
    /// </summary>
    public void Release(GameObject obj, float? releaseDelay = null)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        string key = GetKey(obj.name);

        if (!map.ContainsKey(key))
            throw new InvalidOperationException($"Pool does not support objects with name '{key}'.");

        if (obj.activeInHierarchy)
            obj.SetActive(false);

        if (releaseDelay != null)
        {
            objectsToRelease.Add(obj);
            Invoke(nameof(ReleaseAfterDelay), releaseDelay.Value);

            return;
        }

        pool.Add(obj);

        OnRelease?.Invoke(this, obj);
        _onRelease?.Invoke(this, obj);
    }

    /// <summary>
    /// Return object to pool for reuse.
    /// </summary>
    public void Release<T>(T obj, float? releaseDelay = null) where T : MonoBehaviour
    {
        Release(obj.gameObject, releaseDelay);
    }

    private void ReleaseAfterDelay()
    {
        if (objectsToRelease.Count < 1)
            return;

        pool.Add(objectsToRelease[0]);

        OnRelease?.Invoke(this, objectsToRelease[0]);
        _onRelease?.Invoke(this, objectsToRelease[0]);

        objectsToRelease.RemoveAt(0);
    }

    private string GetKey(string objName)
    {
        return objName.Contains("(Clone)") ? objName.Replace("(Clone)", "") : objName;
    }
}