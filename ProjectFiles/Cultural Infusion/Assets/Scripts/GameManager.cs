using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //total number of objects that /could/ spawn
    private int m_totalNumOfCollectables;
    //total number of objects /to/ spawn. If this is less than total num of collectables it's set to that
    private int m_numCollectableInScene = 4;

    public Image[] images;
    GameObject[] m_collectableObjects;
    bool[] m_collectedObjects;

    void Start()
    {
        //seed the collected objects array
        m_collectedObjects = new bool[m_numCollectableInScene];
        for (int i = 0; i < m_numCollectableInScene; ++i)
            m_collectedObjects[i] = false;

        SpawnObjects();
    }

    private void SpawnObjects()
    {
        //find all the objects
        GameObject[] collectiblePrefabs = Resources.LoadAll<GameObject>("Tanabata/Collectables/");
        m_totalNumOfCollectables = collectiblePrefabs.Length;

        //four objects to collect
        m_collectableObjects = new GameObject[m_numCollectableInScene];

        //if (m_totalNumOfCollectables < m_numCollectableInScene)
        //    m_numCollectableInScene = m_totalNumOfCollectables;

        //randomize the objects to choose
        List<int> whichObjectsToSpawn = new List<int>(m_numCollectableInScene);
        for (int i = 0; i < m_numCollectableInScene; ++i)
        {
            int r = Random.Range(0, m_totalNumOfCollectables);
            //if (whichObjectsToSpawn.Contains(r))
            //{
            //    i--;
            //    continue;
            //}
            whichObjectsToSpawn.Add(r);
        }

        //find ui locations and images
        Object[] collectImages = Resources.LoadAll("Images/", typeof(Sprite));
        Sprite[] sprites = new Sprite[collectImages.Length];

        //convert images to sprites
        for (int i = 0; i < collectImages.Length; i++)
        {
            sprites[i] = (Sprite)collectImages[i];
        }

        //find all objects with the tag 'SpawnLocation'
        GameObject[] spawnLocations = GameObject.FindGameObjectsWithTag("SpawnLocation");

        //randomize the locations to choose
        List<int> whereToSpawnObjects = new List<int>(spawnLocations.Length);
        for (int i = 0; i < spawnLocations.Length; ++i)
        {
            int r = Random.Range(0, spawnLocations.Length);
            if (whereToSpawnObjects.Contains(r))
            {
                i--;
                continue;
            }
            whereToSpawnObjects.Add(r);
            //change ui image sprite
            images[i].GetComponent<Image>().sprite = sprites[r];
        }

        //todo: generalize this (use scene name?)
        //todo: create the objects
        for (int i = 0; i < m_numCollectableInScene; ++i)
        {
            m_collectableObjects[i] = (GameObject)Instantiate(collectiblePrefabs[whichObjectsToSpawn[i]], spawnLocations[i].transform.position, Quaternion.identity);
            m_collectableObjects[i].transform.parent = spawnLocations[i].transform;
        }
    }

    void Update()
    {
        if (CheckForWin())
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    //WIN CHECK
    bool CheckForWin()
    {
        bool ret = true;
        for (int i = 0; i < m_numCollectableInScene; ++i)
            ret = ret && m_collectedObjects[i];

        return ret;
    }

    //WHEN OBJECT IS CLICKED
    public void OnGameObjectClicked(GameObject go)
    {
        int i = 0;
        for (; i < m_numCollectableInScene; ++i)
        {
            if (m_collectableObjects[i] == go)
                break;
            if (i == m_numCollectableInScene)
                return;
        }

        if (i == m_numCollectableInScene)
            return;

        Debug.Log(go);
        m_collectedObjects[i] = true;
        Destroy(go);
    }
}
