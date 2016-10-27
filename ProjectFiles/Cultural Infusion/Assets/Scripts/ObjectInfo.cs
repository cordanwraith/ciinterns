using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class ObjectInfo : MonoBehaviour, IPointerClickHandler
{
    public string m_name;
    public string m_description;

    private GameManager m_manager;
    
    void Start()
    {
        m_manager = FindObjectOfType<GameManager>();
        m_manager.AddObjectToBank(gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_manager.OnGameObjectClicked(gameObject);
    }

    public void SpawnPrefab(GameObject go)
    {
        GameObject _go;
        _go = Instantiate(go, transform.position, transform.rotation) as GameObject;
        _go.transform.parent = transform;
    }
}
