using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    protected static GameManager instance;

    /* This level data file holds 
     * information about what objects 
     * to spawn, and how many.
     */
    public LevelData data;

    private List<GameObject> m_objectBank = new List<GameObject>();
    private List<GameObject> m_objectPool = new List<GameObject>();
    float m_totalObjectWeight;

    public static GameManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        // Make sure there is only GameManager in the scene.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        SetInitialObjects();
        AddObjectsToPool(4);
	}

    //WIN CHECK
    bool CheckForWin()
    {
        int num;
        num = m_objectBank.Count;
        num += m_objectPool.Count;

        if (m_objectPool.Count == 0 && num > 0)
            AddObjectsToPool(4);

        return (num == 0);
    }
    
    //WHEN OBJECT IS CLICKED
    public void OnGameObjectClicked(GameObject go)
    {
        if (m_objectPool.Contains(go))
            RemoveObjectFromPool(go);
    }

    /// ADD OBJECTS TO POOL\BANK ///
    public void AddObjectsToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (m_objectBank.Count == 0) break;

            // Select randomly from the bank
            int randID = Random.Range(0, m_objectBank.Count);

            // Add to pool, then remove from bank.
            m_objectPool.Add(m_objectBank[randID]);
            m_objectBank.RemoveAt(randID);
        }
    }

    public void AddObjectToBank(GameObject go)
    {
        m_objectBank.Add(go);
    }

    /// REMOVE OBJECTS FROM POOL\BANK ///
    private void RemoveObjectFromBank(GameObject go)
    {
        m_objectBank.Remove(go);
    }

    private void RemoveObjectFromPool(GameObject go)
    {
        m_objectPool.Remove(go);
        Destroy(go);
        CheckForWin();
    }

    /// SPAWNING OBJECTS ///
    private GameObject Spawn()
    {
        // Generate a random position in the list.
        float pick = Random.value * m_totalObjectWeight;
        int chosenIndex = 0;
        float cumulativeWeight = data.Objects[0].weight;

        // Step through the list until we've accumulated more weight than this.
        // The length check is for safety in case rounding errors accumulate.
        while (pick > cumulativeWeight && chosenIndex < data.Objects.Count - 1)
        {
            chosenIndex++;
            cumulativeWeight += data.Objects[chosenIndex].weight;
        }

        // Spawn the chosen item.
        return data.Objects[chosenIndex].go;
    }

    void UpdateTotalWeight()
    {
        m_totalObjectWeight = 0f;
        foreach (var spawnable in data.Objects)
            m_totalObjectWeight += spawnable.weight;
    }

    void SetInitialObjects()
    {
        foreach (var obj in m_objectBank)
        {
            GameObject go;

            go = Instantiate(Spawn(), obj.transform.position, obj.transform.rotation) as GameObject;
            go.transform.parent = obj.transform;
        }
    }
}
