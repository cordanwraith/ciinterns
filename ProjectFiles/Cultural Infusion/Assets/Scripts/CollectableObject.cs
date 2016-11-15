using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class CollectableObject : MonoBehaviour, IPointerClickHandler
{
    private GameManager m_manager;

    void Start()
    {
        m_manager = FindObjectOfType<GameManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_manager.OnGameObjectClicked(gameObject);
    }


    IEnumerator CollectionCountDown()
    {
        yield return new WaitForSeconds(1);
        m_manager.OnGameObjectClicked(gameObject);
    }
}
