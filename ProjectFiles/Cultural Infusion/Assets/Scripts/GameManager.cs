using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public LevelData data;

    private List<GameObject> m_objectBank = new List<GameObject>();
    private List<GameObject> m_objectPool = new List<GameObject>();

    void Start()
    {
        AddObjectsToPool(4);
	}

    bool CheckForWin()
    {
        int num;
        num = m_objectBank.Count;
        num += m_objectPool.Count;

        if (m_objectPool.Count == 0 && num > 0)
            AddObjectsToPool(4);

        return (num == 0);
    }
    
    public void OnGameObjectClicked(GameObject go)
    {
        if (m_objectPool.Contains(go))
        {
            RemoveObjectFromPool(go);
        }
    }

    public void AddObjectsToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (m_objectBank.Count == 0) break;

            int rand = Random.Range(0, m_objectBank.Count);
            m_objectPool.Add(m_objectBank[rand]);
            m_objectBank.RemoveAt(rand);
        }
    }

    public void AddObjectToBank(GameObject go)
    {
        m_objectBank.Add(go);
    }

    private void RemoveObjectFromPool(GameObject go)
    {
        m_objectPool.Remove(go);
        Destroy(go);
        CheckForWin();
    }
}
