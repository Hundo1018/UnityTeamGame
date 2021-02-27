using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    #region 單例模式
    private static ObjectPool _instance;
    private ObjectPool()
    {
        _pool = new Dictionary<string, Queue<GameObject>>();
        _prefabs = new Dictionary<string, GameObject>();
    }
    public static ObjectPool GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ObjectPool();
        }
        return _instance;
    }
    #endregion
    private Dictionary<string, Queue<GameObject>> _pool;
    private Dictionary<string, GameObject> _prefabs;
    /// <summary>
    /// 為物件建立物件池
    /// </summary>
    /// <param name="prefab">預製體</param>
    /// <param name="objectName">識別用的物件名字</param>
    /// <param name="n">預先初始化的數量</param>
    void CreatePool(GameObject prefab, string objectName, int n)
    {
        _prefabs.Add(objectName, prefab);
        _pool.Add(objectName, new Queue<GameObject>());
        for (int i = 0; i < n; i++)
        {
            GameObject go = GameObject.Instantiate(prefab);
            go.SetActive(false);
            _pool[objectName].Enqueue(go);
        }
    }
    void ReUse(string objectName, Vector3 position, Quaternion rotation)
    {
        GameObject go;
        if (_pool[objectName].Count > 0)
        {
            go = _pool[objectName].Dequeue();
            go.transform.position = position;
            go.transform.rotation = rotation;
            go.SetActive(true);
        }
        else
        {
            go = GameObject.Instantiate(_prefabs[objectName]);
            go.transform.position = position;
            go.transform.rotation = rotation;
        }
    }
    void Recycle(GameObject go, string objectName)
    {
        go.SetActive(false);
        _pool[objectName].Enqueue(go);
    }
}