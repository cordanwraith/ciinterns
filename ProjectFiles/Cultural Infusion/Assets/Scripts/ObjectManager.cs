using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ObjectManager
{
    static List<GameObject> objectBank = new List<GameObject>();
    static List<GameObject> objectPool = new List<GameObject>();

    public static void AddObject(GameObject _object)
    {
        /* Objects will use this to add themselves
        to the object bank */
        objectBank.Add(_object);
        Debug.Log("Add object to list");
    }

    public static void SetInitialPool(int num)
    {
        /* Takes random objects from the object Bank,
        sets them to be in the initial object pool.*/
        for (int i = 0; i < num; ++i)
        {
            int rand = Random.Range(0, objectBank.Count);
            objectPool.Add(objectBank[rand]);
            objectBank.RemoveAt(rand);
        }
    }

    public static void Hit(GameObject _object)
    {
        /* This is used to check if the hit object
        is in the current object pool. */
        

        for (int i = 0; i < objectPool.Count; ++i)
        {
            string _name = _object.GetComponent<ObjectInfo>().m_name;
            string pName = objectPool[i].GetComponent<ObjectInfo>().m_name;

            if (pName == _name)
            {
                Debug.Log("FOUND ONE");
                _object.GetComponent<ObjectInfo>().Die();
                break;
            }
        }
    }
}
