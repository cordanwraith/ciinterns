using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public LevelData data;

    private List<GameObject> object_Bank = new List<GameObject>();
    private List<GameObject> object_Pool = new List<GameObject>();

    void Awake ()
    {
        CreateLevelObjects();
        AddObjectsToPool(4);
	}

    bool CheckForWin()
    {
        int num;
        num = object_Bank.Count;
        num += object_Pool.Count;

        if (object_Pool.Count == 0 && num > 0)
            AddObjectsToPool(4);

        return (num == 0);
    }


    private void CreateLevelObjects()
    {
        foreach(var obj in data.Objects)
        {
            for(int i = 0;i < obj.count; ++i)
            {
                Vector3 pos = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
                FindPointOnNav(pos);

                GameObject go = Instantiate(obj.prefab, pos, Quaternion.identity) as GameObject;
                object_Bank.Add(go);                
            }
        }
    }

    private Vector3 FindPointOnNav(Vector3 vec3)
    {
        Vector3 result = new Vector3();
        float range = 1.0f;
        bool foundPoint = false;

        while (!foundPoint)
        {
            Vector3 randomPoint = vec3 + Random.insideUnitSphere * range;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                foundPoint = true;
            }
            else
            {
                result = Vector3.zero;
                foundPoint = false;
                range += 1.0f;
            }
        }

        return result;
    }

    
    public void OnGameObjectClicked(GameObject go)
    {
        if (object_Pool.Contains(go))
        {
            RemoveObjectFromPool(go);
        }
    }

    public void AddObjectsToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (object_Bank.Count == 0) break;

            int rand = Random.Range(0, object_Bank.Count);
            object_Pool.Add(object_Bank[rand]);
            object_Bank.RemoveAt(rand);
        }
    }

    private void RemoveObjectFromPool(GameObject go)
    {
        object_Pool.Remove(go);
        Destroy(go);
        CheckForWin();
    }

    
}
