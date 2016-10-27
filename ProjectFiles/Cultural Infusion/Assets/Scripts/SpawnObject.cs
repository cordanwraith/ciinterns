using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class SpawnObject : MonoBehaviour, IPointerClickHandler
{
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
}
