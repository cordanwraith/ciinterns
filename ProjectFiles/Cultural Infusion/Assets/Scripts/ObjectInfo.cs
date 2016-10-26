using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class ObjectInfo : MonoBehaviour, IPointerClickHandler
{
    public string m_name;
    public string m_description;

    private GameManager m_manager;
    
    void Awake()
    {
        m_manager = FindObjectOfType<GameManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_manager.OnGameObjectClicked(gameObject);
    }
}
